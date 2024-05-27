using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Model.SendOtp
{
    public class SendOtpRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public string? payload { get; set; }
    }
}
