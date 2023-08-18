using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

[Authorize(Roles = Domain.Consts.Roles.Admin)]
public class CreateModel : PageModel
{

    private readonly IHavayarUserService _havayarUserService;

    public CreateModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    [BindProperty]
    public HavayarUserCreateViewModel Input { get; set; } = default!;

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = Request.Form.Files.FirstOrDefault();
            using var dataStream = new MemoryStream();
            await file.CopyToAsync(dataStream);
            var profilepicture = dataStream.ToArray();
            var user = new HavayarUserCreateDto(Input.Email, Input.Username, Input.Password, Input.FirstName, Input.LastName, Input.BirthDate, profilepicture, Input.Roles);
            var userId = await _havayarUserService.CreateHavayarUserAsync(user, CancellationToken.None);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
