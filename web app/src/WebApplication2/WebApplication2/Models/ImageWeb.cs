using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication2.Commuincation;

namespace WebApplication2.Models
{
    public class ImageWeb
    {
        private Config config;
        private WebClient client;
        private string[] studnet = new string[3];
        public ImageWeb(Config c)
        {
            this.config = c;
            client = WebClient.singeltonClient();
            bool connected = client.connected();
            if (connected)
            {
                Status = "connected";
            }
            else
            {
                Status = "disconnected";
            }
            ParseStudent();
        }
        private void ParseStudent()
        {
            StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/datails.xml"));
            string line;
            
            for(int i = 0; i<3;i++)
            {
                line = sr.ReadLine();
                this.studnet[i] = line;
            }
            sr.Close();
        }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "status")]
        public string Status { set; get; }
        public string GetName { get { return studnet[0]; } }
        public string GetLastName { get { return studnet[1]; } }
        public string GetId { get { return studnet[2]; } }
        public bool Connected()
        {
            return client.connected();
            
        }
        public string GetStatus()
        {
            bool connected = client.connected();
            if (connected)
            {
                Status = "connected";
            }
            else
            {
                Status = "disconnected";
            }
            return Status;
        }
        public string GetNumPhotos()
        {
            try
            {
                int num = Directory.GetFiles(this.config.OutputDir + @"\Thumbnails", "*", SearchOption.AllDirectories).Length;
                return num.ToString();
            }catch(Exception e)
            {
                return "0";
            }
            return null;
        }
    }
}