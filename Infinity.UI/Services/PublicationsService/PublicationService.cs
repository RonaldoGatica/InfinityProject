using ClassLibraryInfinity.Entities;
using System.Net.Http.Json;

namespace Infinity.UI.Services.PublicationsService
{
    public class PublicationService : IPublicationService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public PublicationService(HttpClient http, IConfiguration config)
        {
            this._http = http;
            _config = config;
        }

        public async Task<Publication> GetPubli(int id)
        {
            Publication publication =  await _http.GetFromJsonAsync<Publication>(_config.GetSection("settings")["apiURL"] + "/api/Publications/" + id);
            return publication;
        }

        public async  Task<List<Publication>> GetPublications()
        {
            return await _http.GetFromJsonAsync<List<Publication>>(_config.GetSection("settings")["apiURL"] + "/api/Publications");
        }

        public async Task create(Publication publication)
        {
            await _http.PostAsJsonAsync(_config.GetSection("settings")["apiURL"] + "/api/Publications", publication);
        }

        public async Task delete(int id)
        {
            await _http.DeleteAsync(_config.GetSection("settings")["apiURL"] + "/api/Publications/"+ id);
        }
    }
}
