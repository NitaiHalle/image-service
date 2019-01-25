using Shered;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication2.Commuincation;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2.Models
{
    public class Config
    {
        public bool configRecieved;
        private string outputDir;
        private string sourceName;
        private string logName;
        private string thumbnail;
        private IWebClient client;
        private List<string> handlers = new List<string>();
        public event EventHandler<TypeEventArgs> sendData;
        public event EventHandler<string> path;
        private string currentHandler;
        public Config()
        {
            this.outputDir = "Outputcsdkmcdks ";
            //  this.logName = "csdc";
            //  this.thumbnail = "23";
            //  this.sourceName = "csdcds";
            //handlers.Add(this.outputDir);
            //handlers.Add(this.outputDir);
            // handlers.Add(this.outputDir);
            this.client = WebClient.singeltonClient();
            this.client.OnDataRecived += getFromServer;
            sendData += client.send;
            this.sendToServer("shit", CommandEnum.GetConfigCommand);

        }
        public void addHandler(string path)
        {
            handlers.Add(path);
        }
        public void deleteHandler(string path)
        {
            sendToServer(path, CommandEnum.CloseCommand);
            Thread.Sleep(1);
            //handlers.Remove(path);
        }

        public void sendToServer(string data, CommandEnum command)
        {
            string[] args = new string[1];
            args[0] = data;
            TypeEventArgs e = new TypeEventArgs((int)command, args);
            sendData?.Invoke(this, e);
        }
        public void getFromServer(object sender, TypeEventArgs e)
        {
            int type = e.TypeArgs;

            switch (type)
            {
                case (int)CommandEnum.GetConfigCommand:
                    updateAppconfig(e);
                    break;
                case (int)CommandEnum.CloseCommand:
                    removeHandler(e);
                    break;
            }

        }
        private void removeHandler(TypeEventArgs e)
        {
            string handler = e.Args[0];
            this.handlers.Remove(handler);
        }
        private void updateAppconfig(TypeEventArgs e)
        {
            string[] data = e.Args;
            this.OutputDir = data[0];
            this.SourceName = data[1];
            this.LogName = data[2];
            this.Thumbnail = data[3];
            
            for (int i = 4; i < data.Length; i++)
            {
                 addHandler(data[i]);

            }
            

        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDir")]
        public string OutputDir { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName")]
        public string SourceName { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName")]
        public string LogName { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers")]
        public List<string> Handlers
        {
            set { handlers = value; }
            get { return handlers; }
        }

        

       

    }
}