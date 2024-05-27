namespace ContractNoteCentralizationAPI.Model.ResetPassword
{
    public class ResetPasswordReq
    {
        public string user_name             {get ;set ; }
        public string password          {get ;set ; }
        public string password_confirm { get; set; }
    }
}
