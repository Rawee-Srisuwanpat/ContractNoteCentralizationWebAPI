using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.ResultCode
{
    public class ResultCodeDao
    {
        [Key]
        public int result_code_id { get; set; }
        public string result_code { get; set; }
        public string description { get; set; }
        public string result_group_type_code { get; set; }
        public int grace_period { get; set; }
        public int active_flag { get; set; }
        public int create_by { get; set; }
        public DateTime create_date { get; set; }
        public int update_by { get; set; }
        public DateTime update_date { get; set; }
    }
}
