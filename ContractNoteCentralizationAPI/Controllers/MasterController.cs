using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.CollectorTeamCode;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.Master;
using ContractNoteCentralizationAPI.Model.ResultCode;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        IMasterService masterService;
        public MasterController(IMasterService masterService)
        {
            this.masterService = masterService;
        }

        //[Authorize]
        [HttpPost]
        [Route("GetAllMaster")]
        public MasterRes GetAllMaster()
        {
            var res = new MasterRes();
            try
            {
                MasterReq req = new MasterReq();
                res = masterService.GetAll(req);
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
        [Route("GetAllActionCode")]
        public ActionCodeRes GetAllActionCode()
        {
            var res = new ActionCodeRes();
            try
            {
                var req = new ActionCodeReq();
                res = masterService.GetAllActionCode(req);
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
        [Route("GetAllResultCode")]
        public ResultCodeRes GetAllResultCode()
        {
            var res = new ResultCodeRes();
            try
            {
                var req = new ResultCodeReq();
                res = masterService.GetAllResultCode(req);
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
        [Route("GetAllCollectorTeamCode")]
        public CollectorTeamCodeRes GetAllCollectorTeamCode() 
        {
            var res = new CollectorTeamCodeRes();
            try
            {
                var req = new CollectorTeamCodeReq();
                res = masterService.GetAllCollectorTeamCode(req);
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
        [Route("GetAllCollectorCode")]
        public CollectorCodeRes GetAllCollectorCode()
        {
            var res = new CollectorCodeRes();
            try
            {
                var req = new CollectorCodeReq();
                res = masterService.GetAllCollectorCode(req);
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
