using ContractNoteCentralizationAPI.Controllers;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.Auth;
using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.Jwt;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Drawing;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using System.Collections;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.VisualBasic;
using ContractNoteCentralizationAPI.Model.Log;
using ContractNoteCentralizationAPI.Model.ManageRole;
using System.Net;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class LogService : ILogService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        private readonly IConfiguration _configuration;
        public LogService(ContractNoteCentralizationDbContext _context, IConfiguration configuration)
        {
            this._context = _context;
            _configuration = configuration;
        }




        public async Task<LogDao> Insert(LogDao obj)
        {
            try
            {
                await _context.tbt_log.AddAsync(obj);
                _context.SaveChanges();

                return obj;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }



        public async Task<bool> Update(LogDao obj)
        {
            try
            {

                var entity = _context.tbt_log.Where(x => x.id == obj.id).FirstOrDefault();

                entity.response_json = string.IsNullOrEmpty(obj.response_json) ? entity.response_json : obj.response_json;
                entity.internal_status_code = string.IsNullOrEmpty(obj.internal_status_code) ? entity.internal_status_code : obj.internal_status_code;
                entity.internal_status_desc = string.IsNullOrEmpty(obj.internal_status_desc) ? entity.internal_status_desc : obj.internal_status_desc;
                entity.http_status_code = string.IsNullOrEmpty(obj.http_status_code) ? entity.http_status_code : obj.http_status_code;
                entity.http_status_desc = string.IsNullOrEmpty(obj.http_status_desc) ? entity.http_status_desc : obj.http_status_desc;
                entity.error_desc = string.IsNullOrEmpty(obj.error_desc) ? entity.error_desc : obj.error_desc;
                entity.remark = string.IsNullOrEmpty(obj.remark) ? entity.remark : obj.remark;
                entity.update_date = DateTime.Now;

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }


        public async Task<InquiryLogApiSvModel> Inquiry(InquiryLogApiReq_data obj, PageDetailReqModel_web page)
        {
            try
            {

                InquiryLogApiSvModel res = new InquiryLogApiSvModel();

                DateTime create_date_from = DateTime.Now;
                DateTime create_date_to = DateTime.Now;


                if (obj.create_date != "")
                {
                    create_date_from = DateTime.ParseExact(obj.create_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    create_date_to = create_date_from.Date.AddDays(1).AddTicks(-1);
                }


                DateTime update_date_from = DateTime.Now;
                DateTime update_date_to = DateTime.Now;


                if (obj.update_date != "")
                {
                    update_date_from = DateTime.ParseExact(obj.update_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    update_date_to = update_date_from.Date.AddDays(1).AddTicks(-1);
                }



                var query = _context.tbt_log.Where(x =>
                    (string.IsNullOrEmpty(obj.transaction_id) || x.transaction_id == obj.transaction_id)
                     && (string.IsNullOrEmpty(obj.create_date) || x.create_date >= create_date_from && x.create_date < create_date_to)
                     && (string.IsNullOrEmpty(obj.update_date) || x.update_date >= update_date_from && x.update_date < update_date_to)
                     && (string.IsNullOrEmpty(obj.controller) || x.controller == obj.controller)
                     && (string.IsNullOrEmpty(obj.method) || x.method == obj.method)
                     && (string.IsNullOrEmpty(obj.http_status_code) || x.http_status_code == obj.http_status_code)
                     && (string.IsNullOrEmpty(obj.internal_status_code) || x.internal_status_code == obj.internal_status_code)
                     && (string.IsNullOrEmpty(obj.ip_request) || x.ip_request == obj.ip_request)
                     && (string.IsNullOrEmpty(obj.system_code) || x.system_code == Guid.Parse(obj.system_code))
                ).AsNoTracking();

                var lst_entity = await query.Skip((Convert.ToUInt16(page.page_no) - 1) * Convert.ToInt16(page.page_size)).Take(Convert.ToInt16(page.page_size)).ToListAsync();
                var total_row = await query.CountAsync();




                List<InquiryLogApiSv_data> lst_log = new List<InquiryLogApiSv_data>();

                foreach (var cn in lst_entity)
                {
                    InquiryLogApiSv_data obj_log = new InquiryLogApiSv_data();
                    var system = await _context.tbm_master_system.Where(x => x.System_code == cn.system_code.ToString()).FirstOrDefaultAsync();

                    obj_log.id = cn.id.ToString();
                    obj_log.transaction_id = cn.transaction_id;
                    obj_log.system_name = system != null ? system.System : "";
                    obj_log.system_code = cn.system_code.ToString();
                    obj_log.method = cn.method;
                    obj_log.controller = cn.controller;
                    obj_log.internal_status_code = cn.internal_status_code;
                    obj_log.internal_status_desc = cn.internal_status_desc;
                    obj_log.http_status_code = cn.http_status_code;
                    obj_log.http_status_desc = cn.http_status_desc;
                    obj_log.error_desc = cn.error_desc;
                    obj_log.remark = cn.remark;
                    obj_log.ip_request = cn.ip_request;
                    obj_log.create_date = cn.create_date.HasValue ? cn.create_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    obj_log.update_date = cn.update_date.HasValue ? cn.update_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;

                    lst_log.Add(obj_log);
                }


                res.data = lst_log;
                res.total_rows = total_row;
                res.page_no = page.page_no;

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }







    }
}
