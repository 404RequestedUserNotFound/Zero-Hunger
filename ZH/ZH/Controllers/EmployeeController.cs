using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZH.Models.ZHEF;

namespace ZH.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
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
            EmployeeTable employeeTable = (from employee in db.EmployeeTables
                                           where employee.Username.Equals(Username) &&
                                           employee.Password.Equals(Password)
                                           select employee).SingleOrDefault();
            


            if (employeeTable == null)
            {

                TempData["msg"] = "Employee not found";
                return View();
            }
            else
            {
                var employeeLogged = (from employee in db.EmployeeTables
                                      where employee.Username.Equals(Username)
                                      select employee).SingleOrDefault();

                var employeeId = employeeLogged.Id;
                Session["employeeLogged"] = employeeId;
                return RedirectToAction("Dashboard");
            }
        }







        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("employeeLogged");
            return RedirectToAction("Login");
        }






        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }








        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            var db = new ZHEntities();
            var employeedetails = db.EmployeeTables.ToList();
            return View(employeedetails);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Create(EmployeeTable employeeTable)
        {
            var db = new ZHEntities();
            db.EmployeeTables.Add(employeeTable);
            db.SaveChanges();
            TempData["msg"] = "Your employee profile has been created";
            return RedirectToAction("EmployeeDetails");
        }







        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ZHEntities();
            var emp = (from  employee in db.EmployeeTables
                       where employee.Id == id
                       select employee).FirstOrDefault();
            return View(emp);
        }







        [HttpPost]
        public ActionResult Edit(EmployeeTable employeeTable)
        {
            var db = new ZHEntities();
            var emp = (from employee in db.EmployeeTables
                       where employee.Id == employeeTable.Id
                       select employee).SingleOrDefault();

            emp.Name = employeeTable.Name;
            emp.Username = employeeTable.Username;
            emp.Password = employeeTable.Password;
            emp.Location = employeeTable.Location;
            db.SaveChanges();


            TempData["msg"] = "Employee details has been update";
            return RedirectToAction("EmployeeDetails");
        }







        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ZHEntities();
            var emp = (from employee in db.EmployeeTables
                       where employee.Id == id
                       select employee).FirstOrDefault();

            db.EmployeeTables.Remove(emp);
            db.SaveChanges();
            TempData["msg"] = "Employee details has been removed";
            return RedirectToAction("EmployeeDetails");
        }







        [HttpGet]
        public ActionResult CollectRequest()
        {
            var db = new ZHEntities();
            var employeeLogged = Convert.ToInt32(Session["employeeLogged"]);
            var emp = (from employee in db.FoodTables
                       where employee.AssignedEmp == employeeLogged
                       select employee).ToList();

            return View(emp);
        }















        public ActionResult CollectionCompleted(int id)
        {
            var db = new ZHEntities();
            var emp = (from employee in db.FoodTables
                       where employee.Id==id
                       select employee).FirstOrDefault();


            emp.Status = "Collection Done";
            db.SaveChanges();
            return RedirectToAction("CollectRequest");
        }





    }
}