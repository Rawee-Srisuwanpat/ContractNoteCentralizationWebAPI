using ContractNoteCentralizationAPI.Model.ActionCode;

namespace ContractNoteCentralizationAPI.Model.CollectorTeamCode
{
    public class CollectorTeamCodeRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<CollectorTeamCodeDto>? payload { get; set; }
    }
}
