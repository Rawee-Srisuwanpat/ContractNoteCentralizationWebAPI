using AutoMapper;
using ContractNoteCentralizationAPI.Helper.AutoMapper;
using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.Log;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using ContractNoteCentralizationAPI.Services.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogApiController : ControllerBase
    {

        private readonly ILogService _logService;
        private readonly ILogger<FollowUpController> _logger;
        private readonly IAuthService _authService;


        public LogApiController(IContactNoteService contactNoteService
            , ILogService logService
            , IAuthService authService, ILogger<FollowUpController> logger)
        {
            _logService = logService;
            _authService = authService;
            _logger = logger;
        }


        [Authorize]
        [HttpPost]
        [Route("Inquiry")]
        public async Task<ActionResult> Inquiry(InquiryLogApiReqModel req)
        {
            InquiryLogApiResModel res = new InquiryLogApiResModel();
            long log_id = 0;

            try
            {

                string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                string system_code = _authService.GetValurFromToken(accessToken, "system_code");


                #region  -------- log insert ------------------
                LogDao log_ins = new LogDao();
                log_ins.transaction_id = req.transaction_id;
                log_ins.method = ControllerContext.ActionDescriptor.ActionName;
                log_ins.controller = ControllerContext.ActionDescriptor.ControllerName;
                log_ins.request_json = JsonSerializer.Serialize(req);
                log_ins.ip_request = ipAddress;
                log_ins.system_code = new Guid(system_code);
                log_ins.create_date = DateTime.Now;
                log_ins.update_date = DateTime.Now;

                log_ins = await _logService.Insert(log_ins);
                log_id = log_ins.id;
                #endregion ------ end log insert --------------

                if (ModelState.IsValid)
                {
                    if (req == null)
                    {
                        res.status.status_code = StatusModel.resquest_model_cannot_be_null;
                        res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);
                    }

                    if (!(Convert.ToInt16(req.page.page_no) > 0 && Convert.ToInt16(req.page.page_no) <= 20))
                    {
                        return BadRequest(new { Message = "Validation errors", Errors = "page_no must be between 0 and 20 " });
                    }



                    var inquiry_obj = await _logService.Inquiry(req.data, req.page);

                    res.data = inquiry_obj.data;
                    res.page.total_rows = inquiry_obj.total_rows.ToString();
                    res.page.page_no = inquiry_obj.page_no;

                    if (inquiry_obj.data.Count > 0)
                    {
                        res.status.status_code = StatusModel.success;
                        res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);
                    }
                    else
                    {
                        res.status.status_code = StatusModel.data_not_found;
                        res.status.status_desc = MessageDesc.GetDescription(res.status.status_code);
                    }


                }
                else
                {

                    var serializableModelState = new SerializableError(ModelState);
                    ResBadRequestModel res_bad = new ResBadRequestModel();
                    res_bad.status_code = StatusModel.bad_request;
                    res_bad.error = serializableModelState;


                    #region  ----------- log update ----------------
                    LogDao log_upd_400 = new LogDao();
                    log_upd_400.id = log_id;
                    log_upd_400.response_json = JsonSerializer.Serialize(res);
                    log_upd_400.internal_status_code = StatusModel.bad_request;
                    log_upd_400.internal_status_desc = MessageDesc.GetDescription(StatusModel.bad_request);
                    log_upd_400.http_status_code = StatusModel.bad_request;
                    log_upd_400.http_status_desc = MessageDesc.GetDescription(StatusModel.bad_request);
                    log_upd_400.response_json = JsonSerializer.Serialize(res_bad);
                    log_upd_400.error_desc = JsonSerializer.Serialize(res_bad.error);
                    log_upd_400.update_date = DateTime.Now;

                    await _logService.Update(log_upd_400);
                    #endregion --------- end log update ------------

                    return BadRequest(res_bad);
                }

                #region  -------- log update 
                LogDao log_upd = new LogDao();
                log_upd.id = log_id;
                log_upd.response_json = JsonSerializer.Serialize(res);
                log_upd.internal_status_code = res.status.status_code;
                log_upd.internal_status_desc = res.status.status_desc;
                log_upd.http_status_code = StatusModel.ok_200;
                log_upd.http_status_desc = MessageDesc.GetDescription(StatusModel.ok_200);
                log_upd.update_date = DateTime.Now;

                await _logService.Update(log_upd);
                #endregion ------ end log update

            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.Message);
                res.status.status_code = StatusModel.error_exception;
                res.status.status_desc = MessageDesc.GetDescription(StatusModel.error_exception);


                #region  ----------- log error ----------------
                LogDao log_upd = new LogDao();
                log_upd.id = log_id;
                log_upd.response_json = JsonSerializer.Serialize(res);
                log_upd.internal_status_code = res.status.status_code;
                log_upd.internal_status_desc = res.status.status_desc;
                log_upd.http_status_code = StatusModel.ok_200;
                log_upd.http_status_desc = MessageDesc.GetDescription(StatusModel.ok_200);
                log_upd.error_desc = ex.Message;
                log_upd.update_date = DateTime.Now;

                await _logService.Update(log_upd);
                #endregion ------ end log error ---------------
            }

            return Ok(res);
        }



    }
}
