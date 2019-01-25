using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication2.Models
{
    public class Photo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Path")]
        public string Path { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "tPath")]
        public string tPath { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "rPath")]
        public string rPath { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "rPath")]
        public string tRPath { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "NameOfPhoto")]
        public string NameOfPhoto { set; get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Date")]
        public string Date { set; get; }

        public Photo(string path)
        {
            tPath = path;
            string [] divideFlag = { @"\OutputDir\Thumbnails" };
            string[] pathSeprate = tPath.Split(divideFlag,StringSplitOptions.None);

            string[] divideFlag1 = { @"\Thumbnails" };
            string[] pathSeprate1 = tPath.Split(divideFlag1, StringSplitOptions.None);

            Path = pathSeprate1[0] + pathSeprate[1];
            rPath = @"..\OutputDir" + pathSeprate[1];
            tRPath = @"..\OutputDir\Thumbnails"+ pathSeprate[1];
            DateTime date = GetDateTakenFromImage(Path);
            Date = date.Month.ToString() + @"/" + date.Year.ToString();
            NameOfPhoto = Path.Substring(Path.LastIndexOf('\\') + 1);
        }


        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        private DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                try
                {

                    PropertyItem propItem = myImage.GetPropertyItem(36867);

                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);

                    return DateTime.Parse(dateTaken);
                }
                catch (Exception e)
                {
                    return new DateTime();
                }

            }

        }
    }
}