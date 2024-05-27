namespace ContractNoteCentralizationAPI.Model.ManagerRegister
{
    public class ManageRegisterDao
    {
        public long Id { get; set; }
        public string? user_name { get; set; }
        public string? password { get; set; }

        public string? email { get; set; }
        public string? system { get; set; }

        public DateTime? request_date { get; set; }
        public string? request_status { get; set; }
        public string? is_active { get; set; }
        public string? create_by { get; set; }

        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }

        public DateTime? update_date { get; set; }

        public string? Otp { get; set; }

        public DateTime? Otp_expire { get; set; }

        public string? Role_code { get; set; }
    }
}
