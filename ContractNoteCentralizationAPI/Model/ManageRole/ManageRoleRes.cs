using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Model.ManageRole
{
    public class ManageRoleRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ManageRoleDto>? payload { get; set; }
    }
}
