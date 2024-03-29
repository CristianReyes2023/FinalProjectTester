namespace Domain.Entities;

public class Rol : BaseEntityInt
{
    public string Nombre { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRol> UsersRols { get; set; }
}
