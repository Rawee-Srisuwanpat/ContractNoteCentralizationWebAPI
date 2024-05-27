namespace ContractNoteCentralizationAPI.Model.ManageRole
{
    public class ManageRoleDao
    {
        public long Id { get; set; }
        public string? Role_code { get; set; }
        public string? Screen_code { get; set; }

        public string? is_active { get; set; }
        public string? description { get; set; }
        public string? visible_menu { get; set; }
        public string? create_data { get; set; }
        public string? edit_data { get; set; }
        public string? view_data { get; set; }
        public string? delete_data { get; set; }
        public string? create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }
        public DateTime? update_date { get; set; }
    }
}
