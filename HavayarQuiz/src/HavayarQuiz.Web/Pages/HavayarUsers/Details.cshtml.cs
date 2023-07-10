using HavayarQuiz.Application.HavayarUsers;
using HavayarQuiz.Domain.Entities;
using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class DetailsModel : PageModel
{
    private readonly IHavayarUserService _havayarUserService;

    public DetailsModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

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
            HavayarUser = new HavayarUserViewModel(havayaruser.Id, havayaruser.Email, havayaruser.Username, havayaruser.FirstName, havayaruser.LastName, havayaruser.BirthDate.ToString("d"), havayaruser.ProfilePicture);
        }
        return Page();
    }
}
