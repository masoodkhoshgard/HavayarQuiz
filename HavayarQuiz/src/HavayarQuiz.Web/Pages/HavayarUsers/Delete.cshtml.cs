using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

[Authorize(Roles = Domain.Consts.Roles.Admin)]
public class DeleteModel : PageModel
{
    private readonly IHavayarUserService _havayarUserService;

    public DeleteModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    //[BindProperty]
    public HavayarUserViewModel HavayarUser { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {

        if (id == null || id == Guid.Empty)
        {
            return NotFound();
        }

        var havayaruser = await _havayarUserService.GetHavayarUserAsync(id ?? Guid.NewGuid(), CancellationToken.None);
        if (havayaruser == null)
        {
            return NotFound();
        }
        else
        {
            HavayarUser = new HavayarUserViewModel(havayaruser.Id, havayaruser.Email, havayaruser.Username, havayaruser.FirstName, havayaruser.LastName, havayaruser.BirthDate.ToString("d"), havayaruser.ProfilePicture, string.Join(", ", havayaruser.Roles));
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _havayarUserService.RemoveHavayarUserAsync(id, CancellationToken.None);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            var havayaruser = await _havayarUserService.GetHavayarUserAsync(id ?? Guid.NewGuid(), CancellationToken.None);
            HavayarUser = new HavayarUserViewModel(havayaruser.Id, havayaruser.Email, havayaruser.Username, havayaruser.FirstName, havayaruser.LastName, havayaruser.BirthDate.ToString("d"), havayaruser.ProfilePicture, string.Join(", ", havayaruser.Roles));
            return Page();
        }
    }
}

