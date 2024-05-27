namespace ContractNoteCentralizationAPI.Model.Master
{
    public class MasterDao
    {
        public long id { get; set; }
        public string? master_type { get; set; }
        public string? master_type_name { get; set; }
        public string? master_text { get; set; }
        public string? master_value { get; set; }
        public string? orderList { get; set; }

        public string? status { get; set; }
        public string? create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }
        public DateTime? update_date { get; set; }
    }
}
