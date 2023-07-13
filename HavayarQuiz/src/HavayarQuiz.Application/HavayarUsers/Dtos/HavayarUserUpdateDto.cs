using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavayarQuiz.Application.HavayarUsers.Dtos;
public record HavayarUserUpdateDto(Guid Id,
                                   string Email,
                                   string Username,
                                   string FirstName,
                                   string LastName,
                                   DateTime BirthDate,
                                   byte[] ProfilePicture,
                                   List<Domain.Enums.Roles> Roles);
