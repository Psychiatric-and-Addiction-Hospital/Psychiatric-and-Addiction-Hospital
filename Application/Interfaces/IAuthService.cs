using Application.DTO;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> CreateTokenWithRefresh(AppUser user);
    }
}
