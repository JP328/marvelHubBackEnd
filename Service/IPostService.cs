using marvelHub.Model;

namespace marvelHub.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();

        Task<Post?> GetById(long id);

        Task<IEnumerable<Post>> GetByTitle(string title);

        Task<Post?> Create(Post post);

        Task<Post?> Update(Post post);

        Task Delete(Post post);
    }
}
