namespace ContractNoteCentralizationAPI.Model.ManagerRegister
{
    public class ManageRegisterDto
    {
        public long? Row_no { get; set; }

        public long? Id { get; set; }
        public string? user_name { get; set; }
        public string? password { get; set; }

        public string? password_confirm { get; set; }
        public string? email { get; set; }
        public string? system { get; set; }
        public string? System_name { get; set; }

        public DateTime? request_date { get; set; }

        public string? RequestStatus { get; set; }

        public string? RequestStatus_text { get; set; }
        public string? Status { get; set; }
        public string? Status_text { get; set; }
        public string? create_by { get; set; }
        public string? update_by { get; set; }


        public DateTime? create_date { get; set; }
        public DateTime? update_date { get; set; }
    }
}
