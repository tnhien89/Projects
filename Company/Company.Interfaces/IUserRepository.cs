using Company.Models;

namespace Company.Interfaces
{
    public interface IUserRepository : IDataRepository<UserInfo>
    {
        ResultDTO<UserInfo> Get(string username, string password);
    }
}
