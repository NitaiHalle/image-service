using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    class LogData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Massage")]
        public string Massage { set; get; }
        public LogData(string m,string t)
        {
            Massage = m;
            Type = t;
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
