using System;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Identity;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Application.Models.Authentication;
using AutoMapper;
using To_Do_List_Library.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Linq;

namespace To_Do_List_Library.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<string> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = _mapper.Map<User>(authenticationRequest);
            var loggedInUser = _userRepository.LoginUser(user);

            if(loggedInUser is null)
            {
                throw new Exception("Invalid Authentication Request");
            }

            var jwtSecurityToken = GenerateToken(loggedInUser);
            var jwtSecurityTokenSerialized = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwtSecurityTokenSerialized;
        }

        public Guid GetUserId(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return Guid.Parse(jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
        }

        public bool IsTokenValid(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true, 
                ValidateAudience = false, 
                ValidateIssuer = false,  
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            }; 
            SecurityToken validatedToken;

            jwtSecurityTokenHandler.ValidateToken(token, validationParameters,out validatedToken);
            
            return true;
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );
        }
    }

}