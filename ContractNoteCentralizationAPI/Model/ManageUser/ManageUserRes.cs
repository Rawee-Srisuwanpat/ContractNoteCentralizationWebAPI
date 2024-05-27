using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Model.ManageUser
{
    public class ManageUserRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ManageUserDto>? payload { get; set; }
    }


    
}
