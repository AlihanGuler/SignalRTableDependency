using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Models;
using System.Configuration;
using System.Data.SqlClient;
 
namespace Client.Controllers
{
    public class UserLoginController : Controller
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserInfo userInfo)
        {
            Session["CurrentUser"] = userInfo;
            return RedirectToAction("Index", "Home");
            //using (var connection = new SqlConnection(_connString))
            //{
            //    connection.Open();

            //    string query = @"SELECT * FROM [dbo].[EMails] WHERE [EMail] = @UserMail";

            //    using (var command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@UserMail", userInfo.UserMail);

            //        object result = command.ExecuteScalar();

            //        if (result != null)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("UserMail", "E-posta bulunamadı.");
            //            return View(userInfo);
            //        }
            //    }
            //}
        }
    }
}
