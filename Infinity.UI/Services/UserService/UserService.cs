using ClassLibraryInfinity.Entities;
using System.Net.Http.Json;

namespace Infinity.UI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public UserService(HttpClient http, IConfiguration config)
        {
            this._http = http;
            _config = config;
        }

        public async Task<Userweb> GetUser(string user)
        {
            return await _http.GetFromJsonAsync<Userweb>(_config.GetSection("settings")["apiURL"] + "/api/Userwebs/" + user);

        }
    }
}
