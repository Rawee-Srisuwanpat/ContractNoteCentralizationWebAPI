namespace ContractNoteCentralizationAPI.Model.ManageUser
{
    public class ManageUserReq
    {
        public long Id { get; set; }

        public string user_name { get; set; }

        public string? request_status { get; set; }

        public string authenticate { get; set; }

        public string? system_code { get; set; }
        public string email { get; set; }
        public string status  { get; set; }
        public string password { get; set; }
        public string role { get; set; }

        
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }



    }
}
