namespace marvelHub.Model;

public class UserLogin
{
    public long Id {  get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Photo { get; set; } = string.Empty;

    public string Token {  get; set; } = string.Empty;
}
