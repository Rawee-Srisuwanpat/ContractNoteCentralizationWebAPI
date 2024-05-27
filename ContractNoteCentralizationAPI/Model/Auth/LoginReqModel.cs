using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.Auth
{


    public class LoginReqModel
    {
        public string transaction_id { get; set; }
        public Login data { get; set; }
    }

    public class Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [Required(ErrorMessage = "authen_type is required")]
        public string authen_type { get; set; }
    }
         
}
