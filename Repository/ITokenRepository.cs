using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;

namespace ProjectAPI.Repository
{
    public interface ITokenRepository
    {
     string CreateJWTToken(IdentityUser user,List<string> roles);
       
    
    }
}
