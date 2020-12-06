using Intepro.DataAccess;
using InteproFinalProj.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace InteproFinalProj.Models
{
    public class StudentsModel
    {
        [Key]

        [Display(Name = "Student ID")]
        public int? studentID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]


        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Student Number is Required")]
              
        [RegularExpression("\\b\\d{8}\\b", ErrorMessage = "Student Number must be 8 digits only")]

        public long studentNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string firstName { get; set; }

        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Middle Name is Required")]
        public string middleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]

        public string lastName { get; set; }

       
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is Required")]
        public int age { get; set; }

        [Display(Name = "Sex")]
        [Required(ErrorMessage = "Sex is Required")]
        public string sex { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is Required")]
        public string address { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "Telephone Number is Required")]
        public string telephoneNumber { get; set; }


        [Display(Name = "Cellphone Number")]
        [Required(ErrorMessage = "Cellphone Number is Required")]
        [RegularExpression("((\\+63)|0)\\d{10}", ErrorMessage = "Invalid format. Example: +639201112222 or 09201112222")]

        public string cellphoneNumber { get; set; }


        [Display(Name = "Course")]
        [Required(ErrorMessage = "Course is Required")]
        public string course { get; set; }

      


        static DAL dl = new DAL();



        public static bool IsExisting(long studentNumber)
        {

            dl.Open();
            dl.SetSql("SELECT * FROM StudentTable WHERE studentNumber = @studentNumber");
            dl.AddParam("@studentNumber", studentNumber);
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
        public static void Add(StudentsModel input)
        {

            dl.Open();
            dl.SetSql("INSERT INTO StudentTable VALUES" +
                " (@studentNumber,@firstName,@middleName,@lastName," +
                " @age, @sex,@address,@telephoneNumber," +
                "@cellphoneNumber,@course)");
            dl.AddParam("@studentNumber", input.studentNumber);
            dl.AddParam("@firstName", input.firstName);
            dl.AddParam("@middleName", input.middleName);
            dl.AddParam("@lastName", input.lastName);
            dl.AddParam("@age", input.age);

            dl.AddParam("@sex", input.sex);
            dl.AddParam("@address", input.address);
            dl.AddParam("@telephoneNumber", input.telephoneNumber);
            dl.AddParam("@cellphoneNumber", input.cellphoneNumber);
            dl.AddParam("@course", input.course);
       

            dl.Execute();
            dl.Close();
        }
        public static List<StudentsModel> getStudents()
        {
            List<StudentsModel> list =
                new List<StudentsModel>();
            dl.Open();
            dl.SetSql("SELECT * FROM StudentTable INNER JOIN SexTable ON StudentTable.sexID = SexTable.sexID; ");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
          
                StudentsModel ac = new StudentsModel();
                ac.studentID = (int)dr[0];
                ac.studentNumber = (long)dr[1];
                ac.firstName = dr[2].ToString();
                ac.middleName = dr[3].ToString();
                ac.lastName = dr[4].ToString();
                ac.age = (int)dr[5];
                ac.sex = dr[12].ToString();
                ac.address = dr[7].ToString();
                ac.telephoneNumber = dr[8].ToString();
                ac.cellphoneNumber = dr[9].ToString();
                ac.course = dr[10].ToString();
        
                list.Add(ac);    
            }
            dr.Close();
            dl.Close();
            return list;
        }
        public static StudentsModel Get1Employee(int? id)
        {
            StudentsModel ac = new StudentsModel();
            dl.Open();
            dl.SetSql("SELECT * FROM StudentTable WHERE studentID=@id");
            dl.AddParam("@id", id);
            SqlDataReader dr = dl.GetReader();
            if (dr.Read() == true)
            {
                ac.studentID = (int)dr[0];
                ac.studentNumber = (long)dr[1];
                ac.firstName = dr[2].ToString();
                ac.middleName = dr[3].ToString();
                ac.lastName = dr[4].ToString();
                ac.age = (int)dr[5];
                ac.sex = dr[6].ToString();
                ac.address = dr[7].ToString();
                ac.telephoneNumber = dr[8].ToString();
                ac.cellphoneNumber = dr[9].ToString();
                ac.course = dr[10].ToString();

            }
            dr.Close();
            dl.Close();
            return ac;
        }
        public static void Update(StudentsModel input)
        {
            dl.Open();
            dl.SetSql("UPDATE StudentTable SET studentNumber=@studentNumber," +
                " firstName=@firstName, middleName=@middleName," +
                "lastName=@lastName ,age=@age " +
                ",sexID=@sex ,address=@address " +
                ",telephoneNumber=@telephoneNumber " +
                ",cellphoneNumber=@cellphoneNumber, course=@course " +
                "WHERE studentID=@studentID");

            dl.AddParam("@studentNumber", input.studentNumber);
            dl.AddParam("@firstName", input.firstName);
            dl.AddParam("@middleName", input.middleName);
            dl.AddParam("@lastName", input.lastName);
            dl.AddParam("@age", input.age);
            dl.AddParam("@sex", input.sex);
            dl.AddParam("@address", input.address);
            dl.AddParam("@telephoneNumber", input.telephoneNumber);
            dl.AddParam("@cellphoneNumber", input.cellphoneNumber);
            dl.AddParam("@course", input.course);
            dl.AddParam("@studentID", input.studentID);

            dl.Execute();
            dl.Close();
        }   

        public static void Delete(int id)
        {
            dl.Open();
            dl.SetSql("DELETE StudentTable WHERE studentID = @id");
            dl.AddParam("@id", id);
            dl.Execute();
            dl.Close();
        }
        public static int accountsCount()
        {

            dl.Open();
            dl.SetSql("SELECT * FROM StudentTable");
            SqlDataReader dr = dl.GetReader();
            int allaccontscount = 0;
            while (dr.Read() == true)
            {
                allaccontscount++;


            }
            dr.Close();
            dl.Close();
            return allaccontscount;
        }
        public static int accountsISCount()
        {

            dl.Open();
            dl.SetSql("SELECT * FROM StudentTable WHERE course ='BS-IS'");
            SqlDataReader dr = dl.GetReader();
            int allaccontscount = 0;
            while (dr.Read() == true)
            {
                allaccontscount++;


            }
            dr.Close();
            dl.Close();
            return allaccontscount;
        }

            public static int accountsCACount()
            {

                dl.Open();
                dl.SetSql("SELECT * FROM StudentTable WHERE course = 'BS-CA'");
                SqlDataReader dr = dl.GetReader();
                int allaccontscount = 0;
                while (dr.Read() == true)
                {
                    allaccontscount++;

            
                }
                dr.Close();
                dl.Close();
                return allaccontscount;
            }
        }


    }

 