using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        IManageUserService manageUserService;
        public ManageUserController(IManageUserService manageUserService)
        {
            this.manageUserService = manageUserService;
        }

        [Authorize]
        [HttpPost]
        [Route("SearchUser")]
        public ManageUserRes SearchUser(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                //ManageUserReq req = new ManageUserReq();
                res = manageUserService.Search(req);
            }
            catch (Exception ex)
            {
                res.status_code = "500";
                res.status_text = ex.Message;
                res.payload = null;
            }
            return res;
        }


        // [Authorize] No need to add
        [HttpPost]
        [Route("CreateUser")]
        public ManageUserRes CreateUser(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                res = manageUserService.Create(req);
            }
            catch (Exception ex)
            {
                res.status_code = "500";
                res.status_text = ex.Message;
                res.payload = null;
            }
            return res;
        }

        [Authorize]
        [HttpPost]
        [Route("EditUser")]
        public ManageUserRes EditUser(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                res = manageUserService.Edit(req);
            }
            catch (Exception ex)
            {
                res.status_code = "500";
                res.status_text = ex.Message;
                res.payload = null;
            }
            return res;
        }
        [Authorize]
        [HttpPost]
        [Route("DeleteUser")]
        public ManageUserRes DeleteUser(ManageUserDeleteReq req)
        {
            var res = new ManageUserRes();
            try
            {
                res = manageUserService.Delete(req);
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
