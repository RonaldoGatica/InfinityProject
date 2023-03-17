using ClassLibraryInfinity.Entities;

namespace Infinity.UI.Services.UserService
{
    public interface IUserService
    {
        Task<Userweb> GetUser(string user);
    }
}
