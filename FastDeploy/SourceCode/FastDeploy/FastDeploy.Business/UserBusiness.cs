using FastDeploy.Business.Interfaces;
using FastDeploy.DataAccess.Interfaces;
using FastDeploy.Models;
using FastDeploy.Utilities;
using Newtonsoft.Json;
using NLog;

namespace FastDeploy.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDataAccess _dataAccess;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserBusiness(IUserDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        ResultData<UserModel> IUserBusiness.Login(UserLoginModel loginModel)
        {
            _logger.Trace("loginModel: {0}", JsonConvert.SerializeObject(loginModel));

            var rs = new ResultData<UserModel>() 
            { 
                Code = 0
            };

            if (loginModel == null)
            {
                rs.Code = -1500;
            }
            else if (string.IsNullOrEmpty(loginModel.Username))
            {
                rs.Code = -1501;
            }
            else {
                rs = _dataAccess.Login(loginModel);
            }

            _logger.Trace(rs.ToString());

            return rs;
        }
    }
}