using Personnel_ManagementModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementIServer
{
    public interface IUserService: IServiceSupport
    {
        int AddUser(User_Entity user_Entity);

        DataTable GetUserInfo(int id);
    }
}
