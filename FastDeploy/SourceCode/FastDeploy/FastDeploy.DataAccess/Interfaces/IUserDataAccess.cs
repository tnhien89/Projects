using FastDeploy.Models;
using FastDeploy.Utilities;

namespace FastDeploy.DataAccess.Interfaces
{
    public interface IUserDataAccess
    {
        ResultData<UserModel> Login(UserLoginModel loginModel);
    }
}
