using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Company.Models;
using Company.Extensions;
using System.Configuration;

namespace Company.DataAccess
{
    public class DataProvider
    {
        private static int _commandTimeout = 36000;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private static string _connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        private static SqlCommand CreateSqlCommandQuery(SqlConnection conn, string query, Hashtable hashTable)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = _commandTimeout;

                if (hashTable == null || hashTable.Count == 0)
                {
                    return cmd;
                }

                foreach (DictionaryEntry entry in hashTable)
                {
                    if (entry.Key == null || string.IsNullOrEmpty(entry.Key.ToString()))
                    {
                        continue;
                    }

                    string key = "@" + entry.Key.ToString();
                    object value = entry.Value == null ? DBNull.Value : entry.Value;

                    SqlParameter para = new SqlParameter(key, value);
                    cmd.Parameters.Add(para);
                }

                return cmd;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return null;
            }
        }

        private static SqlCommand CreateSqlCommandStored(SqlConnection conn, string stored, Hashtable hashTable)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(stored, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = _commandTimeout;

                if (hashTable == null || hashTable.Count == 0)
                {
                    return cmd;
                }

                foreach (DictionaryEntry entry in hashTable)
                {
                    if (entry.Key == null || string.IsNullOrEmpty(entry.Key.ToString()))
                    {
                        continue;
                    }

                    string key = "@" + entry.Key.ToString();
                    object value = entry.Value == null ? DBNull.Value : entry.Value;

                    SqlParameter para = new SqlParameter(key, value);
                    cmd.Parameters.Add(para);
                }

                return cmd;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return null;
            }
        }

        public static ResultDTO<DataSet> ExecuteQuery(string query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = _commandTimeout;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();

                return new ResultDTO<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<DataSet> ExecuteQuery(string query, object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters());
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultDTO<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<T> ExecuteQueryReturnObject<T>(string query, object obj) where T: new()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters());
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultDTO<T> result = new ResultDTO<T>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result.Code = 0;
                    result.Data = ds.Tables[0].Rows[0].ToObject<T>();
                }
                else
                {
                    result.Code = -1404;
                    result.Message = "Data not found.";
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<T>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<DataSet> ExecuteQuery(string query, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, hashTable);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultDTO<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<object> ExecuteScalar(string query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = _commandTimeout;
                conn.Open();

                object result = cmd.ExecuteScalar();

                return new ResultDTO<object>()
                {
                    Code = 0,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<object>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<object> ExecuteScalar(string query, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, hashTable);

                conn.Open();

                object result = cmd.ExecuteScalar();

                return new ResultDTO<object>()
                {
                    Code = 0,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<object>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<int> ExecuteStored(string stored, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, hashTable);

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                int exec = cmd.ExecuteNonQuery();

                return new ResultDTO<int>()
                {
                    Code = (int)dbReturn.Value,
                    Data = (int)dbReturn.Value
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<int>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<int> ExecuteStored(string stored, object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                int exec = cmd.ExecuteNonQuery();

                return new ResultDTO<int>()
                {
                    Code = (int)dbReturn.Value,
                    Data = (int)dbReturn.Value
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<int>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<long> ExecuteStoredBigId(string stored, object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.BigInt);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                int exec = cmd.ExecuteNonQuery();

                return new ResultDTO<long>()
                {
                    Code = 0,
                    //Code = (long)dbReturn.Value,
                    Data = (long)dbReturn.Value
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<long>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<DataSet> ExecuteStoredReturnDataSet(string stored, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, hashTable == null ? new Hashtable() : hashTable);

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultDTO<DataSet>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<DataSet> ExecuteStoredReturnDataSet(string stored, Object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, obj == null ? new Hashtable() : obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultDTO<DataSet>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());

                return new ResultDTO<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static ResultDTO<T> ExcuteStoredReturnObject<T>(string stored, object obj) where T: new()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                
                ResultDTO<T> result = new ResultDTO<T>()
                {
                    Code = (int)dbReturn.Value
                };

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result.Data = ds.Tables[0].Rows[0].ToObject<T>();
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultDTO<T>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public static ResultDTO<List<T>> ExcuteStoredReturnCollection<T>(string stored, object obj) where T: new()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultDTO<List<T>> result = new ResultDTO<List<T>>() { 
                    Code = (int)dbReturn.Value
                };

                if (ds != null && ds.Tables.Count > 0)
                {
                    result.Data = (List<T>)ds.Tables[0].ToList<T>();
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultDTO<List<T>>() { 
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public static ResultDTO<ArrayList> ExcuteStoredReturnCollection(string stored, object obj, Type[] types)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultDTO<ArrayList> result = new ResultDTO<ArrayList>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds.ToArrayList(types)
                };

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultDTO<ArrayList>()
                {
                    Code = int.MinValue,
                    Message = ex.Message
                };
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
