using HavayarQuiz.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class CreateModel : PageModel
{

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public HavayarUser HavayarUser { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid || _context.HavayarUser == null || HavayarUser == null)
        //{
        //    return Page();
        //}

        //_context.HavayarUser.Add(HavayarUser);
        //await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
