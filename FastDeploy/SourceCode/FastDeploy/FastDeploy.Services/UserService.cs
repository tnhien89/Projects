using FastDeploy.Business.Interfaces;
using FastDeploy.Models;
using FastDeploy.Services.Interfaces;
using FastDeploy.Utilities;
using Newtonsoft.Json;
using NLog;

namespace FastDeploy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserBusiness _business;
        private readonly Logger _logger;

        public UserService(IUserBusiness userBusiness)
        {
            _business = userBusiness;
            _logger = LogManager.GetCurrentClassLogger();
        }

        ResultData<UserModel> IUserService.Login(UserLoginModel loginModel)
        {
            _logger.Trace("loginModel: {0}", JsonConvert.SerializeObject(loginModel));

            return _business.Login(loginModel);
        }
    }
}