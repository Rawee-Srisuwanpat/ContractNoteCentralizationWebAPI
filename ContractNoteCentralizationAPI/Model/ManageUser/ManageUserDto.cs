namespace ContractNoteCentralizationAPI.Model.ManageUser
{
    public class ManageUserDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string system { get; set; }

        public string systemName { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

        public string authenticate { get; set; }

        public string status { get; set; }

        public string statusText { get; set; }

        public string request_status { get; set; }

        public string request_status_text { get; set; }

        public string? request_date_text { get; set; }

        public string? create_by { get; set; }

        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }

        public DateTime? update_date { get; set; }
    }
}
