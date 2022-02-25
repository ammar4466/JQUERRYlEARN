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
                SqlCommand cmd = new SqlCommand("insert into tbl_employee values(@Name, @Email, @City,@Vehicle)", con);
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
        [HttpPost]
        [Route("Update")]
        public ActionResult Update(EmpModel model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_employee set Name=@Name,Email=@Email,City=@City,Vehicle=@Vehicle where Id = @Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", model.Id);
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
        [HttpPost]
        [Route("GetData")]
        public ActionResult GetData(EmpModel model)
        {
            List<EmpModel> empdata = new List<EmpModel>();
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from tbl_employee", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                // DataTable dt = new DataTable();
                while (dr.Read())
                {
                    if (dr != null)
                    {
                        EmpModel emp = new EmpModel();
                        emp.Id = Convert.ToInt32(dr["Id"]);
                        emp.Name = Convert.ToString(dr["Name"]);
                        emp.City = Convert.ToString(dr["City"]);
                        emp.Email = Convert.ToString(dr["Email"]);
                        emp.Vehicle = Convert.ToString(dr["Vehicle"]);
                        empdata.Add(emp);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return Json(empdata);
        }
        [HttpPost]
        [Route("DelData")]
        public ActionResult DelData(int Id=0)
        {
            SqlCommand cmd = new SqlCommand("Delete from tbl_employee where Id = @Id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("EditData")]
        public ActionResult EditData(int Id = 0)
        {
            EmpModel emp = new EmpModel();
            try
            {
                SqlCommand cmd = new SqlCommand("select *  from tbl_employee where Id= @Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                    if (dr != null)
                    {
                        emp.Id = Convert.ToInt32(dr["Id"]);
                        emp.Name = Convert.ToString(dr["Name"]);
                        emp.City = Convert.ToString(dr["City"]);
                        emp.Email = Convert.ToString(dr["Email"]);
                        emp.Vehicle = Convert.ToString(dr["Vehicle"]);
                    }
                
                con.Close();
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return Json(emp);
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