using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZH.Models.ZHEF;

namespace ZH.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index()
        {
            return View();
        }







        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }







        //login form database and checking if null
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var db = new ZHEntities();
            NGOTable ngotable = (from NGOTable in db.NGOTables
                                 where NGOTable.Username.Equals(Username) &&
                                       NGOTable.Password.Equals(Password)
                                 select NGOTable).FirstOrDefault();


            if (ngotable == null)
            {
                TempData["msg"] = "Username or password not found";
                return View();
            }
            else
            {
                var NGOLogged = (from NGOTable in db.NGOTables
                                 where NGOTable.Username.Equals(Username)
                                 select NGOTable).SingleOrDefault();

                var NGOId = NGOLogged.Id;
                Session["NGOLogged"] = NGOId;
                return RedirectToAction("Dashboard");
            }

        }





        [HttpGet]
        public ActionResult Logout() {

            Session.Remove("NGOLogged");
            return RedirectToAction("Login");
        }









        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }










        //list of collection request
        public ActionResult CollectRequest()
        {
            var db = new ZHEntities();
            var foodtable = db.FoodTables.ToList();
            return View(foodtable);
        }











        [HttpGet]
        public ActionResult Employee(int id)
        {
            var db = new ZHEntities();
            var emp = (from collection in db.FoodTables
                       where collection.Id==id
                       select collection).SingleOrDefault();
            return View(emp);
        }









        [HttpPost]
        public ActionResult Employee(int Id, int Employee)
        {
            var db = new ZHEntities();
            var emp = (from collection in db.FoodTables
                       where collection.Id == Id
                       select collection).SingleOrDefault();
            db.SaveChanges();

            TempData["msg"] = "Your request is accepted";
            return RedirectToAction("Dashboard");
        }

    }

}