using Microsoft.AspNetCore.Identity;

namespace HavayarQuiz.Domain.Entities;

public class HavayarUser : IdentityUser<Guid>, IEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
    public byte[] ProfilePicture { get; set; }
}

