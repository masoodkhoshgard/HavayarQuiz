using System.ComponentModel.DataAnnotations;

namespace HavayarQuiz.Web.Pages.HavayarUsers.Contracts;

public record HavayarUserViewModel(Guid Id,
                                   [Display(Name = "Email")] string Email, [Display(Name = "UserName")] string UserName,
                                   [Display(Name = "First Name")] string FirstName,
                                   [Display(Name = "Last Name")] string LastName,
                                   [Display(Name = "Birth Date")] string BirthDate,
                                   [Display(Name = "Profile Picture")] byte[] ProfilePicture);
