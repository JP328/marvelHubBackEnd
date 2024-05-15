using marvelHub.Data;
using marvelHub.Model;
using Microsoft.EntityFrameworkCore;

namespace marvelHub.Service.Implements
{
    public class CommentService(AppDbContext context) : ICommentService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Comment>> GetByPost(Post post)
        {
            try
            {
                return await _context.Comments
                   .AsNoTracking()
                   .Include(c => c.Post)
                   .Where(c => c.Post.Id == post.Id)
                   .ToListAsync();
            }
            catch { return null; }
        }
       
        public async Task<IEnumerable<Comment>> GetByUserId(string userid)
        {
            try
            {
                 return await _context.Comments
                    .AsNoTracking()
                    .Include(c => c.Post)
                    .Where(c => c.UserId == userid)
                    .ToListAsync();
            }
            catch { return null; }
        }
        
        public async Task<Comment?> GetById(long id)
        {
            try
            {
                var Comment = await _context.Comments
                    .AsNoTracking()
                    .Include(c => c.Post)
                    .FirstAsync(c => c.Id == id);
                
                return Comment;
            }
            catch { return null; }
        }
        
        public async Task<Comment?> Create(Comment comment)
        {
            if (comment.Post is not null)
            {
                var CheckPost = await _context.Posts.FindAsync(comment.Post.Id);

                if (CheckPost is null)
                    return null;

                comment.Post = CheckPost;
            }

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
        public async Task<Comment?> Update(Comment comment)
        {
            var CommentUpdate = await _context.Posts.FindAsync(comment.Id);

            if (comment.Post is null)
                return null;

            if (comment.Post is not null)
            {
                var CheckPost = await _context.Posts.FindAsync(comment.Post.Id);

                if (CheckPost is null)
                    return null;

                comment.Post = CheckPost;
            }

            if (CommentUpdate is not null)
                _context.Entry(CommentUpdate).State = EntityState.Detached;
            
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return comment;
        }
        public async Task Delete(Comment comment)
        {
            _context.Remove(comment);
            await _context.SaveChangesAsync();
        }

    }
}
