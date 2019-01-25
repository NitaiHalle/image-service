using Shered;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Commuincation;

namespace WebApplication2.Models
{
    public class LogModel
    {
        private IWebClient webClient;
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "logsList")]
        public List<Log> logsList { get; set; }
        public event EventHandler<TypeEventArgs> sendData;
        public LogModel()
        {
            logsList = new List<Log>();
            webClient = WebClient.singeltonClient();
            sendData += webClient.send;
            webClient.OnDataRecived += GetDataFromServer;

            TypeEventArgs e = new TypeEventArgs((int)CommandEnum.LogCommand, null);
            sendData?.Invoke(this, e);

        }
        public void getFile()
        {
            TypeEventArgs e = new TypeEventArgs((int)CommandEnum.LogCommand, null);
            sendData?.Invoke(this, e);
        }
        private void GetDataFromServer(object sender, TypeEventArgs e)
        {
            int type = e.TypeArgs;
            switch (type)
            {
                case (int)CommandEnum.LogCommand:
                    massageLog(e.Args);
                    break;
                case (int)CommandEnum.NewFileCommand:
                    LogFile(e.Args);
                    break;
            }
        }
        private void massageLog(string[] args)
        {
            try
            {
                //string[] arg = args[0].Split(',');
                Log log = new Log(args[0], args[1]);
                logsList.Add(log);
            }
            catch (Exception exc) { }
        }

        private void LogFile(string[] args)
        {
            logsList.Clear();
            Log l = new Log("1", "xsxs");
            //logsList.Add(l);
            for (int i = 0; i < args.Length - 1; i++)
            {
                try
                {
                    string[] log = args[i].Split(',');
                    Log logData = new Log(log[0], log[1]);
                    if (log[0] != null && !log[1].Equals("new client connecting.."))
                    {
                        logsList.Add(logData);
                    }
                    
                }
                catch (Exception exc) { }
            }
        }
    }
}