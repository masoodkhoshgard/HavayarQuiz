using System.ComponentModel.DataAnnotations;

namespace HavayarQuiz.Domain.Enums;

public enum Roles
{
    [Display(Name = "Administrator")]
    Admin,
    [Display(Name = "Basic User")]
    BasicUser
}
