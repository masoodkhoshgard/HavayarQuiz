﻿using HavayarQuiz.Web.Pages.HavayarUsers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HavayarQuiz.Web.Pages.HavayarUsers;

[Authorize(Roles = Domain.Consts.Roles.Admin)]
public class EditModel : PageModel
{
    private readonly IHavayarUserService _havayarUserService;

    public EditModel(IHavayarUserService havayarUserService)
    {
        _havayarUserService = havayarUserService;
    }

    [BindProperty]
    public HavayarUserUpdateViewModel Input { get; set; } = default!;

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
            Input = new HavayarUserUpdateViewModel
            {
                Id = havayaruser.Id,
                Email = havayaruser.Email,
                Username = havayaruser.Username,
                FirstName = havayaruser.FirstName,
                LastName = havayaruser.LastName,
                BirthDate = havayaruser.BirthDate,
                ProfilePicture = havayaruser.ProfilePicture,
                Roles = havayaruser.Roles.Select(Enum.Parse<Domain.Enums.Roles>).ToList()
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using var dataStream = new MemoryStream();
                await file.CopyToAsync(dataStream);
                Input.ProfilePicture = dataStream.ToArray();
            }

            var user = new HavayarUserUpdateDto(Input.Id, Input.Email, Input.Username, Input.FirstName, Input.LastName, Input.BirthDate, Input.ProfilePicture, Input.Roles);
            await _havayarUserService.UpdateHavayarUserAsync(user, CancellationToken.None);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            if (Input.ProfilePicture is null)
            {
                var havayaruser = await _havayarUserService.GetHavayarUserAsync(Input.Id, CancellationToken.None);
                Input.ProfilePicture = Input.ProfilePicture ?? havayaruser.ProfilePicture;
            }

            return Page();
        }
    }
}
