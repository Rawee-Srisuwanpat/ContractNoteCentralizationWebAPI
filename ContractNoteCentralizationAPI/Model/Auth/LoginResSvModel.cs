using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ManageRole;
using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.Auth
{
    public class LoginResSvModel
    {

        public LoginResSvModel()
        {
            status = new StatusModel();
        }

        public StatusModel status { get; set; }
        public string token { get; set; }
        public long user_id { get; set; } 
    }


}
