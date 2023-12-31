﻿namespace HavayarQuiz.Application.HavayarUsers.Dtos;
public record HavayarUserUpdateDto(Guid Id,
                                   string Email,
                                   string Username,
                                   string FirstName,
                                   string LastName,
                                   DateTime BirthDate,
                                   byte[] ProfilePicture,
                                   List<Domain.Enums.Roles> Roles);
