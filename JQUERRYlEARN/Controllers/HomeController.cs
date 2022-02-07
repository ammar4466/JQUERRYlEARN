using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using JQUERRYlEARN.Models;
using System.Configuration;

namespace JQUERRYlEARN.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
       [Route("AddData")]
        public ActionResult AddData(EmpModel model)
        {  
            try
            {
                SqlCommand cmd = new SqlCommand("insert into employee values(@Name, @Email, @City,@Vehicle)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@Vehicle", model.Vehicle);         
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return Json(model);
        }
        [HttpGet]
        [Route("GetData")]
        public ActionResult GetData(EmpModel model)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("Select *from employee", con);
                DataSet ds = new DataSet();
                
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return Json(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}