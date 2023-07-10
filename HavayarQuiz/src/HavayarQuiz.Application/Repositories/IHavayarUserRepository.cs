using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavayarQuiz.Application.Repositories;
public interface IHavayarUserRepository : IRepository<HavayarUser, Guid>
{
    Task<string> AddAsync(HavayarUser entity, List<Domain.Enums.Roles> roles, string password, CancellationToken cancellation = default);
}
