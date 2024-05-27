using ContractNoteCentralizationAPI.Model.Auth;
using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ManageRole;
using System.Security.Claims;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResSvModel> Login(string username, string password, bool isUserAD);
        string GetValurFromToken(string idtoken, string type_name);
        StatusModel CheckUserAD(string username, string password);
        Task<string> UpdateRefreshToken(EnumChannel channel, long user_id);

        Task<RefreshTokenResultModel> RefreshToken(string token, string resfresh_token, EnumChannel channel);
        //List<ManageRoleDto>? GetRole(string userName);
    }
}
