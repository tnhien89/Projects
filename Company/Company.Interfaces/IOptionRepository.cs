using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Interfaces
{
    public interface IOptionRepository
    {
        ResultDTO<List<Option>> Get(string optionType);
        ResultDTO<int> InsertOrUpdate(Option obj);
    }
}
