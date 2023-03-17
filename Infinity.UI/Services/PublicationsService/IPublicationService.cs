using ClassLibraryInfinity.Entities;

namespace Infinity.UI.Services.PublicationsService
{
    public interface IPublicationService
    {
        Task<Publication> GetPubli(int id);

        Task<List<Publication>> GetPublications();

        Task create(Publication publication);

        Task delete(int id);
    }
}
