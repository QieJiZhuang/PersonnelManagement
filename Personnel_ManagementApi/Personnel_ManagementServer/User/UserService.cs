using DTcms.DBUtility;
using Personnel_ManagementIServer;
using Personnel_ManagementModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traffic.Utility;
using 存储过程;

namespace Personnel_ManagementServer.User
{
    public class UserService : IUserService
    {
        public int AddUser(User_Entity user_Entity)
        {
            return 1;
        }

        public DataTable GetUserInfo(int id)
        {
            string strsql = "select * from dbo.TUser";
            DataTable dt = DbHelperSQL.GetDataTable(strsql);
            return dt;
        }
    }
}
