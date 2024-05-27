namespace ContractNoteCentralizationAPI.Model.ManageUser
{
    public class ManageUserDao
    {
        public long id { get; set; }

        public string? user_name { get; set; }



        public string? request_status { get; set; }

        public string? role { get; set; }

        public string? email { get; set; }

        public string? password { get; set; }

        public string? status { get; set; }
        public string? system_code { get; set; }



        public string? authenticate { get; set; }

        public string? create_by { get; set; }

        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }

        //public string? request_status { get; set; }

        public DateTime? update_date { get; set; }

        public string? otp { get; set; }

        public DateTime? otp_expire { get; set; }

        public string? refresh_token { get; set; }

        public DateTime? refresh_token_expiry_time { get; set; }

    }
}
