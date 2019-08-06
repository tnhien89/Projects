using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace DotNetLibrary.Interfaces
{
    public interface IDataProvider
    {
        void SetConnectionString(string connectionString);
        ResultData<DataSet> ExecuteQuery(string query);
        ResultData<DataSet> ExecuteQuery(string query, object obj);
        ResultData<T> ExecuteQueryReturnObject<T>(string query, object obj) where T : new();
        ResultData<DataSet> ExecuteQuery(string query, Hashtable hashTable);
        ResultData<object> ExecuteScalar(string query);
        ResultData<object> ExecuteScalar(string query, Hashtable hashTable);
        ResultData<int> ExecuteStored(string stored, Hashtable hashTable);
        ResultData<int> ExecuteStored(string stored, object obj);
        ResultData<DataSet> ExecuteStoredReturnDataSet(string stored, Hashtable hashTable);
        ResultData<DataSet> ExecuteStoredReturnDataSet(string stored, Object obj);
        ResultData<T> ExcuteStoredReturnObject<T>(string stored, object obj) where T : new();
        ResultData<T> ExcuteStoredReturnObject<T>(string stored, Hashtable hashTable) where T : new();
        ResultData<List<T>> ExcuteStoredReturnCollection<T>(string stored, object obj) where T : new();
        ResultData<ArrayList> ExcuteStoredReturnCollection(string stored, object obj, Type[] types);
    }
}
