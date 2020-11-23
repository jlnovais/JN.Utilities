using JN.Utilities.Core.Dto;
using JN.Utilities.Core.Entities;

namespace JN.Utilities.Core.Services
{
    public interface IUsersService
    {
        Result<User> GetUser(string username, string password);
    }
}