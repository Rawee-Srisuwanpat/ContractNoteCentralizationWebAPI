using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Model.LogLogin
{
    public class LogLoginRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<LogLginDto>? payload { get; set; }
    }
}
