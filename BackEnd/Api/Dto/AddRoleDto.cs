
using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

public class AddRoleDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}
