using HavayarQuiz.Application.HavayarUsers;
using Microsoft.AspNetCore.Mvc;

namespace HavayarQuiz.API.Controllers.HavayarUsers;

[ApiController]
[Route("[controller]")]
public class HavayarUserController : ControllerBase
{
    private readonly IHavayarUserService _havayarUserService;
}
