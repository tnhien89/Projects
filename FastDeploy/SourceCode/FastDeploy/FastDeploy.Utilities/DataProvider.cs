using FastDeploy.Utilities.Extensions;
using FastDeploy.Utilities.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FastDeploy.Utilities
{
    public class DataProvider : IDataProvider
    {
        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;
        private readonly int _commandTimeout = 36000;

        public DataProvider(IConfiguration configuration)
        { 
            _configuration = configuration;
            //--
            if (_configuration != null)
            {
                _connectionString = _configuration.GetConnectionString("DbConnectionDefault");

                if (int.TryParse(_configuration["AppSettings:CommandTimeout"], out int val) && val > 0)
                {
                    _commandTimeout = val;
                }
            }
        }

        private SqlCommand CreateSqlCommandQuery(SqlConnection conn, string query, Hashtable hashTable, CommandType commandType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = new(query, conn)
                {
                    CommandType = commandType,
                    CommandTimeout = _commandTimeout
                };

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

                    SqlParameter para = new(key, value);
                    cmd.Parameters.Add(para);
                }

                return cmd;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SqlCommand CreateSqlCommandStored(SqlConnection conn, string stored, Hashtable hashTable)
        {
            try
            {
                SqlCommand cmd = new(stored, conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = _commandTimeout
                };

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
                    object value = entry.Value ?? DBNull.Value;

                    SqlParameter para = new(key, value);
                    cmd.Parameters.Add(para);
                }

                return cmd;
            }
            catch (Exception)
            {
                throw;
            }
        }

        ResultData<List<T>> IDataProvider.ExcuteStoredReturnCollection<T>(string stored, object obj)
        {
            SqlConnection conn = new(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);

                ResultData<List<T>> result = new()
                {
                    Code = (int)dbReturn.Value
                };

                if (ds != null && ds.Tables.Count > 0)
                {
                    result.Data = ds.Tables[0].ToList<T>();
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultData<List<T>>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
                };
            }
            finally
            {
                cmd?.Dispose();
                conn?.Close();
            }
        }

        ResultData<ArrayList> IDataProvider.ExcuteStoredReturnCollection(string stored, object obj, Type[] types)
        {
            SqlConnection conn = new(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);

                ResultData<ArrayList> result = new()
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<T> IDataProvider.ExcuteStoredReturnObject<T>(string stored, object obj)
        {
            SqlConnection conn = new(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);

                ResultData<T> result = new()
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<T> IDataProvider.ExcuteStoredReturnObject<T>(string stored, Hashtable hashTable)
        {
            SqlConnection conn = new(_connectionString);
            SqlCommand cmd = CreateSqlCommandStored(conn, stored, hashTable);

            SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
            dbReturn.Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);

                ResultData<T> result = new()
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<DataSet> IDataProvider.ExecuteQuery(string query)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = new SqlCommand(query, conn)
                {
                    CommandTimeout = _commandTimeout
                };
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<DataSet> IDataProvider.ExecuteQuery(string query, object obj, CommandType commandType)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters(), commandType);
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<DataSet> IDataProvider.ExecuteQuery(string query, Hashtable hashTable)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, hashTable);
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<List<T>> IDataProvider.ExecuteQueryReturnCollection<T>(string query)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = new SqlCommand(query, conn)
                {
                    CommandTimeout = _commandTimeout
                };
                conn.Open();

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);
                da.Dispose();

                ResultData<List<T>> result = new()
                {
                    Code = (int)dbReturn.Value
                };

                if (ds != null && ds.Tables.Count > 0)
                {
                    result.Data = ds.Tables[0].ToList<T>();
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultData<List<T>>()
                {
                    Code = int.MinValue,
                    Message = ex.Message,
                    Error = ex.ToString()
                };
            }
            finally
            {
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<T> IDataProvider.ExecuteQueryReturnObject<T>(string query, object obj)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;
            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandQuery(conn, query, obj.GetParameters());
                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
                da.Fill(ds);

                ResultData<T> result = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<object> IDataProvider.ExecuteScalar(string query)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = new SqlCommand(query, conn)
                {
                    CommandTimeout = _commandTimeout
                };
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<object> IDataProvider.ExecuteScalar(string query, Hashtable hashTable)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<int> IDataProvider.ExecuteStored(string stored, Hashtable hashTable)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<int> IDataProvider.ExecuteStored(string stored, object obj)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<DataSet> IDataProvider.ExecuteStoredReturnDataSet(string stored, Hashtable hashTable)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, hashTable);

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }

        ResultData<DataSet> IDataProvider.ExecuteStoredReturnDataSet(string stored, object obj)
        {
            SqlConnection? conn = null;
            SqlCommand? cmd = null;

            try
            {
                conn = new SqlConnection(_connectionString);
                cmd = CreateSqlCommandStored(conn, stored, obj.GetParameters());

                SqlParameter dbReturn = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                dbReturn.Direction = ParameterDirection.ReturnValue;

                conn.Open();

                SqlDataAdapter da = new(cmd);
                DataSet ds = new();
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
                cmd?.Dispose();

                conn?.Close();
            }
        }
    }
}