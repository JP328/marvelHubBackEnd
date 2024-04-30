using marvelHub.Model;

namespace marvelHub.Service
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetByPost(Post post);
        Task<Comment?> GetById(long id);
        Task<IEnumerable<Comment>> GetByUserId(string userid);
        Task<Comment?> Create(Comment comment);
        Task<Comment?> Update(Comment comment);
        Task Delete(Comment comment);
    }
}
