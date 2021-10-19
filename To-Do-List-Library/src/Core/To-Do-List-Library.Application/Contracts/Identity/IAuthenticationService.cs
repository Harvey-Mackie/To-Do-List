using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Models.Authentication;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Core.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(AuthenticationRequest authenticationRequest);
        bool IsTokenValid(string token);
        Guid GetUserId(string token);
    }
}
