using ContractNoteCentralizationAPI.Model.ManageRole;

namespace ContractNoteCentralizationAPI.Model.UsersAuthentication
{
    public class LoginRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ManageRoleDto>? payload { get; set; }

    }
}
