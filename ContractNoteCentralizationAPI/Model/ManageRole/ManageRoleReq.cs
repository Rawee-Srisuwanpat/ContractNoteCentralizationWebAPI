namespace ContractNoteCentralizationAPI.Model.ManageRole
{
    public class ManageRoleReq
    {
        public long Id { get; set; }


        public string role_name { get; set; }



        public string? Description { get; set;} = string.Empty;

        public string  is_active { get; set;  }


        

        public string create_by { get; set; }
        public string update_by { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }

        public List<Screen> screens { get; set; }
    }

    public class Screen
    {
        public string Screen_name { get; set; }

        public string Screen_description { get; set; } = string.Empty;

        public List<Action> actions { get; set; } 
    }

    public class Action
    {
        public long Row_no { get; set; }
        public string Action_name { get; set; }

        public string description { get; set; } = string.Empty;
    }
}
