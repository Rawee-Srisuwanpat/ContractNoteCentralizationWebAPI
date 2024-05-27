using ContractNoteCentralizationAPI.Model.ResetPassword;
using ContractNoteCentralizationAPI.Model.SendOtp;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using ContractNoteCentralizationAPI.Model.VerifyOtp;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IUsersAuthenticationService
    {
        LoginRes Login(LoginReq req);

        //RegisterRes Register(RegisterReq req);
        SendOtpRes SendOtp(SendOtpReq req);

        VerifyOtpRes VerifyOtp(VerifyOtpReq req);

        ResetPasswordRes ResetPassword(ResetPasswordReq req);

    }
}
