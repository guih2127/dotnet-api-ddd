using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        private ILoginService _loginService;
        public SigningConfigurations _signingConfigurations;
        public TokenConfiguration _tokenConfigurations;
        public IConfiguration _configuration;

        public LoginService(IUserRepository repository, 
                            SigningConfigurations signingConfigurations, 
                            TokenConfiguration tokenConfiguration,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                if (baseUser == null)
                    return new { authenticated = false, message = "Falha ao autenticar."};
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );
                }

                return null; // remover
            }
            else
                return null;
        }
    }
}