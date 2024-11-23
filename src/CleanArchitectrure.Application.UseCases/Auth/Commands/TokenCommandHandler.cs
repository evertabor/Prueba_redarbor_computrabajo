using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitectrure.Application.UseCases.Auth.Commands
{
    public class TokenCommandHandler : IRequestHandler<TokenCommand, BaseResponse<TokenDto>>
    {
        private readonly IConfiguration _config;

        public TokenCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<BaseResponse<TokenDto>> Handle(TokenCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<TokenDto>();

            var claims = new List<Claim>
            {
                new (ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new (ClaimTypes.Name, Guid.NewGuid().ToString()),
                new (ClaimTypes.Role, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:Expires"] ?? "5")),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            response.Data = new TokenDto
            {
                AccessToken = jwt
            };

            response.succcess = true;
            response.Message = "Token generado correctamente";

            return response;
        }
    }
}
