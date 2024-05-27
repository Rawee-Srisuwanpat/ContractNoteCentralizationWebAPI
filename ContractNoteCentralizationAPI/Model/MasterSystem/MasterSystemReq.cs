
namespace ContractNoteCentralizationAPI.Model.MasterSystem
{
    public class MasterSystemReq
    {

        public long? Row_no { get; set; }

        public long? Id { get; set; }

        public string? System { get; set; }

        
        public string? Description { get; set; }

        


        public string? create_by { get; set; }

        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }

        public DateTime? update_date { get; set; }

        public string? status { get; set; }
    }
}
