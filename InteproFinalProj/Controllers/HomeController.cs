using Intepro.DataAccess;
using InteproFinalProj.HelperClass;
using InteproFinalProj.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InteproFinalProj.Controllers
{
    public class HomeController : Controller
    {
    

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            DAL dl = new DAL();



            TempData["Tag"] = "";
            dl.Open();
            dl.SetSql("SELECT accountID,firstName, lastName FROM AccountsTable WHERE email = @email AND password =@password");
            dl.AddParam("@email", Request.Form["email"]);
            dl.AddParam("@password", Helper.ComputeSha256Hash(Request.Form["password"]));
            SqlDataReader dr = dl.GetReader();
            if (dr.Read() == true)
            {
                Session["UserID"] = dr[0].ToString();
           
                return RedirectToAction("Index", "Admin");
            }
            dr.Close();
            dl.Close();



            TempData["Tag"] = "Incorrect Credentials!!";
            return RedirectToAction("Login", "Home");
        }




        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(AccountModel inputs)
        {
            ViewBag.Error = "";
            if (AccountModel.IsExisting(inputs.email))
            {
                ViewBag.Error = "<span class='field-validation-valid text-danger'> Email Already Existing </span>";
                return View(inputs);
            }

            AccountModel.Add(inputs);
            Session["UserID"] = inputs.accountID;

            return RedirectToAction("Index", "Admin");
        }

      

 




    }
}