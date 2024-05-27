using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.ResetPassword;
using ContractNoteCentralizationAPI.Model.SendOtp;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using ContractNoteCentralizationAPI.Model.VerifyOtp;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthenticationController : ControllerBase
    {
        IUsersAuthenticationService usersAuthenticationService;
        private readonly ContractNoteCentralizationDbContext _context;
        public UsersAuthenticationController(IUsersAuthenticationService usersAuthenticationService, ContractNoteCentralizationDbContext _context) 
        { 
            this.usersAuthenticationService = usersAuthenticationService;
            this._context = _context;
        }
        // GET: api/<UsersAuthenticationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersAuthenticationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        //[HttpPost]
        //[Route("Login")]
        //public LoginRes Login(LoginReq req)
        //{
        //    var loginRes = new LoginRes();
        //    try
        //    {
        //        loginRes = usersAuthenticationService.Login(req);
        //    }
        //    catch (Exception ex)
        //    {
        //        loginRes.status_code = "500";
        //        loginRes.status_text = ex.Message;
        //        loginRes.payload = null;
        //    }
        //    return loginRes;
        //}


        

        [HttpPost]
        [Route("SendOtp")]
        public SendOtpRes SendOtp(SendOtpReq req)
        {
            var loginRes = new SendOtpRes();
            try
            {
                loginRes = usersAuthenticationService.SendOtp(req);
            }
            catch (Exception ex)
            {
                loginRes.status_code = "500";
                loginRes.status_text = ex.Message;
                loginRes.payload = null;
            }
            return loginRes;
        }

        [HttpPost]
        [Route("VerifyOtp")]
        public VerifyOtpRes VerifyOtp(VerifyOtpReq req)
        {
            var res = new VerifyOtpRes();
            try
            {
                res = usersAuthenticationService.VerifyOtp(req);
            }
            catch (Exception ex)
            {
                res.status_code = "500";
                res.status_text = ex.Message;
                res.payload = null;
            }
            return res;
        }

        [HttpPost]
        [Route("ResetPassword")]
        public ResetPasswordRes ResetPassword(ResetPasswordReq req)
        {
            var res = new ResetPasswordRes();
            try
            {
                res = usersAuthenticationService.ResetPassword(req);
            }
            catch (Exception ex)
            {
                res.status_code = "500";
                res.status_text = ex.Message;
                res.payload = null;
            }
            return res;
        }


    }
}
