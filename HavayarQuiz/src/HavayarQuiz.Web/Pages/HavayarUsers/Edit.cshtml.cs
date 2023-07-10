using HavayarQuiz.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

public class EditModel : PageModel
{
    //private readonly HavayarQuiz.Web.Data.HavayarQuizWebContext _context;

    //public EditModel(HavayarQuiz.Web.Data.HavayarQuizWebContext context)
    //{
    //    _context = context;
    //}

    [BindProperty]
    public HavayarUser HavayarUser { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        //if (id == null || _context.HavayarUser == null)
        //{
        //    return NotFound();
        //}

        //var havayaruser =  await _context.HavayarUser.FirstOrDefaultAsync(m => m.Id == id);
        //if (havayaruser == null)
        //{
        //    return NotFound();
        //}
        //HavayarUser = havayaruser;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        //_context.Attach(HavayarUser).State = EntityState.Modified;

        //try
        //{
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!HavayarUserExists(HavayarUser.Id))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
        //}

        return RedirectToPage("./Index");
    }

    //private bool HavayarUserExists(Guid id)
    //{
    //  return (_context.HavayarUser?.Any(e => e.Id == id)).GetValueOrDefault();
    //}
}
