using Newtonsoft.Json;
using Personnel_ManagementIServer;
using Personnel_ManagementModel;
using Personnel_ManagementServer.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Personnel_ManagementApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.aa = GetUserInfo(1);
            return View();
        }

        public IUserService userService { get; set; }


        public string GetUserInfo(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            DataTable dt = userService.GetUserInfo(id);
            responseModel.Code = 200;
            responseModel.Data = JsonConvert.SerializeObject(dt);
            responseModel.Msg = "";

            return JsonConvert.SerializeObject(responseModel);
        }
    }
}
