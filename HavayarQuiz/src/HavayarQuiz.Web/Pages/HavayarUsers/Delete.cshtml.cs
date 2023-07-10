using HavayarQuiz.Application.HavayarUsers;
using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class DeleteModel : PageModel
{
    private readonly IHavayarUserService _havayarUserService;

    public DeleteModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        //if (id == null || _context.HavayarUser == null)
        //{
        //    return NotFound();
        //}
        //var havayaruser = await _context.HavayarUser.FindAsync(id);

        //if (havayaruser != null)
        //{
        //    HavayarUser = havayaruser;
        //    _context.HavayarUser.Remove(HavayarUser);
        //    await _context.SaveChangesAsync();
        //}

        return RedirectToPage("./Index");
    }
}
