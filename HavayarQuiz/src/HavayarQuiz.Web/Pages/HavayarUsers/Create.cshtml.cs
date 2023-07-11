using HavayarQuiz.Application.HavayarUsers;
using HavayarQuiz.Application.HavayarUsers.Dtos;
using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class CreateModel : PageModel
{

    private readonly IHavayarUserService _havayarUserService;

    public CreateModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    [BindProperty]
    public HavayarUserCreateViewModel Input { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Request.Form.Files.Count > 0)
        {
            var file = Request.Form.Files.FirstOrDefault();
            using var dataStream = new MemoryStream();
            await file.CopyToAsync(dataStream);
            Input.ProfilePicture = dataStream.ToArray();
        }

        var user = new HavayarUserCreateDto(Input.Email, Input.Username, Input.Password, Input.FirstName, Input.LastName, Input.BirthDate, Input.ProfilePicture, new List<Domain.Enums.Roles> { Input.Role });
        var userId = _havayarUserService.CreateHavayarUserAsync(user, CancellationToken.None);
        //_context.HavayarUser.Add(HavayarUser);
        //await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
