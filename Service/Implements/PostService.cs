using marvelHub.Data;
using marvelHub.Model;
using Microsoft.EntityFrameworkCore;

namespace marvelHub.Service.Implements
{
    public class PostService(AppDbContext context) : IPostService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Theme)
                .ToListAsync();
        }

        public async Task<Post?> GetById(long id)
        {
            try
            {
                var Post = await _context.Posts
                    .AsNoTracking()
                    .Include(p => p.User)
                    .Include(p => p.Theme)
                    .FirstAsync(i => i.Id == id);

                return Post;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Post>> GetByTitle(string title)
        {
            var Post = await _context.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Theme)
                .Where(p => p.Title
                    .Contains(title))
                .ToListAsync();

            return Post;
        }

        public async Task<Post?> Create(Post post)
        {
            if (post.Theme is not null)
            {
                var CheckTheme = await _context.Themes.FindAsync(post.Theme.Id);

                if (CheckTheme is null)
                    return null;

                post.Theme = CheckTheme;
            }

            post.User = post.User is not null ?
                await _context.Users.FirstOrDefaultAsync(p => p.Id == post.User.Id)
                : null;

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> Update(Post post)
        {
            var PostUpdate = await _context.Posts.FindAsync(post.Id);

            if (post.Theme is null)
                return null;

            if(post.Theme is not null)
            {
                var CheckTheme = await _context.Themes.FindAsync(post.Theme.Id);

                if(CheckTheme is null) 
                    return null;

                post.Theme = CheckTheme;
            }
            
            if (PostUpdate is not null)
                _context.Entry(PostUpdate).State = EntityState.Detached;
            
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task Delete(Post post)
        {
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}

       