using ContractNoteCentralizationAPI.Model.MasterSystem;

namespace ContractNoteCentralizationAPI.Model.Master
{
    public class MasterRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<MasterDto>? payload { get; set; }
    }
}
