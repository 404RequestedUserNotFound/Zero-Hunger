using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZH.Models.ZHEF;

namespace ZH.Controllers
{
    public class ResturentController : Controller
    {
        // GET: Resturent
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var db = new ZHEntities();
            ResturentTable resturentTable = (from resturent in db.ResturentTables
                                             where resturent.Username.Equals(Username) &&
                                             resturent.Password.Equals(Password)
                                             select resturent).FirstOrDefault();


            if (resturentTable == null)
            {
                TempData["msg"] = "Resturent username and password not found";
                return View();
            }
            else
            {
                var resturentLogged = (from resturent in db.ResturentTables
                                       where resturent.Username.Equals(Username)
                                       select resturent).FirstOrDefault();

                var resturentId = resturentLogged.Id;
                Session["resturentLogged"] = resturentId;
                return RedirectToAction("Dashboard");
            }
        }













        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("resturentLogged");
            return RedirectToAction("Login");
        }









        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }






        [HttpGet]
        public ActionResult RequestCollection()
        {
            var db = new ZHEntities();
            var resturentLogged = Convert.ToInt32(Session["resturentLogged"]);
            var res = (from resturent in db.FoodTables
                       where resturent.RequestedRes == resturentLogged
                       select resturent).ToList();

            return View(res);
        }








        [HttpGet]
        public ActionResult CreateRequest()
        {
            return View();
        }










        [HttpPost]
        public ActionResult CreateRequest(string Foodtype, string Quantity)
        {
            var db = new ZHEntities();
            FoodTable foodTable = new FoodTable();
            foodTable.FoodType = Foodtype;
            foodTable.Quantity = Quantity;
            foodTable.Quantity = Quantity;
            foodTable.RequestedRes = Convert.ToInt32(Session["resturentLogged"]);
            foodTable.Status = "Pending";
            db.FoodTables.Add(foodTable);
            db.SaveChanges();
            TempData["msg"] = "Request for food collection has been confirmed";
            return RedirectToAction("RequestCollection");
        }











        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ZHEntities();
            var ft = (from food in db.FoodTables
                       where food.Id == id
                       select food).SingleOrDefault();



            db.FoodTables.Remove(ft);
            db.SaveChanges();
            TempData["msg"] = "Request for food collection has been removed";
            return RedirectToAction("CollectRequest");

        }




        [HttpGet]
        public ActionResult ManageResturent()
        {
            var db = new ZHEntities();
            var resturent = db.ResturentTables.ToList();
            return View(resturent);
        }






        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }





        [HttpPost]
        public ActionResult Create(ResturentTable resturentTable)
        {
            var db = new ZHEntities();
            db.ResturentTables.Add(resturentTable);
            db.SaveChanges();
            TempData["msg"] = "resturent has been added to the system";
            return RedirectToAction("ManageResturent");
        }







        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ZHEntities();
            var resturentTable = (from resturent in db.ResturentTables
                       where resturent.Id == id
                       select resturent).SingleOrDefault();
            return View(resturentTable);

        }






        [HttpPost]
        public ActionResult Edit(ResturentTable resturentTable)
        {
            var db = new ZHEntities();
            var res = (from resturent in db.ResturentTables
                       where resturent.Id == resturentTable.Id
                       select resturent).SingleOrDefault();

            res.Name = resturentTable.Name;
            res.Username = resturentTable.Username;
            res.Password = resturentTable.Password;
            res.Location = resturentTable.Location;
            db.SaveChanges();


            TempData["msg"] = "Resturent has been added to the system";
            return RedirectToAction("ManageResturent");
        }









        [HttpGet]
        public ActionResult DeleteResturent(int id)
        {
            var db = new ZHEntities();
            var res = (from resturent in db.ResturentTables
                       where resturent.Id == id
                       select resturent).SingleOrDefault();


            db.ResturentTables.Remove(res);
            db.SaveChanges();

            TempData["msg"] = "Resturent has been deleted";
            return RedirectToAction("ManageResturent");
        }



    }
}