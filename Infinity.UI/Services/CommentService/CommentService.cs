using ClassLibraryInfinity.Entities;
using System.Net.Http.Json;

namespace Infinity.UI.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public CommentService(HttpClient http, IConfiguration config)
        {
            this._http = http;
            _config = config;
        }

        public async Task<List<Comment>> GetPubli(int id)
        {
            return await _http.GetFromJsonAsync<List<Comment>>(_config.GetSection("settings")["apiURL"] + "/more/" + id);
        }

        public async Task<List<Comment>> GetPublications()
        {
            return await _http.GetFromJsonAsync<List<Comment>>(_config.GetSection("settings")["apiURL"] + "/api/Comments");
        }

        public async Task create(Comment publication)
        {
            await _http.PostAsJsonAsync(_config.GetSection("settings")["apiURL"] + "/api/Comments", publication);
        }

        public async Task delete(int id)
        {
            await _http.DeleteAsync(_config.GetSection("settings")["apiURL"] + "/api/Comments/" + id);
        }
    }
}
