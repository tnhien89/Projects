using DotNetLibrary.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using DotNetLibrary.Interfaces;

namespace DotNetLibrary.DataAccess
{
    public class DataProvider : IDataProvider
    {
        private string _connectionString;
        private int _commandTimeout = 36000;

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string ConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _connectionString;
        }

        private SqlCommand CreateSqlCommandQuery(SqlConnection conn, string query, Hashtable hashTable)
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
                throw ex;
            }
        }

        private SqlCommand CreateSqlCommandStored(SqlConnection conn, string stored, Hashtable hashTable)
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
                throw ex;
            }
        }

        public ResultData<DataSet> ExecuteQuery(string query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = _commandTimeout;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();

                return new ResultData<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                return new ResultData<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString(),
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

        public ResultData<DataSet> ExecuteQuery(string query, object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters());
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultData<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                return new ResultData<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString(),
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

        public ResultData<T> ExecuteQueryReturnObject<T>(string query, object obj) where T: new()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters());
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultData<T> result = new ResultData<T>();
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
                return new ResultData<T>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<DataSet> ExecuteQuery(string query, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandQuery(conn, query, hashTable);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultData<DataSet>()
                {
                    Code = 0,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                return new ResultData<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString(),
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

        public ResultData<object> ExecuteScalar(string query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = _commandTimeout;
                conn.Open();

                object result = cmd.ExecuteScalar();

                return new ResultData<object>()
                {
                    Code = 0,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultData<object>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString(),
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

        public ResultData<object> ExecuteScalar(string query, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandQuery(conn, query, hashTable);

                conn.Open();

                object result = cmd.ExecuteScalar();

                return new ResultData<object>()
                {
                    Code = 0,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultData<object>()
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

        public ResultData<int> ExecuteStored(string stored, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandStored(conn, stored, hashTable);

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                int exec = cmd.ExecuteNonQuery();

                return new ResultData<int>()
                {
                    Code = (int)dbReturn.Value,
                    Data = (int)dbReturn.Value
                };
            }
            catch (Exception ex)
            {
                return new ResultData<int>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<int> ExecuteStored(string stored, object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                int exec = cmd.ExecuteNonQuery();

                return new ResultData<int>()
                {
                    Code = (int)dbReturn.Value,
                    Data = (int)dbReturn.Value
                };
            }
            catch (Exception ex)
            {
                return new ResultData<int>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<DataSet> ExecuteStoredReturnDataSet(string stored, Hashtable hashTable)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandStored(conn, stored, hashTable);

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultData<DataSet>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                return new ResultData<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<DataSet> ExecuteStoredReturnDataSet(string stored, Object obj)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(ConnectionString());
                cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return new ResultData<DataSet>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds
                };
            }
            catch (Exception ex)
            {
                return new ResultData<DataSet>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<T> ExcuteStoredReturnObject<T>(string stored, object obj) where T: new()
        {
            SqlConnection conn = new SqlConnection(ConnectionString());
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                
                ResultData<T> result = new ResultData<T>()
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
                return new ResultData<T>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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

        public ResultData<T> ExcuteStoredReturnObject<T>(string stored, Hashtable hashTable) where T : new()
        {
            SqlConnection conn = new SqlConnection(ConnectionString());
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, hashTable);

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultData<T> result = new ResultData<T>()
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
                return new ResultData<T>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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
        public ResultData<List<T>> ExcuteStoredReturnCollection<T>(string stored, object obj) where T: new()
        {
            SqlConnection conn = new SqlConnection(ConnectionString());
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultData<List<T>> result = new ResultData<List<T>>() { 
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
                return new ResultData<List<T>>() { 
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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
        public ResultData<ArrayList> ExcuteStoredReturnCollection(string stored, object obj, Type[] types)
        {
            SqlConnection conn = new SqlConnection(ConnectionString());
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ResultData<ArrayList> result = new ResultData<ArrayList>()
                {
                    Code = (int)dbReturn.Value,
                    Data = ds.ToArrayList(types)
                };

                return result;
            }
            catch (Exception ex)
            {
                return new ResultData<ArrayList>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
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
