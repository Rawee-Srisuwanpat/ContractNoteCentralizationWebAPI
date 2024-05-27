using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.CollectorTeamCode
{
    public class CollectorCodeDao
    {
        [Key]
      public int collector_id { get ; set ; } 
      public string collector_code { get ; set ; } 
      public int? user_id { get ; set ; } 
      public string collector_name { get ; set ; } 
      public string? telephone_no { get ; set ; } 
      public string? extension { get ; set ; } 
      public int team_id { get ; set ; } 
      public int? work_line_id { get ; set ; } 
      public string? level_code { get ; set ; } 
      public int active_flag { get ; set ; } 
      public string? create_by { get ; set ; } 
      public DateTime create_date { get ; set ; } 
      public string update_by { get ; set ; } 
      public DateTime update_date { get ; set ; } 
      public int? suspend_flag { get ; set ; } 
      public int? trans_id { get ; set ; } 
      public string? trans_code { get ; set ; } 
      public int? number_of_limit { get ; set ; } 
      public string? product_type { get ; set ; } 
      public string? sub_product_type { get ; set ; } 
      public int? allocate_seq { get ; set ; } 
      public string? stop_vat_flag { get ; set ; } 
      public int? collector_type { get ; set ; } 
      public int? write_off_flag { get ; set ; } 
    }
}
