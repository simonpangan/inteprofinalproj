using Intepro.DataAccess;
using InteproFinalProj.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace InteproFinalProj.Models
{
    public class AccountModel
    {
        [Key]
        public int accountID { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]


        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string confirmPassword { get; set; }


       static DAL dl = new DAL();

        public static void Add(AccountModel input)
        {
         
            dl.Open();
            dl.SetSql("INSERT INTO AccountsTable VALUES" +
                " (@firstName,@lastName, @email, @password)");
            dl.AddParam("@firstName", input.firstName);
            dl.AddParam("@lastName", input.lastName);
            dl.AddParam("@email", input.email);
            dl.AddParam("@password", Helper.ComputeSha256Hash(input.password));
            dl.Execute();
            dl.Close();
        }

        public static bool IsExisting(string email)
        {

            dl.Open();
            dl.SetSql("SELECT Email FROM AccountsTable WHERE email = @email");
            dl.AddParam("@email", email);
            SqlDataReader dr = dl.GetReader();
            if (dr.Read() == true)
            {
                dr.Close();
                dl.Close();
                return true;
            }
            dr.Close();
            dl.Close();
            return false;
        }
    }
}