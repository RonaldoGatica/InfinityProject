using System.Net.Http.Json;

namespace Infinity.UI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public AuthService(HttpClient http, IConfiguration config)
        {
            this._http = http;
            _config = config;
        }

        public async Task<ServiceResponse<ResponseAuthentication>> Register(UserCredentials request)
        {
            var result = await _http.PostAsJsonAsync(_config.GetSection("settings")["apiURL"] + "/api/accounts/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<ResponseAuthentication>>();
        }

        public async Task<ServiceResponse<ResponseAuthentication>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync(_config.GetSection("settings")["apiURL"] + "/api/accounts/login", request);//config.GetSection("Services")["apiURL"]
            return await result.Content.ReadFromJsonAsync<ServiceResponse<ResponseAuthentication>>();
        }
    }
}
