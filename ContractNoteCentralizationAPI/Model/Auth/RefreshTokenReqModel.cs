using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ManageRole;
using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.Auth
{
    public class RefreshTokenReqModel
    {

        public string transaction_id { get; set; }
        public refreshToken_data data { get; set; }

    }


    public class refreshToken_data
    {
        [Required(ErrorMessage = "token is required")]
        public string token { get; set; }

        [Required(ErrorMessage = "refresh_token is required")]
        public string refresh_token { get; set; }
    }



}
