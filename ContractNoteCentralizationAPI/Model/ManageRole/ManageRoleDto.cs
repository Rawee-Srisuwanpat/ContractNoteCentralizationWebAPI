namespace ContractNoteCentralizationAPI.Model.ManageRole
{
    public class ManageRoleDto
    {

        public long Id_tbm_Role { get; set; }
        public string? Role_code { get; set; }

        public string? Role_name { get; set; }



        public long Id { get; set; }
        public string? Screen_code { get; set; }

        public string? Screen_name { get; set; }

        public string?  is_active { get; set; }

        public string? is_active_text { get; set; }

        public string? description { get; set; }


        public List<Screen> screens { get; set; }

    
        public string? create_by { get; set; }
        public string? create_date { get; set; }
        public string? update_by { get; set; }
        public string? update_date { get; set; }


    }
}
