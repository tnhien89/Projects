using Company.DataAccess;
using Company.Models;
using Company.Interfaces;
using System.Collections.Generic;

namespace Company.BusinessLogic
{
    public class OptionService
    {
        IOptionRepository _repo;

        public OptionService()
        {
            _repo = new OptionRepository();
        }

        public ResultDTO<List<Option>> Get(string optionType)
        {
            return _repo.Get(optionType);
        }

        public ResultDTO<int> InsertOrUpdate(Option obj)
        {
            return _repo.InsertOrUpdate(obj);
        }
    }
}
