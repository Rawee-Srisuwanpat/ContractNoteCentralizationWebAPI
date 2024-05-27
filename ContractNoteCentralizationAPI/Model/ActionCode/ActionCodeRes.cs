using ContractNoteCentralizationAPI.Model.Master;

namespace ContractNoteCentralizationAPI.Model.ActionCode
{
    public class ActionCodeRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ActionCodeDto>? payload { get; set; }
    }
}
