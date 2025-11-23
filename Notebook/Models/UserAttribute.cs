using System.ComponentModel.DataAnnotations;

namespace Notebook.Models;

public class UserAttribute
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }          

    [Required]
    [StringLength(100)]
    public string Key { get; set; } = string.Empty;    

    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty; 

    
    public User User { get; set; } = null!;
}