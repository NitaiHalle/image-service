using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Log
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Massage")]
        public string Massage { set; get; }
        
        public Log(string m, string t)
        {
            Massage = t;
            Type = m;
        }
        public string getType()
        {
            return this.Type;
        }
        public string getMassage()
        {
            return this.Massage;
        }
    }
}