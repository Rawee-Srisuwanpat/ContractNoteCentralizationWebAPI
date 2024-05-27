namespace ContractNoteCentralizationAPI.Model.ManagerRegister
{
    public class ManageRegisterReq
    {
        public long Id { get; set; }

        public long Row_no { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }

        //public string password_confirm { get; set; }
        public string email { get; set; }
        public string system { get; set; }

        public DateTime request_date { get; set; }
        public string request_status { get; set; }
        public string is_active { get; set; }

        public string create_by { get; set; }
        public string update_by { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
    }
}
