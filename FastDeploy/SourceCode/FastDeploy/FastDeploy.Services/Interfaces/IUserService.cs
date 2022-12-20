using FastDeploy.Models;
using FastDeploy.Utilities;

namespace FastDeploy.Services.Interfaces
{
    public interface IUserService
    {
        ResultData<UserModel> Login(UserLoginModel loginModel);
    }
}
