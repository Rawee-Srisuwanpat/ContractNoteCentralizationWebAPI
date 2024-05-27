using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Model.VerifyOtp;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterSystemController : ControllerBase
    {
        IMasterSystemService masterSystemService;
        public MasterSystemController(IMasterSystemService masterSystemService)
        {
            this.masterSystemService = masterSystemService;
        }

        
        [HttpPost]
        [Route("SearchMasterSystem")]
        public MasterSystemRes SearchMasterSystem()
        {
            var res = new MasterSystemRes();
            try
            {
                MasterSystemReq req = new MasterSystemReq();
                res = masterSystemService.SearchMasterSystem(req);
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
        [Route("CreateMasterSystem")]
        public MasterSystemRes CreateMasterSystem(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                res = masterSystemService.Create(req);
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
        [Route("EditMasterSystem")]
        public MasterSystemRes EditMasterSystem(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                res = masterSystemService.Edit(req);
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
        [Route("DeleteMasterSystem")]
        public MasterSystemRes DeleteMasterSystem(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                res = masterSystemService.Delete(req);
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
