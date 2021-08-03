using System.Collections.Generic;
using Microsoft.Extensions.Options;
using UserApi.Extensions;
using UserApi.Models;

namespace UserApi.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwt(ApplicationUser user, IList<string> roles, IOptions<AuthOptions> authOptions);
    }
}