namespace ContractNoteCentralizationAPI.Model.ManageRole
{
    public class TbmRoleDoa
    {
        public long Id           {get; set;}
        public string? Role_code    {get; set;}
        public string? Role_name    { get; set; }

        public string? description { get; set; }
        public string? is_active { get; set; }

        public string? create_by { get; set; }

        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }

        public DateTime? update_date { get; set; }


    }
}
