using Company.DataAccess;
using Company.Interfaces;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BusinessLogic
{
    public class UserService
    {
        private IUserRepository _repo;

        public UserService()
        {
            this._repo = new UserRepository();
        }

        public ResultDTO<UserInfo> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new ResultDTO<UserInfo>() {
                    Code = -1500
                };
            }

            return _repo.Get(username, password);
        }
    }
}
