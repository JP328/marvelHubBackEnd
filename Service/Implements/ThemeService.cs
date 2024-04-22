using marvelHub.Data;
using marvelHub.Model;
using Microsoft.EntityFrameworkCore;

namespace marvelHub.Service.Implements;

public class ThemeService : IThemeService
{
    private readonly AppDbContext _context;

    public ThemeService(AppDbContext context)
    {
        _context = context;
    }

    public  async Task<IEnumerable<Theme>> GetAll()
    {
        return await _context.Themes
            .Include(t => t.Post)
            .ToListAsync();
    }

    public async Task<Theme?> GetById(int id)
    {
        try
        {
            var Theme = await _context.Themes
                .Include(t => t.Post)
                .FirstAsync(t => t.Id == id);
            return Theme;

        } catch { return null; }
    }

    public async Task<IEnumerable<Theme>> GetByType(string type)
    {
        var Theme = await _context.Themes
            .Include(t => t.Post)
            .Where(t => t.Type.ToUpper().Equals(type.ToUpper()))
            .ToListAsync();

        return Theme;
    }

    public async Task<Theme?> Create(Theme theme)
    {
        await _context.Themes.AddAsync(theme);
        await _context.SaveChangesAsync();

        return theme;
    }

    public async Task<Theme?> Update(Theme theme)
    {
        var ThemeUpdate = await _context.Themes.FindAsync(theme.Id);

        if (ThemeUpdate is null)
            return null;

        _context.Entry(ThemeUpdate).State = EntityState.Detached;
        _context.Entry(theme).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return theme;
    }

    public async Task Delete(Theme theme)
    {
        _context.Remove(theme);
        await _context.SaveChangesAsync();
    }

}
