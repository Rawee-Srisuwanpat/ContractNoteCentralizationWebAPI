using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ManageRole;
using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.Auth
{
    public class RefreshTokenResultModel
    {

        public RefreshTokenResultModel()
        {
            status = new StatusModel();
        }


        public StatusModel status { get; set; }
        public string token { get; set; }
        public string refresh_token { get; set; }

        //public List<ManageRoleDto>? payload { get; set; }
    }


}
