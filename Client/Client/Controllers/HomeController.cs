using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Models;
using Microsoft.AspNet.SignalR;
using Server.Hubs;
using System.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data.SqlClient;
namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly NotificationHub _notHub;
        private  DatabaseListener _databaseListener;
        public HomeController()
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] != null)
            {
                 UserInfo userInfo = (UserInfo)System.Web.HttpContext.Current.Session["CurrentUser"];
                //_databaseListener = new DatabaseListener(ConnectionManager.GetConnections(System.Web.HttpContext.Current.Session["ConnectionId"].ToString()));
                _databaseListener = userInfo.DatabaseListener;
            }
        }
        public ActionResult Index()
        {
            return View();
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
        public ActionResult ShowConnectedUsers()
        {
            return View();
        }
        public ActionResult GetMessages()
        {
            if (_databaseListener != null)
            {
                var messages = _databaseListener.GetAllMessages().Select(m => new Client.Models.Messages { Message = m.Message });
                return PartialView("_MessagesList", messages);
            }
            return PartialView("MessagesList", null);
        }
        [HttpPost]
        public JsonResult AddConnectionId(string id) 
        {
            _databaseListener = new DatabaseListener(ConnectionManager.GetConnections(id));
            //Session["CurrentUser"] = id;
            UserInfo userInfo = (UserInfo)Session["CurrentUser"];
            userInfo.ConnectionId = id;
            userInfo.DatabaseListener = _databaseListener;
            Session["CurrentUser"]=userInfo;
            AddEmailToDb(userInfo.UserMail, id); 
            return new JsonResult() {Data = "success"};
        }
        public void AddEmailToDb(string email,string connectionıd)
        {
            string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString ;
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"INSERT INTO EMails (EMail,ConnectionId) VALUES (@Email, @ConnectionId)", connection))
                {
                    command.Parameters.AddWithValue("@Email",email );
                    command.Parameters.AddWithValue("@ConnectionId",connectionıd );
                    command.ExecuteNonQuery();                
                }
            }
        }
    }
}