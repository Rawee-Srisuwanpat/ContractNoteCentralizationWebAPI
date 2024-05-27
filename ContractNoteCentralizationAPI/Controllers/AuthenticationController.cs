using ContractNoteCentralizationAPI.Model.Auth;
using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.Jwt;
using ContractNoteCentralizationAPI.Model.Log;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using ContractNoteCentralizationAPI.Services.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContractNoteCentralizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ILogService _logService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ILogService logService)
        {
            _authService = authService;
            _logger = logger;
            _logService = logService;
        }


        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult> Login(LoginReqModel req)
        {
            long log_id = 0;

            LoginResModel res = new LoginResModel();

            try
            {

                string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();

                #region  -------- log insert ------------------
                LogDao log_ins = new LogDao();
                log_ins.transaction_id = req.transaction_id;
                log_ins.method = ControllerContext.ActionDescriptor.ActionName;
                log_ins.controller = ControllerContext.ActionDescriptor.ControllerName;
                log_ins.request_json = JsonSerializer.Serialize(req);
                log_ins.ip_request = ipAddress;
                log_ins.system_code = Guid.Empty;
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
                        //res.status.status_desc = $"{nameof(req)} cannot be null";
                    }

                    var result_login = await _authService.Login(req.data.username, req.data.password, req.data.authen_type.ToUpper() == "AD" ? true : false);

                    res.status = result_login.status;
                    res.token = result_login.token;



                    if (result_login.status.status_code == StatusModel.success)
                    {
                        if (req.data.authen_type.ToUpper() == "AD")
                        {
                            var status_checkUserAD = _authService.CheckUserAD(req.data.username, req.data.password);

                            if (status_checkUserAD.status_code == StatusModel.success)
                            {
                                res.refresh_token = await _authService.UpdateRefreshToken(EnumChannel.web, result_login.user_id);
                            }
                            else
                            {
                                res.token = "";
                            }

                            res.status = status_checkUserAD;
                        }       
                    }

                }
                else
                {
                    var serializableModelState = new SerializableError(ModelState);
                    ResBadRequestModel res_bad = new ResBadRequestModel();
                    res_bad.status_code = StatusModel.bad_request;
                    res_bad.error = serializableModelState;


                    #region  -------- log update 
                    LogDao log_upd_400 = new LogDao();
                    log_upd_400.id = log_id;
                    log_upd_400.response_json = JsonSerializer.Serialize(res);
                    log_upd_400.internal_status_code = StatusModel.bad_request;
                    log_upd_400.internal_status_desc = MessageDesc.GetDescription(res_bad.status_code);
                    log_upd_400.http_status_code = StatusModel.bad_request;
                    log_upd_400.http_status_desc = MessageDesc.GetDescription(StatusModel.bad_request);
                    log_upd_400.response_json = JsonSerializer.Serialize(res_bad);
                    log_upd_400.error_desc = JsonSerializer.Serialize(res_bad.error);
                    log_upd_400.update_date = DateTime.Now;

                    await _logService.Update(log_upd_400);
                    #endregion ------ end log update

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




        [HttpPost]
        [Route("RefreshToken")]

        public async Task<ActionResult> RefreshToken(RefreshTokenReqModel req)
        {
            long log_id = 0;

            RefreshTokenResModel res = new RefreshTokenResModel();

            try
            {

                string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();

                #region  -------- log insert ------------------
                LogDao log_ins = new LogDao();
                log_ins.transaction_id = req.transaction_id;
                log_ins.method = ControllerContext.ActionDescriptor.ActionName;
                log_ins.controller = ControllerContext.ActionDescriptor.ControllerName;
                log_ins.request_json = JsonSerializer.Serialize(req);
                log_ins.ip_request = ipAddress;
                log_ins.system_code = Guid.Empty;
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
                        //res.status.status_desc = $"{nameof(req)} cannot be null";
                    }

                    var result_refreshToken = await _authService.RefreshToken(req.data.token, req.data.refresh_token, EnumChannel.web);
                    res.status = result_refreshToken.status;
                    res.token = result_refreshToken.token;
                    res.refresh_token = result_refreshToken.refresh_token;

                }
                else
                {
                    var serializableModelState = new SerializableError(ModelState);
                    ResBadRequestModel res_bad = new ResBadRequestModel();
                    res_bad.status_code = StatusModel.bad_request;
                    res_bad.error = serializableModelState;


                    #region  -------- log update 
                    LogDao log_upd_400 = new LogDao();
                    log_upd_400.id = log_id;
                    log_upd_400.response_json = JsonSerializer.Serialize(res);
                    log_upd_400.internal_status_code = StatusModel.bad_request;
                    log_upd_400.internal_status_desc = MessageDesc.GetDescription(res_bad.status_code);
                    log_upd_400.http_status_code = StatusModel.bad_request;
                    log_upd_400.http_status_desc = MessageDesc.GetDescription(StatusModel.bad_request);
                    log_upd_400.response_json = JsonSerializer.Serialize(res_bad);
                    log_upd_400.error_desc = JsonSerializer.Serialize(res_bad.error);
                    log_upd_400.update_date = DateTime.Now;

                    await _logService.Update(log_upd_400);
                    #endregion ------ end log update

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
