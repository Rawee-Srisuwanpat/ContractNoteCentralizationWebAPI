using ContractNoteCentralizationAPI.Controllers;
using ContractNoteCentralizationAPI.Helper.AD;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.Auth;
using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.Jwt;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using ContractNoteCentralizationAPI.Services.Interface;
using ContractNoteCentralizationAPI.Services.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PrincipalModel _principal;
        public AuthService(ContractNoteCentralizationDbContext _context, IConfiguration configuration, PrincipalModel principal)
        {
            this._context = _context;
            this._principal = principal;
            _configuration = configuration;
        }


        public async Task<LoginResSvModel> Login(string username, string password, bool isUserAD)
        {
            LoginResSvModel res = new LoginResSvModel();

            var user = await GetUserByUserName(username, isUserAD);
            if (user == null)
            {
                res.status.status_code = StatusModel.invalid_username;
                res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);
                return res;
            }


            if (!isUserAD)

            {
                if (user.password != password)
                {
                    res.status.status_code = StatusModel.invalid_password;
                    res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);
                    return res;
                }
            }



            var authClaims = new List<Claim>
            {
               new Claim("username", user.user_name),
               new Claim("system_code", (user.system_code == null ? "280EE861-B289-443D-8EB1-A43D364124EE" : user.system_code) ),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Role, user.role)
            };

            string token = GenerateToken(authClaims, isUserAD ? EnumChannel.web : EnumChannel.api);


            res.user_id = user.id;
            res.token = token;
            res.status.status_code = StatusModel.success;
            res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);

            return res;
        }


        public StatusModel CheckUserAD(string username, string password)
        {
            StatusModel status = new StatusModel();

            try
            {
                var Domain = _principal.domain;
                var UserAdmin = _principal.userAdmin;
                var PassAdmin = _principal.passAdmin;

                bool isValid = false;
                UserPrincipal usr;

                PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain, null, UserAdmin, PassAdmin);

                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, username))
                {
                    if (foundUser == null)
                    {
                        status.status_code = StatusModel.invalid_username_ad;
                        status.status_desc = MessageDesc.GetDescription(status.status_code);

                        return status;
                    }
                }


                usr = UserPrincipal.FindByIdentity(pc, username);
                bool is_user_lock = usr.IsAccountLockedOut();

                if (is_user_lock)
                {
                    //usr.UnlockAccount();
                    status.status_code = StatusModel.user_ad_is_locked;
                    status.status_desc = MessageDesc.GetDescription(status.status_code);

                    return status;
                }


                isValid = pc.ValidateCredentials(username, password);
                if (isValid)
                {
                    status.status_code = StatusModel.success;
                    status.status_desc = MessageDesc.GetDescription(status.status_code);

                    return status;
                }
                else
                {
                    status.status_code = StatusModel.invalid_password_ad;
                    status.status_desc = MessageDesc.GetDescription(status.status_code);

                    return status;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public string GetValurFromToken(string idtoken, string type_name)
        {
            var token = new JwtSecurityToken(jwtEncodedString: idtoken);
            return token.Claims.First(c => c.Type == type_name).Value;

        }

        //public   List<ManageRoleDto>? GetRole(string userName) 
        //{
        //    //using (var ctx = _context) 
        //    //{
        //    //    var user = await ctx.tbt_manage_user.FirstOrDefaultAsync(z => z.user_name == userName && z.authenticate == "2");
        //    //    ManageRoleService manageRoleService = new ManageRoleService(ctx);
        //    //    var payload = manageRoleService.Search(new ManageRoleReq()).payload;
        //    //    var role =  payload.Where(z => z.Id_tbm_Role.ToString() == user.role).ToList();
        //    //    return role;
        //    //}
        //}


        private string GenerateToken(IEnumerable<Claim> claims, EnumChannel channel)
        {

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            Int64 _TokenExpiryTimeInMinute = 0;

            if (channel == EnumChannel.web)
            {
                _TokenExpiryTimeInMinute = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInMinute_web"]);
            }
            else if (channel == EnumChannel.api)
            {
                _TokenExpiryTimeInMinute = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInMinute_api"]);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddMinutes(_TokenExpiryTimeInMinute),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> UpdateRefreshToken(EnumChannel channel, long user_id)
        {
            string RefreshToken = GenerateRefreshToken();

            var user = await _context.tbt_manage_user.Where(x => x.id == user_id).FirstOrDefaultAsync();
            user.refresh_token = RefreshToken;
            user.refresh_token_expiry_time = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return RefreshToken;
        }

        public async Task<RefreshTokenResultModel> RefreshToken(string token, string resfresh_token, EnumChannel channel)
        {

            RefreshTokenResultModel result = new RefreshTokenResultModel();

            string? accessToken = token;
            string? refreshToken = resfresh_token;

            ClaimsPrincipal? principal = null;

            try
            {
                principal = GetPrincipalFromExpiredToken(accessToken);
            }
            catch (Exception ex)
            {
                result.status.status_code = StatusModel.error_exception;
                result.status.status_desc = "Error Token : " + ex.Message.ToString();
                return result;
            }



            if (principal == null)
            {
                result.status.status_code = StatusModel.invalid_token_or_refresh_token;
                result.status.status_desc = MessageDesc.GetDescription(result.status.status_code);
                return result;
            }


            string username = principal.Claims.First(c => c.Type == "username").Value;
            string system_code = principal.Claims.First(c => c.Type == "system_code").Value;


            var user = await GetUserForRefreshToken(username, system_code);

            if (user == null || user.refresh_token != refreshToken || user.refresh_token_expiry_time <= DateTime.Now)
            {
                result.status.status_code = StatusModel.invalid_token_or_refresh_token;
                result.status.status_desc = MessageDesc.GetDescription(result.status.status_code);
                return result;
            }


            var authClaims = new List<Claim>
            {
               new Claim("username", user.user_name),
               new Claim("system_code", system_code ),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Role, user.role)
            };

            var newAccessToken = GenerateToken(authClaims, channel);
            var newRefreshToken = GenerateRefreshToken();


            user.refresh_token = newRefreshToken;
            await _context.SaveChangesAsync();


            result.status.status_code = StatusModel.success;
            result.status.status_desc = MessageDesc.GetDescription(result.status.status_code);
            result.refresh_token = newRefreshToken;
            result.token = newAccessToken;

            return result;
        }


        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }



        private async Task<ManageUserDao> GetUserByUserName(string username, bool isUserAD)
        {
            try
            {
                ManageUserDao user = null;

                if (isUserAD)
                {
                    user = await _context.tbt_manage_user.Where(x => x.user_name == username && x.status == "1" && x.authenticate == "2").FirstOrDefaultAsync();
                }
                else
                {
                    user = await _context.tbt_manage_user.Where(x => x.user_name == username && x.status == "1" && x.request_status == "3" && x.authenticate == "1").FirstOrDefaultAsync();
                }


                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<ManageUserDao> GetUserForRefreshToken(string username, string system_code)
        {
            try
            {
                ManageUserDao user = null;
                user = await _context.tbt_manage_user.Where(x => x.user_name == username && x.status == "1" && x.system_code == system_code).FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
