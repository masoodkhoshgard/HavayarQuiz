namespace HavayarQuiz.Application.HavayarUsers.Dtos;
public record HavayarUserCreateDto(string Email,
                                   string Username,
                                   string Password,
                                   string FirstName,
                                   string LastName,
                                   DateTime BirthDate,
                                   byte[] ProfilePicture,
                                   List<Domain.Enums.Roles> Roles);
