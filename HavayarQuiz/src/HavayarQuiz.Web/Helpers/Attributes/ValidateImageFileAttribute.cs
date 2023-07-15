using System.ComponentModel.DataAnnotations;

namespace HavayarQuiz.Web.Helpers.Attributes;

public class ValidateImageFileAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not Microsoft.AspNetCore.Http.IFormFile file)
        {
            return ValidationResult.Success;
        }

        var extension = Path.GetExtension(file.FileName);

        if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Contains(extension.ToLower()))
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Only the following file extensions are allowed: {string.Join(", ", _allowedExtensions)}";
    }
}
