using ContractNoteCentralizationAPI.Model.Common;
using System.Data;

namespace ContractNoteCentralizationAPI.Services.Util
{
    public class MessageDesc
    {
        public static string GetDescription(string status_code)
        {
            string status_desc = string.Empty;


            switch (status_code)
            {
                case StatusModel.success:
                    status_desc = "success";
                    break;
                case StatusModel.error_exception:
                    status_desc = "error exception";
                    break;
                case StatusModel.invalid_password:
                    status_desc = "invalid password";
                    break;
                case StatusModel.invalid_username:
                    status_desc = "invalid username";
                    break;
                case StatusModel.invalid_username_ad:
                    status_desc = "invalid_username_ad";
                    break;
                case StatusModel.invalid_password_ad:
                    status_desc = "invalid_username_ad";
                    break;
                case StatusModel.user_ad_is_locked:
                    status_desc = "user_ad_is_locked";
                    break;
                case StatusModel.data_not_found:
                    status_desc = "data not found";
                    break;
                case StatusModel.resquest_model_is_invalid:
                    status_desc = "resquest model is invalid";
                    break;
                case StatusModel.resquest_model_cannot_be_null:
                    status_desc = "resquest model cannot be null";
                    break;
                case StatusModel.resquest_model_must_to_at_least_1_filte:
                    status_desc = "resquest model must to at least 1 filte";
                    break; 
                case StatusModel.invalid_token_or_refresh_token:
                    status_desc = "invalid access token or refresh token";
                    break;
                case StatusModel.bad_request:
                    status_desc = "bad request";
                    break;
                case StatusModel.ok_200:
                    status_desc = "ok";
                    break;
            }

            return status_desc;
        }


    }
}
