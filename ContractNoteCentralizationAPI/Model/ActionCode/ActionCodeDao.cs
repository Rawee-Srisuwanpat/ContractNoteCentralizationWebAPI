using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.ActionCode
{
    public class ActionCodeDao
    {
        [Key]
        public int action_code_id { get; set; }
        public string action_code { get; set; }
        public string description { get; set; }
        public string contact_type_code { get; set; }
        public string action_group_code { get; set; }
        public int sla_time { get; set; }
        public int active_flag { get; set; }
        public int create_by { get; set; }
        public DateTime create_date { get; set; }
        public int update_by { get; set; }
        public DateTime update_date { get; set; }
    }
}
