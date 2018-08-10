using System;
using System.Collections.Generic;
using Company.Interfaces;
using Company.Models;
using NLog;

namespace Company.DataAccess
{
    public class OptionRepository : IOptionRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<List<Option>> Get(string optionType)
        {
            _log.Debug("optionType: {0}", optionType);

            ResultDTO<List<Option>> rs = DataProvider.ExcuteStoredReturnCollection<Option>("usp_Options_Get", new { OptionType = optionType });
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<int> InsertOrUpdate(Option obj)
        {
            _log.Debug("Name: {0} - Value: {1}", obj.Name, obj.Value);

            ResultDTO<int> rs = DataProvider.ExecuteStored("usp_Options_Insert_Update", obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
