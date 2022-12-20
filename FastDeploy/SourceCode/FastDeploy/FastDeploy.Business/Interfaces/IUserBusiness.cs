using FastDeploy.Models;
using FastDeploy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDeploy.Business.Interfaces
{
    public interface IUserBusiness
    {
        ResultData<UserModel> Login(UserLoginModel loginModel);
    }
}
