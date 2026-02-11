using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
