using HavayarQuiz.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HavayarQuiz.Web.Pages.HavayarUsers.Contracts;

public class HavayarUserUpdateViewModel 
{

    [Required]
    public Guid Id { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Birth Date")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Profile Picture")] public byte[]? ProfilePicture { get; set; }

    [Required]
    [Display(Name = "User Role")]
    public List<Roles> Roles { get; set; }
}
