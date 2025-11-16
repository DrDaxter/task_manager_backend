namespace taskManagerBE.Dto;

public class UserDto
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHas { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Rol { get; set; } = null!;
}