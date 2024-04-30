using marvelHub.Model;

namespace marvelHub.Service;

public interface IThemeService
{
    Task<IEnumerable<Theme>> GetAll();

    Task<Theme?> GetById(int id);

    Task<IEnumerable<Theme>> GetByType(string type);

    Task<Theme?> Create(Theme theme);

    Task<Theme?> Update(Theme theme);

    Task Delete(Theme theme);
}