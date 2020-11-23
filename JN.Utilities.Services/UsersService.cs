using System.Collections.Generic;
using System.Linq;
using JN.Utilities.Core.Dto;
using JN.Utilities.Core.Entities;
using JN.Utilities.Core.Services;

namespace JN.Utilities.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEnumerable<User> _users;

        public UsersService(IEnumerable<User> users)
        {
            _users = users;
        }

        public Result<User> GetUser(string username, string password)
        {
            if (_users == null)
                return new Result<User>()
                {
                    Success = false,
                    ErrorCode = -1
                };

            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password /*&& x.Active*/);

            return new Result<User>()
            {
                Success = user != null,
                ErrorCode = user != null ? 0 : -1,
                ReturnedObject = user
            };
        }
    }
}
