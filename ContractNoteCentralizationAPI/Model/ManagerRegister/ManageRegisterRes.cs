using ContractNoteCentralizationAPI.Model.MasterSystem;

namespace ContractNoteCentralizationAPI.Model.ManagerRegister
{
    public class ManageRegisterRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ManageRegisterDto>? payload { get; set; }
    }
}
