namespace ContractNoteCentralizationAPI.Model.Common
{
    public class StatusModel
    {
        public const string success = "00";
        public const string ok_200 = "200";
        public const string bad_request = "400";
        public const string error_exception = "99";
        public const string invalid_username = "01";
        public const string invalid_password = "02";
        public const string invalid_password_ad = "011";
        public const string invalid_username_ad = "012";
        public const string user_ad_is_locked = "013";
        public const string invalid_token_or_refresh_token = "014";
        public const string data_not_found = "03";
        public const string resquest_model_is_invalid = "04";
        public const string resquest_model_cannot_be_null = "05";
        public const string resquest_model_must_to_at_least_1_filte = "06";


        public string status_code { get; set; }
        public string status_desc { get; set; }
    }
}
