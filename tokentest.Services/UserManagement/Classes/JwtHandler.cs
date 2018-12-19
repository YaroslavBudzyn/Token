using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using tokentest.Common.ApplicationSettings;
using tokentest.DataAccess.UserManagement.Entities;
using tokentest.Services.UserManagement.Classes.Interfaces;

namespace tokentest.Services.UserManagement.Classes
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtHeader _jwtHeader;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            //Check maybe use appsettings.Development.json
            _options = options.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(signingCredentials);
        }

        public Token Generate(User user, string role)
        {
            //Check maybe use appsettings.Development.json
            var expires = DateTime.Now.AddMinutes(_options.ExpiryMinutes).ToUniversalTime();// 5min 5 //14 days 20160

            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds;
            var iat = (long)new TimeSpan(DateTime.Now.Ticks - centuryBegin.Ticks).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub",  user.Name},
                {"iss",  _options.Issuer},
                {"aud",  _options.Audience},
                {"iat",  iat},
                {"exp",  exp},
                {"mail", user.Email},
                {"role", role}

            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);


            // Refresh Token Generate
            var refreshExp = (long)new TimeSpan(expires.AddMinutes(_options.RefreshExpiryMinutes).Ticks - centuryBegin.Ticks).TotalSeconds; // 2 days

            var refreshPayload = new JwtPayload
            {
                {"sub",  user.Email},
                {"iss",  _options.Issuer},
                {"iat",  iat},
                {"exp",  refreshExp},
                {"mail", user.Email},
                {"role", role}
            };

            var refreshJwt = new JwtSecurityToken(_jwtHeader, refreshPayload);
            var refreshToken = _jwtSecurityTokenHandler.WriteToken(refreshJwt);

            //Return token
            return new Token
            {
                Code = token,
                UserId = user.Id,
                User = user,
                RefreshToken = refreshToken
            };
        }
    }
}
