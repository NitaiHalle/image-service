using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PhotosModel
    {
        private List<Photo> list = new List<Photo>();
        private Config config;
        public PhotosModel(Config c)
        {
            config = c;
        }
        public List<Photo> PhotosList
        {
            get
            {
                try
                {
                    list.Clear();
                    string[] paths = Directory.GetFiles(config.OutputDir + @"\Thumbnails",
                        "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < paths.Length; i++)
                    {
                        list.Add(new Photo(paths[i]));
                    }
                }
                catch
                {
                }
                return list;
            }
        }
        public void delete(string path)
        {
            path = '~' + path.Substring(path.IndexOf("\\"));
            string file = System.Web.Hosting.HostingEnvironment.MapPath(path);
            try
            {
                File.Delete(file);
            }
            catch { }
        }
    }
}