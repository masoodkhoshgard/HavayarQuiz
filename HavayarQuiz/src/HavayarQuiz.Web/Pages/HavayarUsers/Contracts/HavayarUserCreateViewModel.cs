﻿using HavayarQuiz.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HavayarQuiz.Web.Pages.HavayarUsers.Contracts;

public class HavayarUserCreateViewModel
{
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

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [NotNull]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Birth Date")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Profile Picture")] public byte[]? ProfilePicture { get; set; }

    [Required]
    [Display(Name = "User Role")]
    public List<Roles> Roles { get; set; }
}
