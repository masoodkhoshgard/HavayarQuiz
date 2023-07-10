using HavayarQuiz.Application.HavayarUsers;
using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class IndexModel : PageModel
{
    private readonly IHavayarUserService _havayarUserService;

    public IndexModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    public IList<HavayarUserViewModel> HavayarUser { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var results = await _havayarUserService.GetHavayarUserAsync(CancellationToken.None);
        HavayarUser = (IList<HavayarUserViewModel>)(results is null
            ? Enumerable.Empty<HavayarUserViewModel>()
            : results.Select(model => new HavayarUserViewModel(model.Id,
                                                               model.Email,
                                                               model.Username,
                                                               model.FirstName,
                                                               model.LastName,
                                                               model.BirthDate.ToString("d"),
                                                               model.ProfilePicture)).ToList());
    }
}
