using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BackEnd.DataAccess
{
    public class UsersDAL
    {
        public static ResultData<UserProfile> Login(string username, string passowrd)
        {
            try
            {
                LogHelpers.LogHandler.Info(string.Format("username: {0}, password: {1}", username, passowrd));
                var stored = "sp_Users_Login";
                object obj = new { 
                    Username = username.Trim(),
                    Password = Utilities.ToMD5Hash(passowrd.Trim())
                };

                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
                if (result.Code < 0)
                {
                    return new ResultData<UserProfile>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                if (result.Data == null || result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    return new ResultData<UserProfile>() { 
                        Code = -104,
                        ErrorMessage = "Wrong username or password"
                    };
                }

                UserProfile user = new UserProfile(result.Data.Tables[0].Rows[0]);
                HttpContext.Current.Session["LoggedUser"] = user;

                return new ResultData<UserProfile>() { 
                    Code = 0,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<UserProfile>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<List<UserProfile>> GetAll()
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                string query = "select * from vw_Users";
                var result = DataAccessHelpers.ExecuteQuery(query);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<List<UserProfile>>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                List<UserProfile> list = new List<UserProfile>();
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    UserProfile user = new UserProfile(row);
                    list.Add(user);
                }

                return new ResultData<List<UserProfile>>() { 
                    Code = 0,
                    Data = list
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<List<UserProfile>>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<UserProfile> Get(int userId)
        {
            try
            {
                LogHelpers.LogHandler.Info("userId: " + userId);
                string stored = "sp_Users_Get";
                object obj = new { 
                    Id = userId
                };

                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<UserProfile>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                if (result.Data == null || result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    return new ResultData<UserProfile>() { 
                        Code = -104,
                        ErrorMessage = "Not found"
                    };
                }

                return new ResultData<UserProfile>() { 
                    Code = 0,
                    Data = new UserProfile(result.Data.Tables[0].Rows[0])
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<UserProfile>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<int> Update(UserProfile user)
        {
            try
            {
                LogHelpers.LogHandler.Info("Id: " + user.Id);
                user.Password = Utilities.ToMD5Hash(user.Password.Trim());
                user.DateOfBirth = Utilities.ParseToDate(user.DateOfBirthString);

                var stored = "sp_Users_Insert_Update";
                var result = DataAccessHelpers.ExecuteStored(stored, user.GetParameters());

                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<int>()
                {
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}