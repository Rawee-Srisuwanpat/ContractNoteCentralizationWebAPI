using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.CollectorTeamCode
{
    public class CollectorTeamDao
    {
        [Key]
       public int team_id { get ; set ; } 
       public string team_code { get ; set ; } 
       public string team_name { get ; set ; } 
       public string collector_group { get ; set ; } 
       public string prod_type { get ; set ; } 
       public int? branch_id { get ; set ; } 
       public int active_flag { get ; set ; } 
       public int create_by { get ; set ; } 
       public DateTime create_date { get ; set ; } 
       public int update_by { get ; set ; } 
       public DateTime update_date { get ; set ; } 
       public string? sub_product_type { get ; set ; } 
       public int? leader_id { get ; set ; } 
    }
}
