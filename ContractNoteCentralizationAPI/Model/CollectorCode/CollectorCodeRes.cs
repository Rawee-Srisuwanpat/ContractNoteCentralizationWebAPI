using ContractNoteCentralizationAPI.Model.ActionCode;

namespace ContractNoteCentralizationAPI.Model.CollectorTeamCode
{
    public class CollectorCodeRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<CollectorCodeDto>? payload { get; set; }
    }
}
