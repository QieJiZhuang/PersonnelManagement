using Newtonsoft.Json;
using Personnel_ManagementIServer;
using Personnel_ManagementModel;
using System.Data;
using System.Web.Http;

namespace Personnel_ManagementApi.Controllers
{

    public class ValuesController : ApiController
    {

        public IUserService userService { get; set; }


        [HttpGet]
        public string GetUserInfo(int id)   
        {
            ResponseModel responseModel = new ResponseModel();
            DataTable dt = userService.GetUserInfo(1);
            responseModel.Code = 200;
            responseModel.Data = JsonConvert.SerializeObject(dt);
            responseModel.Msg = "";
            return JsonConvert.SerializeObject(responseModel);
        }
    }


}
