using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models;

public class Player
{
    [Required]
    [MinLength(5, ErrorMessage ="MinLength = 5")]
    public string Login { get; set; }= string.Empty;

    [Required]
    [EmailAddress(ErrorMessage ="Email is invalid")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(16,120, ErrorMessage = "Age is not 16-120")]
    public ushort Age { get; set; } 
}
