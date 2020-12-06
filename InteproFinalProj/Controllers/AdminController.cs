using InteproFinalProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InteproFinalProj.Controllers
{
    public class AdminController : Controller
    {
        
        public AdminController()
        {
           
        }
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.All = StudentsModel.accountsCount();
                ViewBag.CA = StudentsModel.accountsCACount();
                ViewBag.IS = StudentsModel.accountsISCount();
                return View(StudentsModel.getStudents());
            }
            return RedirectToAction("Login", "Home");

        }

        [HttpGet]
        public ActionResult Add()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Add(StudentsModel input)
        {
            if (StudentsModel.IsExisting(input.studentNumber))
            {
                ViewBag.Error = "<span class='field-validation-valid text-danger'> Student Number already existing </span>";
                return View(input);

            }
            StudentsModel.Add(input);
            return RedirectToAction("Index", "Admin");
        }
    
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Home");

            if (id == null) return RedirectToAction("Index", "Admin");
            else if(StudentsModel.Get1Employee(id) == null) return RedirectToAction("Index", "Admin");

            return View(StudentsModel.Get1Employee(id));
        }

        [HttpPost]
        public ActionResult Edit(StudentsModel ac)
        {
            StudentsModel.Update(ac);

            return RedirectToAction("Index", "Admin");
        }


        [HttpPost]
        public ActionResult Delete (int id)
        {
            StudentsModel.Delete(id);
            return RedirectToAction("Index", "Admin");
        }

     
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Login", "Home");
        }

    }
}