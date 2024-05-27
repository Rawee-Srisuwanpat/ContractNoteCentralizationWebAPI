using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageRoleController : ControllerBase
    {
        IManageRoleService manageRoleService;
        public ManageRoleController(IManageRoleService manageRoleService)
        {
             this.manageRoleService = manageRoleService;
        }

        [HttpPost]
        [Route("SearchRole")]
        public ManageRoleRes SearchRole()
        {
            var res = new ManageRoleRes();
            try
            {
                var req = new ManageRoleReq();
                res = manageRoleService.Search(req);
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
        [Route("SearchRoleByUser")]
        public ManageRoleRes SearchRoleByUser(SearchRoleByUserReq user)
        {
            var res = new ManageRoleRes();
            try
            {
                var req = new ManageRoleReq();
                res = manageRoleService.SearchRoleByUser(user);

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
        [Route("CreateRole")]
        public ManageRoleRes CreateRole(ManageRoleReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                res = manageRoleService.Create(req);
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
        [Route("EditRole")]
        public ManageRoleRes EditRole(ManageRoleReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                res = manageRoleService.Edit(req);
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
        [Route("DeleteRole")]
        public ManageRoleRes DeleteRole(ManageRoleDeleteReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                res = manageRoleService.Delete(req);
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
