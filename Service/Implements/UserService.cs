using marvelHub.Data;
using marvelHub.Model;
using Microsoft.EntityFrameworkCore;

namespace marvelHub.Service.Implements;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users
            .Include(u => u.Post)
            .ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        var searchUser = await _context.Users
            .Include(u => u.Post)
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        return searchUser;
    }

    public async Task<User?> GetById(long id)
    {
        try
        {
            var User = await _context.Users
                .Include(u => u.Post)
                .FirstAsync(u => u.Id == id);]

            User.Password = "";

            return User;

        } catch { return null; }
    }

    public async Task<User?> Create(User user)
    {
        var SearchUser = await GetById(user.Id);

        if (SearchUser is not null)
            return null;

        if (user.Photo is null || user.Photo == "")
            user.Photo = "https://i.imgur.com/I8MfmC8.png";

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 13);

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> Update(User user)
    {
        var UserUpdate = await _context.Users.FindAsync(user.Id);

        if (user.Photo is null || user.Photo == "") 
            user.Photo = "https://i.imgur.com/I8MfmC8.png";

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 13);

        _context.Entry(UserUpdate).State = EntityState.Detached;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return user;
    }
}
