using ContractNoteCentralizationAPI.Model.ActionCode;

namespace ContractNoteCentralizationAPI.Model.ResultCode
{
    public class ResultCodeRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ResultCodeDto>? payload { get; set; }
    }
}
