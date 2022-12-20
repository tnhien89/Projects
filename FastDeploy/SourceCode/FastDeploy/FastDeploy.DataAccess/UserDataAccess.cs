using FastDeploy.DataAccess.Interfaces;
using FastDeploy.Models;
using FastDeploy.Utilities;
using FastDeploy.Utilities.Interfaces;
using Newtonsoft.Json;
using NLog;

namespace FastDeploy.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDataProvider _dataProvider;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserDataAccess(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        ResultData<UserModel> IUserDataAccess.Login(UserLoginModel loginModel)
        {
            _logger.Trace("loginModel: {0}", JsonConvert.SerializeObject(loginModel));
            //---
            var rs = _dataProvider.ExcuteStoredReturnObject<UserModel>("usp_UserLogin", loginModel);
            if (rs.Code < 0)
            {
                _logger.Error(rs.ToString());
            }

            return rs;
        }
    }
}