using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Commuincation;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        static List<Employee> employees = new List<Employee>()
        {
          new Employee  { FirstName = "Moshe", LastName = "Aron", Email = "Stam@stam", Salary = 10000, Phone = "08-8888888" },
          new Employee  { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 2000, Phone = "08-8888888" },
          new Employee   { FirstName = "Mor", LastName = "Sinai", Email = "Stam@stam", Salary = 500, Phone = "08-8888888" },
          new Employee   { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 20, Phone = "08-8888888" },
          new Employee   { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 700, Phone = "08-8888888" }
        };
        static WebClient client = WebClient.singeltonClient();
        
        static Config config = new Config();
        static ImageWeb IW = new ImageWeb(config);
        static LogModel logModel = new LogModel();
        static List<string> handlers = new List<string>();
        static PhotosModel photosModel = new PhotosModel(config);


        public FirstController()
        {
            

        }
        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult logView()
        {
            logModel.getFile();
            return View(logModel);
        }
       
        public ActionResult PhotosView()
        {
            ViewBag.photos = photosModel.PhotosList;
            return View(photosModel.PhotosList);
        }
        public ActionResult ImageView(string p)
        {
            Photo photo = new Photo(p);
            return View(photo);
        }

        public ActionResult DeleteImageView(string p)
        {
            Photo photo = new Photo(p);
            return View(photo);
        }
        public ActionResult PhotosView1()
        {
            ViewBag.photos = photosModel.PhotosList;
            return View(photosModel.PhotosList);
        }
        public ActionResult ImageWebView()
        {
            if (!client.connected())
            {
                client.reconnect();
            }
            ViewBag.Connected = client.connected();
            ViewBag.numbersPhotos = IW;

            return View(IW);
        }
        [HttpGet]
        public ActionResult Config()
        {

            return View(config);
        }

        public ActionResult Config(string item)
        {

            return RedirectToAction("removeHandler",new { item });
        }
        [HttpGet]
        public ActionResult AjaxView()
        {
             
            return View();
        }
        public ActionResult removeHandler(string item)
        {
            ViewBag.handler = item;
            return View();
        }
        public void removeFromHandlers(string path)
        {
            config.deleteHandler(path);
            
        }
        public void DeleteImage(string path, string pathT)
        {
            photosModel.delete(path);
            photosModel.delete(pathT);
        }
        [HttpGet]
        public string GetStatus()
        {
            
            return IW.GetStatus();
        }
        [HttpGet]
        public string GetNumOfPhotos()
        {

            return IW.GetNumPhotos();
        }
        [HttpGet]
        public JObject GetEmployee()
        {
            JObject data = new JObject();
            data["FirstName"] = "Kuky";
            data["LastName"] = "Mopy";
            return data;
        }

        [HttpPost]
        public JObject GetEmployee(string name, int salary)
        {
            foreach (var empl in employees)
            {
                if (empl.Salary > salary || name.Equals(name))
                {
                    JObject data = new JObject();
                    data["FirstName"] = empl.FirstName;
                    data["LastName"] = empl.LastName;
                    data["Salary"] = empl.Salary;
                    return data;
                }
            }
            return null;
        }

        // GET: First/Details
        public ActionResult Details()
        {
            return View(employees);
        }

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: First/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                employees.Add(emp);

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (Employee emp in employees) {
                if (emp.ID.Equals(id)) { 
                    return View(emp);
                }
            }
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee empT)
        {
            try
            {
                foreach (Employee emp in employees)
                {
                    if (emp.ID.Equals(id))
                    {
                        emp.copy(empT);
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: First/Delete/5
        public ActionResult Delete(int id)
        {
            int i = 0;
            foreach (Employee emp in employees)
            {
                if (emp.ID.Equals(id))
                {
                    employees.RemoveAt(i);
                    return RedirectToAction("Details");
                }
                i++;
            }
            return RedirectToAction("Error");
        }
    }
}
