using System.ComponentModel.DataAnnotations;
using Notebook.Models;

namespace Notebook.ViewModels;

public class UserEditViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    [StringLength(50, MinimumLength = 2)]
    [Display(Name = "Imię")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [StringLength(150, MinimumLength = 2)]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data urodzenia jest wymagana")]
    [DataType(DataType.Date)]
    [Display(Name = "Data urodzenia")]
    public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-20);

    [Required(ErrorMessage = "Płeć jest wymagana")]
    [Display(Name = "Płeć")]
    public Gender Gender { get; set; }

    
    public List<UserAttributeViewModel> Attributes { get; set; } = new()
    {
        new UserAttributeViewModel()
    };
}

public class UserAttributeViewModel
{
    [Required(ErrorMessage = "Nazwa atrybutu jest wymagana")]
    [StringLength(100)]
    public string Key { get; set; } = string.Empty;

    [Required(ErrorMessage = "Wartość jest wymagana")]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;
}