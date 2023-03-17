using ClassLibraryInfinity.Entities;

namespace Infinity.UI.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<Comment>> GetPubli(int id);

        Task<List<Comment>> GetPublications();

        Task create(Comment publication);

        Task delete(int id);
    }
}
