namespace HavayarQuiz.Application.HavayarUsers.Dtos;
public record HavayarUserReturnDto(Guid Id,
                                   string Email,
                                   string Username,
                                   string FirstName,
                                   string LastName,
                                   DateTime BirthDate,
                                   byte[] ProfilePicture);
