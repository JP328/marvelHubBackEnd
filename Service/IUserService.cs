using marvelHub.Model;

namespace marvelHub.Service;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();

    Task<User?> GetById(long id);

    Task<User?> GetByEmail(string email);

    Task<User?> Create(User user);

    Task<User?> Update(User user);

    Task Delete(User user);
}
