using ContractNoteCentralizationAPI.DataAccessADO.Implement;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Linq.Dynamic.Core;
using ContractNoteCentralizationAPI.Helper.DB;
using ContractNoteCentralizationAPI.Model.Contract;
using System;
using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class ContactNoteService : IContactNoteService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        private readonly ChatBotDbContext _chatBot_context;
        private readonly IConfiguration _configuration;
        private readonly ADO_ContactNoteRepository _contactNoteRepo_ADO;


        public ContactNoteService(ContractNoteCentralizationDbContext context
            //, ChatBotDbContext chatBot_context
            , IConfiguration configuration
            , ADO_ContactNoteRepository contactNoteRepository_ADO)
        {
            // _chatBot_context = chatBot_context;
            this._context = context;
            _configuration = configuration;
            _contactNoteRepo_ADO = contactNoteRepository_ADO;
        }


        public async Task<InquirySvModel> Inquiry(InquiryContactNote obj, PageDetailReqModel page)
        {
            try
            {

                InquirySvModel res = new InquirySvModel();


                DateTime? contact_date_from = DateTime.Now;
                DateTime? contact_date_to = DateTime.Now;

                if (obj.contact_date_from != "" && obj.contact_date_to != "")
                {
                    contact_date_from = DateTime.ParseExact(obj.contact_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    contact_date_to = DateTime.ParseExact(obj.contact_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    contact_date_from = null;
                    contact_date_to = null;
                }


                DateTime? create_date_from = DateTime.Now;
                DateTime? create_date_to = DateTime.Now;

                if (obj.create_date_from != "")
                {
                    create_date_from = DateTime.ParseExact(obj.create_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    create_date_to = DateTime.ParseExact(obj.create_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    create_date_from = null;
                    create_date_to = null;
                }


                Guid? system = null;
                if (obj.system_code != "")
                {
                    system = Guid.Parse(obj.system_code);
                }


                DateTime? remind_date = DateTime.Now;
                if (obj.remind_date != "")
                {
                    remind_date = DateTime.ParseExact(obj.remind_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    remind_date = null; ;
                }

                DateTime? update_date = DateTime.Now;
                if (obj.update_date != "")
                {
                    update_date = DateTime.ParseExact(obj.update_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else { update_date = null; }


                var parameters = new[] {

                 new Microsoft.Data.SqlClient.SqlParameter("@contract_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.contract_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.request_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@telephone_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.telephone_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@note", System.Data.SqlDbType.VarChar) { Value = (obj.note ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@action_code", System.Data.SqlDbType.VarChar) { Value = (obj.action_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@system", System.Data.SqlDbType.UniqueIdentifier) { Value = (object)contact_date_from ?? DBNull.Value  },
                 new Microsoft.Data.SqlClient.SqlParameter("@related_dept_code", System.Data.SqlDbType.VarChar, 1000){ Value = (obj.related_dept_code ?? (object)DBNull.Value)},
                 new Microsoft.Data.SqlClient.SqlParameter("@result_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.related_dept_code ?? (object)DBNull.Value) },

                 new Microsoft.Data.SqlClient.SqlParameter("@remind_date", System.Data.SqlDbType.DateTime) { Value =  (object)remind_date ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@update_date", System.Data.SqlDbType.DateTime) { Value =  (object)update_date ?? DBNull.Value },

                 new Microsoft.Data.SqlClient.SqlParameter("@contact_date_from", System.Data.SqlDbType.DateTime) { Value =  (object)contact_date_from ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@contact_date_to", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_to ?? DBNull.Value},
                 new Microsoft.Data.SqlClient.SqlParameter("@create_date_from", System.Data.SqlDbType.DateTime) { Value = (object)create_date_from ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@create_date_to", System.Data.SqlDbType.DateTime) { Value = (object)create_date_to ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@PTP_Amount", System.Data.SqlDbType.Decimal) { Value = (obj.PTP_Amount != ""? Convert.ToDecimal(obj.PTP_Amount) : (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@next_action_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_action_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@next_result_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_result_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@collector_code", System.Data.SqlDbType.VarChar, 50) { Value = (obj.collector_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@collector_team_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.collector_team_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_doc_flag", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_flag ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_doc_other", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_other ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@note_dept_code", System.Data.SqlDbType.VarChar, 5) { Value = (obj.note_dept_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@tel_sms", System.Data.SqlDbType.VarChar, 30) { Value = (obj.tel_sms ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@create_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.create_by ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@update_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.update_by ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@payment_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.payment_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@PageNumber", System.Data.SqlDbType.Int) { Value = (page.page_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@RowsOfPage", System.Data.SqlDbType.Int) { Value = (page.page_size ?? (object)DBNull.Value) }
                };






                // List<ContactNoteDao> lst_entity = await _context.tbt_contact_note.FromSqlRaw("exec dbo.sp_inquiry_contact_note_for_system @contract_no, @request_no, @telephone_no" +
                //", @note, @action_code, @system, @related_dept_code, @result_code, @remind_date, @update_date, @contact_date_from, @contact_date_to, @create_date_from, @create_date_to" +
                //", @PTP_Amount, @next_action_code, @next_result_code, @collector_code, @collector_team_code, @request_doc_flag, @request_doc_other" +
                //", @note_dept_code ,@tel_sms, @create_by, @update_by, @payment_no, @PageNumber, @RowsOfPage ", parameters.ToArray()).AsNoTracking().ToListAsync<ContactNoteDao>();


                List<ContactNoteIquiryDao> lst_entity = await _context.tbt_contact_note_inquiy.FromSqlRaw("exec dbo.sp_inquiry_contact_note_for_system @contract_no, @request_no, @telephone_no" +
              ", @note, @action_code, @system, @related_dept_code, @result_code, @remind_date, @update_date, @contact_date_from, @contact_date_to, @create_date_from, @create_date_to" +
              ", @PTP_Amount, @next_action_code, @next_result_code, @collector_code, @collector_team_code, @request_doc_flag, @request_doc_other" +
              ", @note_dept_code ,@tel_sms, @create_by, @update_by, @payment_no, @PageNumber, @RowsOfPage ", parameters.ToArray()).AsNoTracking().ToListAsync<ContactNoteIquiryDao>();



                List<ContactNoteDto> lst_contact_note = new List<ContactNoteDto>();

                foreach (ContactNoteIquiryDao entity in lst_entity)
                {
                    ContactNoteDto e = new ContactNoteDto();
                    e.contract_no = entity.contract_no;
                    e.request_no = entity.request_no;
                    e.telephone_no = entity.telephone_no;
                    e.note = entity.note;
                    e.action_code = entity.action_code;
                    e.related_dept_code = entity.related_dept_code;
                    e.result_code = entity.result_code;
                    e.contact_date = entity.contact_date.HasValue ? entity.contact_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    e.remind_date = entity.remind_date.HasValue ? entity.remind_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    e.PTP_Amount = entity.PTP_Amount.ToString();
                    e.next_action_code = entity.next_action_code;
                    e.next_result_code = entity.next_result_code;
                    e.collector_code = entity.collector_code;
                    e.collector_team_code = entity.collector_team_code;
                    e.request_doc_flag = entity.request_doc_flag;
                    e.request_doc_other = entity.request_doc_other;
                    e.note_dept_code = entity.note_dept_code;
                    e.create_by = entity.create_by;
                    e.create_date = entity.create_date.HasValue ? entity.create_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    e.update_by = entity.update_by;
                    e.update_date = entity.update_date.HasValue ? entity.update_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    e.payment_no = entity.payment_no;
                    e.tel_sms = entity.tel_sms;

                    lst_contact_note.Add(e);
                }

                res.data = lst_contact_note;

                if (lst_entity.Count > 0)
                {
                    res.total_rows = lst_entity.FirstOrDefault().total_rows;
                }

                res.page_no = page.page_no;

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }

        public async Task<InquiryWebSvModel> InquiryWeb(InquiryContactNote_web obj, PageDetailReqModel_web page)
        {
            try
            {


                InquiryWebSvModel res = new InquiryWebSvModel();

                DateTime? contact_date_from = DateTime.Now;
                DateTime? contact_date_to = DateTime.Now;

                if (obj.contact_date_from != "" && obj.contact_date_to != "")
                {
                    contact_date_from = DateTime.ParseExact(obj.contact_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    contact_date_to = DateTime.ParseExact(obj.contact_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    contact_date_from = null;
                    contact_date_to = null;
                }



                DateTime? create_date_from = DateTime.Now;
                DateTime? create_date_to = DateTime.Now;

                if (obj.create_date_from != "")
                {
                    create_date_from = DateTime.ParseExact(obj.create_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    create_date_to = DateTime.ParseExact(obj.create_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    create_date_from = null;

                    create_date_to = null;
                }

                Guid? system = null;
                if (obj.system_code != "")
                {
                    system = Guid.Parse(obj.system_code);
                }

                var parameters = new[] {

                 new Microsoft.Data.SqlClient.SqlParameter("@contract_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.contract_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.request_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@telephone_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.telephone_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@note", System.Data.SqlDbType.VarChar) { Value = (obj.note ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@action_code", System.Data.SqlDbType.VarChar) { Value = (obj.action_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@system", System.Data.SqlDbType.UniqueIdentifier) { Value = (object)contact_date_from ?? DBNull.Value  },
                 new Microsoft.Data.SqlClient.SqlParameter("@related_dept_code", System.Data.SqlDbType.VarChar, 1000){ Value = (obj.related_dept_code ?? (object)DBNull.Value)},
                 new Microsoft.Data.SqlClient.SqlParameter("@result_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.related_dept_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@contact_date_from", System.Data.SqlDbType.DateTime) { Value =  (object)contact_date_from ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@contact_date_to", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_to ?? DBNull.Value},
                 new Microsoft.Data.SqlClient.SqlParameter("@create_date_from", System.Data.SqlDbType.DateTime) { Value = (object)create_date_from ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@create_date_to", System.Data.SqlDbType.DateTime) { Value = (object)create_date_to ?? DBNull.Value },
                 new Microsoft.Data.SqlClient.SqlParameter("@PTP_Amount", System.Data.SqlDbType.Decimal) { Value = (obj.PTP_Amount != ""? Convert.ToDecimal(obj.PTP_Amount) : (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@next_action_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_action_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@next_result_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_result_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@collector_code", System.Data.SqlDbType.VarChar, 50) { Value = (obj.collector_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@collector_team_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.collector_team_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_doc_flag", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_flag ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@request_doc_other", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_other ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@note_dept_code", System.Data.SqlDbType.VarChar, 5) { Value = (obj.note_dept_code ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@tel_sms", System.Data.SqlDbType.VarChar, 30) { Value = (obj.tel_sms ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@create_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.create_by ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@update_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.update_by ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@payment_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.payment_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@PageNumber", System.Data.SqlDbType.Int) { Value = (page.page_no ?? (object)DBNull.Value) },
                 new Microsoft.Data.SqlClient.SqlParameter("@RowsOfPage", System.Data.SqlDbType.Int) { Value = (page.page_size ?? (object)DBNull.Value) }
                };


                List<ContactNoteIquiryDao> lst_entity = await _context.tbt_contact_note_inquiy.FromSqlRaw("exec dbo.sp_inquiry_contact_note_web @contract_no, @request_no, @telephone_no" +
                    ", @note, @action_code, @system, @related_dept_code, @result_code, @contact_date_from, @contact_date_to, @create_date_from, @create_date_to" +
                    ", @PTP_Amount, @next_action_code, @next_result_code, @collector_code, @collector_team_code, @request_doc_flag, @request_doc_other" +
                    ", @note_dept_code ,@tel_sms, @create_by, @update_by, @payment_no, @PageNumber, @RowsOfPage ", parameters.ToArray()).AsNoTracking().ToListAsync<ContactNoteIquiryDao>();



                List<InquiryWeb_contact_note> lst_cn = new List<InquiryWeb_contact_note>();

                foreach (ContactNoteIquiryDao cn in lst_entity)
                {
                    InquiryWeb_contact_note obj_cn = new InquiryWeb_contact_note();

                    var contract = await _context.tbt_contract.Where(x => x.ContractNo == cn.contract_no).FirstOrDefaultAsync();
                    var collector = await _context.tbm_collector.Where(x => x.collector_code == cn.collector_code).FirstOrDefaultAsync();
                    var collector_team = await _context.tbm_collector_team.Where(x => x.team_code == cn.collector_team_code).FirstOrDefaultAsync();
                    var System_code = await _context.tbm_master_system.Where(x => x.System_code == cn.system.ToString()).FirstOrDefaultAsync();

                    obj_cn.contact_note_id = cn.contact_note_id.ToString();
                    obj_cn.contract_no = cn.contract_no;
                    obj_cn.customer_name = contract != null ? contract.CustomerName : "";
                    obj_cn.system_name = System_code != null ? System_code.System : "";
                    obj_cn.system_code = cn.system.ToString();
                    obj_cn.request_no = cn.request_no;
                    obj_cn.telephone_no = cn.telephone_no;
                    obj_cn.note = cn.note;
                    obj_cn.action_code = cn.action_code;
                    obj_cn.related_dept_code = cn.related_dept_code;
                    obj_cn.result_code = cn.result_code;
                    obj_cn.contact_date = cn.contact_date.HasValue ? cn.contact_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    obj_cn.remind_date = cn.remind_date.HasValue ? cn.remind_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty; ;
                    obj_cn.PTP_Amount = cn.PTP_Amount.ToString();
                    obj_cn.next_action_code = cn.next_action_code;
                    obj_cn.next_result_code = cn.next_result_code;
                    obj_cn.collector_code = cn.collector_code;
                    obj_cn.collector_name = collector != null ? collector.collector_name : "";
                    obj_cn.collector_team_code = cn.collector_team_code;
                    obj_cn.collector_team_name = collector_team != null ? collector_team.team_name : "";
                    obj_cn.request_doc_flag = cn.request_doc_flag;
                    obj_cn.request_doc_other = cn.request_doc_other;
                    obj_cn.note_dept_code = cn.note_dept_code;
                    obj_cn.create_by = cn.create_by;
                    obj_cn.create_date = cn.create_date.HasValue ? cn.create_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    obj_cn.update_by = cn.create_by;
                    obj_cn.update_date = cn.update_date.HasValue ? cn.update_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    obj_cn.payment_no = cn.payment_no;
                    obj_cn.tel_sms = cn.tel_sms;

                    lst_cn.Add(obj_cn);
                }


                res.data = lst_cn;

                if (lst_entity.Count > 0)
                {
                    res.total_rows = lst_entity.FirstOrDefault().total_rows;
                }

                res.page_no = page.page_no;

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }


        public async Task<InquirySvModel> Inquiry_ADO(InquiryContactNote obj, PageDetailReqModel page)
        {
            try
            {

                InquirySvModel res = new InquirySvModel();


                DateTime? contact_date_from = DateTime.Now;
                DateTime? contact_date_to = DateTime.Now;

                if (obj.contact_date_from != "" && obj.contact_date_to != "")
                {
                    contact_date_from = DateTime.ParseExact(obj.contact_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    contact_date_to = DateTime.ParseExact(obj.contact_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    contact_date_from = null;
                    contact_date_to = null;
                }


                DateTime? create_date_from = DateTime.Now;
                DateTime? create_date_to = DateTime.Now;

                if (obj.create_date_from != "")
                {
                    create_date_from = DateTime.ParseExact(obj.create_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    create_date_to = DateTime.ParseExact(obj.create_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    create_date_from = null;
                    create_date_to = null;
                }


                Guid? system = null;
                if (obj.system_code != "")
                {
                    system = Guid.Parse(obj.system_code);
                }


                DateTime? remind_date = DateTime.Now;
                if (obj.remind_date != "")
                {
                    remind_date = DateTime.ParseExact(obj.remind_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    remind_date = null; ;
                }

                DateTime? update_date = DateTime.Now;
                if (obj.update_date != "")
                {
                    update_date = DateTime.ParseExact(obj.update_date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else { update_date = null; }




                List<System.Data.SqlClient.SqlParameter> lst_param = new List<System.Data.SqlClient.SqlParameter>();

                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contract_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.contract_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.request_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@telephone_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.telephone_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@note", System.Data.SqlDbType.VarChar) { Value = (obj.note ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@action_code", System.Data.SqlDbType.VarChar) { Value = (obj.action_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@system", System.Data.SqlDbType.UniqueIdentifier) { Value = (object)system ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@related_dept_code", System.Data.SqlDbType.VarChar, 1000) { Value = (obj.related_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@result_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.related_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@remind_date", System.Data.SqlDbType.DateTime) { Value = (object)remind_date ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@update_date", System.Data.SqlDbType.DateTime) { Value = (object)update_date ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contact_date_from", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_from ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contact_date_to", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_to ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_date_from", System.Data.SqlDbType.DateTime) { Value = (object)create_date_from ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_date_to", System.Data.SqlDbType.DateTime) { Value = (object)create_date_to ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@PTP_Amount", System.Data.SqlDbType.Decimal) { Value = (obj.PTP_Amount != "" ? Convert.ToDecimal(obj.PTP_Amount) : (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@next_action_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_action_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@next_result_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_result_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@collector_code", System.Data.SqlDbType.VarChar, 50) { Value = (obj.collector_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@collector_team_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.collector_team_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_doc_flag", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_flag ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_doc_other", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_other ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@note_dept_code", System.Data.SqlDbType.VarChar, 5) { Value = (obj.note_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@tel_sms", System.Data.SqlDbType.VarChar, 30) { Value = (obj.tel_sms ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.create_by ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@update_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.update_by ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@payment_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.payment_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@PageNumber", System.Data.SqlDbType.Int) { Value = (page.page_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@RowsOfPage", System.Data.SqlDbType.Int) { Value = (page.page_size ?? (object)DBNull.Value) });


                DataTable dt_contact_note = _contactNoteRepo_ADO.Search(lst_param, StoredProcedureContractNoteCentralization.sp_inquiry_contact_note_for_system);

                List<ContactNoteDto> lst_contact_note = new List<ContactNoteDto>();

                foreach (DataRow row in dt_contact_note.Rows)
                {
                    ContactNoteDto e = new ContactNoteDto();
                    e.contract_no = row["contract_no"].ToString();
                    e.request_no = row["request_no"].ToString();
                    e.telephone_no = row["telephone_no"].ToString();
                    e.note = row["note"].ToString();
                    e.action_code = row["action_code"].ToString();
                    e.related_dept_code = row["related_dept_code"].ToString();
                    e.result_code = row["result_code"].ToString();
                    e.contact_date = row["contact_date"].ToString();
                    e.remind_date = row["remind_date"].ToString();
                    e.PTP_Amount = row["PTP_Amount"].ToString();
                    e.next_action_code = row["next_action_code"].ToString();
                    e.next_result_code = row["next_result_code"].ToString();
                    e.collector_code = row["collector_code"].ToString();
                    e.collector_team_code = row["collector_team_code"].ToString();
                    e.request_doc_flag = row["request_doc_flag"].ToString();
                    e.request_doc_other = row["request_doc_other"].ToString();
                    e.note_dept_code = row["note_dept_code"].ToString();
                    e.create_by = row["create_by"].ToString();
                    e.create_date = row["create_date"].ToString();
                    e.update_by = row["update_by"].ToString();
                    e.update_date = row["update_date"].ToString();
                    e.payment_no = row["payment_no"].ToString();
                    e.tel_sms = row["tel_sms"].ToString();

                    lst_contact_note.Add(e);
                }

                res.data = lst_contact_note;

                if (dt_contact_note.Rows.Count > 0)
                {
                    res.total_rows = Convert.ToInt64(dt_contact_note.Rows[0]["total_rows"].ToString());
                }

                res.page_no = page.page_no;

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }


        public async Task<InquiryWebSvModel> InquiryWeb_ADO(InquiryContactNote_web obj, PageDetailReqModel_web page)
        {
            try
            {


                InquiryWebSvModel res = new InquiryWebSvModel();

                DateTime? contact_date_from = DateTime.Now;
                DateTime? contact_date_to = DateTime.Now;

                if (obj.contact_date_from != "" && obj.contact_date_to != "")
                {
                    contact_date_from = DateTime.ParseExact(obj.contact_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    contact_date_to = DateTime.ParseExact(obj.contact_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    contact_date_from = null;
                    contact_date_to = null;
                }



                DateTime? create_date_from = DateTime.Now;
                DateTime? create_date_to = DateTime.Now;

                if (obj.create_date_from != "")
                {
                    create_date_from = DateTime.ParseExact(obj.create_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    create_date_to = DateTime.ParseExact(obj.create_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    create_date_from = null;

                    create_date_to = null;
                }

                Guid? system = null;
                if (obj.system_code != "")
                {
                    system = Guid.Parse(obj.system_code);
                }


                List<System.Data.SqlClient.SqlParameter> lst_param = new List<System.Data.SqlClient.SqlParameter>();

                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contract_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.contract_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.request_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@telephone_no", System.Data.SqlDbType.VarChar, 10) { Value = (obj.telephone_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@note", System.Data.SqlDbType.VarChar) { Value = (obj.note ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@action_code", System.Data.SqlDbType.VarChar) { Value = (obj.action_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@system", System.Data.SqlDbType.UniqueIdentifier) { Value = (object)system ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@related_dept_code", System.Data.SqlDbType.VarChar, 1000) { Value = (obj.related_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@result_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.related_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contact_date_from", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_from ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@contact_date_to", System.Data.SqlDbType.DateTime) { Value = (object)contact_date_to ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_date_from", System.Data.SqlDbType.DateTime) { Value = (object)create_date_from ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_date_to", System.Data.SqlDbType.DateTime) { Value = (object)create_date_to ?? DBNull.Value });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@PTP_Amount", System.Data.SqlDbType.Decimal) { Value = (obj.PTP_Amount != "" ? Convert.ToDecimal(obj.PTP_Amount) : (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@next_action_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_action_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@next_result_code", System.Data.SqlDbType.VarChar, 20) { Value = (obj.next_result_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@collector_code", System.Data.SqlDbType.VarChar, 50) { Value = (obj.collector_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@collector_team_code", System.Data.SqlDbType.VarChar, 10) { Value = (obj.collector_team_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_doc_flag", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_flag ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@request_doc_other", System.Data.SqlDbType.VarChar, 5) { Value = (obj.request_doc_other ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@note_dept_code", System.Data.SqlDbType.VarChar, 5) { Value = (obj.note_dept_code ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@tel_sms", System.Data.SqlDbType.VarChar, 30) { Value = (obj.tel_sms ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@create_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.create_by ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@update_by", System.Data.SqlDbType.VarChar, 30) { Value = (obj.update_by ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@payment_no", System.Data.SqlDbType.VarChar, 30) { Value = (obj.payment_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@PageNumber", System.Data.SqlDbType.Int) { Value = (page.page_no ?? (object)DBNull.Value) });
                lst_param.Add(new System.Data.SqlClient.SqlParameter("@RowsOfPage", System.Data.SqlDbType.Int) { Value = (page.page_size ?? (object)DBNull.Value) });


                DataTable dt_contact_note = _contactNoteRepo_ADO.Search(lst_param, StoredProcedureContractNoteCentralization.sp_inquiry_contact_note_web);

                List<InquiryWeb_contact_note> lst_cn = new List<InquiryWeb_contact_note>();

                foreach (DataRow row in dt_contact_note.Rows)
                {
                    InquiryWeb_contact_note obj_cn = new InquiryWeb_contact_note();

                    var contract = await _context.tbt_contract.Where(x => x.ContractNo == row["contract_no"].ToString()).FirstOrDefaultAsync();
                    var collector = await _context.tbm_collector.Where(x => x.collector_code == row["collector_code"].ToString()).FirstOrDefaultAsync();
                    var collector_team = await _context.tbm_collector_team.Where(x => x.team_code == row["collector_team_code"].ToString()).FirstOrDefaultAsync();
                    var System_code = await _context.tbm_master_system.Where(x => x.System_code == row["system"].ToString()).FirstOrDefaultAsync();

                    obj_cn.contact_note_id = row["contact_note_id"].ToString();
                    obj_cn.contract_no = row["contract_no"].ToString();
                    obj_cn.customer_name = contract != null ? contract.CustomerName : "";
                    obj_cn.system_name = System_code != null ? System_code.System : "";
                    obj_cn.system_code = row["system"].ToString();
                    obj_cn.request_no = row["request_no"].ToString();
                    obj_cn.telephone_no = row["telephone_no"].ToString();
                    obj_cn.note = row["note"].ToString();
                    obj_cn.action_code = row["action_code"].ToString();
                    obj_cn.related_dept_code = row["related_dept_code"].ToString();
                    obj_cn.result_code = row["result_code"].ToString();
                    obj_cn.contact_date = row["contact_date"].ToString();
                    obj_cn.remind_date = row["remind_date"].ToString();
                    obj_cn.PTP_Amount = row["PTP_Amount"].ToString();
                    obj_cn.next_action_code = row["next_action_code"].ToString();
                    obj_cn.next_result_code = row["next_result_code"].ToString();
                    obj_cn.collector_code = row["collector_code"].ToString();
                    obj_cn.collector_name = collector != null ? collector.collector_name : "";
                    obj_cn.collector_team_code = row["collector_team_code"].ToString();
                    obj_cn.collector_team_name = collector_team != null ? collector_team.team_name : "";
                    obj_cn.request_doc_flag = row["request_doc_flag"].ToString();
                    obj_cn.request_doc_other = row["request_doc_other"].ToString();
                    obj_cn.note_dept_code = row["note_dept_code"].ToString();
                    obj_cn.create_by = row["create_by"].ToString();
                    obj_cn.create_date = row["create_date"].ToString();
                    obj_cn.update_by = row["update_by"].ToString();
                    obj_cn.update_date = row["update_date"].ToString();
                    obj_cn.payment_no = row["payment_no"].ToString();
                    obj_cn.tel_sms = row["tel_sms"].ToString();

                    lst_cn.Add(obj_cn);
                }


                res.data = lst_cn;

                if (dt_contact_note.Rows.Count > 0)
                {
                    res.total_rows = Convert.ToInt64(dt_contact_note.Rows[0]["total_rows"].ToString());
                }

                res.page_no = page.page_no;

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }

        public async Task<bool> Insert(List<ContactNoteDto> lst_obj, string system_code, string request_ip, string transaction_id)
        {
            try
            {
                List<ContactNoteDao> lst_entity = new List<ContactNoteDao>();

                foreach (var obj in lst_obj)
                {
                    ContactNoteDao entity = new ContactNoteDao();
                    entity.contact_note_id = Guid.NewGuid();
                    entity.contract_no = obj.contract_no;
                    entity.request_no = obj.request_no;
                    entity.system = new Guid(system_code);
                    entity.telephone_no = obj.telephone_no;
                    entity.note = obj.note;
                    entity.action_code = obj.action_code;
                    entity.related_dept_code = obj.related_dept_code;
                    entity.result_code = obj.result_code;
                    entity.contact_date = DateTime.ParseExact(obj.contact_date.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    entity.remind_date = DateTime.ParseExact(obj.remind_date.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    entity.PTP_Amount = Convert.ToDecimal(obj.PTP_Amount);
                    entity.next_action_code = obj.next_action_code;
                    entity.next_result_code = obj.next_result_code;
                    entity.collector_code = obj.collector_code;
                    entity.collector_team_code = obj.collector_team_code;
                    entity.request_doc_flag = obj.request_doc_flag;
                    entity.request_doc_other = obj.request_doc_other;
                    entity.note_dept_code = obj.note_dept_code;
                    entity.create_by = obj.create_by;
                    entity.create_date = DateTime.ParseExact(obj.create_date.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    entity.update_by = obj.update_by;
                    entity.update_date = DateTime.ParseExact(obj.update_date.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    entity.payment_no = obj.payment_no;
                    entity.tel_sms = obj.tel_sms;
                    entity.ip_request = request_ip;
                    entity.create_date_system = DateTime.Now;
                    entity.transaction_id = transaction_id;

                    lst_entity.Add(entity);
                }

                try
                {
                    await _context.tbt_contact_note.AddRangeAsync(lst_entity);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {


                    // Rollback changes for the entities that caused the error
                    foreach (var entry in _context.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                        {
                            entry.State = EntityState.Unchanged;
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            entry.State = EntityState.Modified;
                            entry.State = EntityState.Unchanged;
                        }
                    }

                    throw ex;
                }



                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
                throw ex;
            }
        }




        public ContactNoteService(ContractNoteCentralizationDbContext _context)
        {
            this._context = _context;
        }

        public ContactNoteRes Search(ContactNoteReq req)
        {
            var res = new ContactNoteRes();
            try
            {
                using (var ctx = _context)
                {
                    res.payload = this.GetAll();
                    res.status_code = "00";
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        private List<ContactNoteDto> GetAll()
        {
            var payload = new List<ContactNoteDto>();
            try
            {
                using (var ctx = _context)
                {
                    long i = 1;
                    foreach (var item in ctx.tbt_contact_note)
                    {
                        ContactNoteDto one = new ContactNoteDto();

                        //one.Row_no  = item.
                        //one.Id  = item.
                        one.contract_no = item.contract_no;
                        // one.Customer_Name = "";
                        //one.Tel = item.telephone_no;
                        //one.Note = item.note;
                        one.contact_date = item.contact_date.ToString();
                        //one.Action_Code = item.action_code;
                        //one.Result_Code = item.result_code;
                        one.remind_date = item.remind_date.ToString();
                        // one.Payment_Card_No = item.payment_no;
                        // one.Dept_Code = item.note_dept_code;
                        one.related_dept_code = item.related_dept_code;
                        one.collector_code = item.collector_code;
                        // one.Collector_Name = "";
                        //one.Emp_Code = "";
                        //one.Employee_Name = item.
                        //one.Create_By_ID = item.
                        one.create_by = item.create_by;
                        //one.Contact_Type = item.
                        //one.Statement = item.
                        //one.Payment_Notification_Document = item.
                        //one.Copy_of_the_Register_Books = item.
                        //one.Bill_Pay_In = item.
                        //one.Payment_Confirmation = item.
                        //one.Transfer_of_Ownership_Document = item.
                        //one.Power_of_Attorney = item.
                        //one.Other  = item.
                        //one.Other1  = item.
                        one.tel_sms = item.tel_sms;
                        //one.Result_Code_Filter  = item.
                        //one.legal_act  = item.
                        //one.Actual_cc  = item.

                        one.create_date = item.create_date.ToString();



                        payload.Add(one);

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return payload;
        }

        public async Task<bool> InsertToIC5(List<ContactNoteDto> lst_obj, string system_code, string request_ip, string transaction_id)
        {
            try
            {
                foreach (var item in lst_obj)
                {
                    //if (IsResultCodeMustToDo(item))
                    if (item.result_code == "PTP")
                        SaveToIc5(item, system_code, request_ip, transaction_id);

                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private bool SaveToIc5(ContactNoteDto item, string system_code, string request_ip, string transaction_id)
        {
            try
            {
                string Oraconns = _configuration.GetConnectionString("Ic5Connection");

                string sql_insert = @$"INSERT INTO TMP_PTP_DETAIL  (ACCOUNTNO, PTP_DATE,PTP_AMT,DTUPTIME)  
                                  VALUES ('{item.contract_no}' 
                                        , TIMESTAMP'{item.remind_date}'
                                        , '{item.PTP_Amount}'
                                        , sysdate)";
                using (OracleConnection conn = new OracleConnection(Oraconns))
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    OracleCommand command = new OracleCommand(sql_insert, conn);


                    int i = command.ExecuteNonQuery();

                    


                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private bool IsResultCodeMustToDo(ContactNoteDto item) 
        {
            try
            {
                string conns = _configuration.GetConnectionString("ContractNoteCentralizationConnection");
                string query = @$"SELECT * FROM Dept  WHERE xxx ='{"xxx"}' ";
                using (var conn = new System.Data.SqlClient.SqlConnection(conns))
                {
                    DataSet ds = new DataSet();
                    var command = new System.Data.SqlClient.SqlCommand(query);
                    var da = new System.Data.SqlClient.SqlDataAdapter(command);
                    da.Fill(ds);

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        return true;
                    else
                        return false;   
                }
            }
            catch (Exception)
            {
                return false;
            }
        }




        // old verson InquiryWeb
        //public async Task<InquiryWebSvModel> InquiryWeb(InquiryContactNote_web obj, PageDetailReqModel_web page)
        //{
        //    try
        //    {

        //        var albums = _context.tbt_contact_note.FromSqlRaw<ContactNoteDao>("SELECT * from  tbt_contact_note    where  contract_no = '0501200431748' and [system] = '18cbe0cc-368e-4a76-96bb-39386542aa55'");


        //        InquiryWebSvModel res = new InquiryWebSvModel();

        //        DateTime contact_date_from = DateTime.Now;
        //        DateTime contact_date_to = DateTime.Now;

        //        if (obj.contact_date_from != "")
        //        {
        //            contact_date_from = DateTime.ParseExact(obj.contact_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        }

        //        if (obj.contact_date_to != "")
        //        {
        //            contact_date_to = DateTime.ParseExact(obj.contact_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //            contact_date_to = contact_date_to.Date.AddDays(1).AddTicks(-1);
        //        }



        //        DateTime create_date_from = DateTime.Now;
        //        DateTime create_date_to = DateTime.Now;

        //        if (obj.create_date_from != "")
        //        {
        //            create_date_from = DateTime.ParseExact(obj.create_date_from.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        }

        //        if (obj.create_date_to != "")
        //        {
        //            create_date_to = DateTime.ParseExact(obj.create_date_to.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //            create_date_to = create_date_to.Date.AddDays(1).AddTicks(-1);
        //        }

        //        IQueryable<ContactNoteDao> query0 = _context.tbt_contact_note;
        //        IQueryable<ContactNoteDao> filteredEntities = query0.Where(x =>
        //            (string.IsNullOrEmpty(obj.contract_no) || x.contract_no == obj.contract_no)
        //         && (string.IsNullOrEmpty(obj.request_no) || x.request_no == obj.request_no)
        //         && (string.IsNullOrEmpty(obj.telephone_no) || x.telephone_no == obj.telephone_no)
        //         && (string.IsNullOrEmpty(obj.note) || x.note == obj.note)
        //         && (string.IsNullOrEmpty(obj.action_code) || x.action_code == obj.action_code)
        //         && (string.IsNullOrEmpty(obj.system_code) || x.system == Guid.Parse(obj.system_code))
        //         && (string.IsNullOrEmpty(obj.related_dept_code) || x.related_dept_code == obj.related_dept_code)
        //         && (string.IsNullOrEmpty(obj.result_code) || x.result_code == obj.result_code)
        //         && (string.IsNullOrEmpty(obj.contact_date_from) || x.contact_date >= contact_date_from && x.contact_date < contact_date_to)
        //         && (string.IsNullOrEmpty(obj.create_date_from) || x.contact_date >= create_date_from && x.contact_date < create_date_to)
        //         && (string.IsNullOrEmpty(obj.PTP_Amount) || x.PTP_Amount == Convert.ToDecimal(obj.PTP_Amount))
        //         && (string.IsNullOrEmpty(obj.next_action_code) || x.next_action_code == obj.next_action_code)
        //         && (string.IsNullOrEmpty(obj.next_result_code) || x.next_result_code == obj.next_result_code)
        //         && (string.IsNullOrEmpty(obj.collector_code) || x.collector_code == obj.collector_code)
        //         && (string.IsNullOrEmpty(obj.collector_team_code) || x.collector_team_code == obj.collector_team_code)
        //         && (string.IsNullOrEmpty(obj.request_doc_flag) || x.request_doc_flag == obj.request_doc_flag)
        //         && (string.IsNullOrEmpty(obj.request_doc_other) || x.request_doc_other == obj.request_doc_other)
        //         && (string.IsNullOrEmpty(obj.note_dept_code) || x.note_dept_code == obj.note_dept_code)
        //         && (string.IsNullOrEmpty(obj.create_by) || x.create_by == obj.create_by)
        //         && (string.IsNullOrEmpty(obj.update_by) || x.update_by == obj.update_by)
        //         && (string.IsNullOrEmpty(obj.payment_no) || x.payment_no == obj.payment_no)
        //         && (string.IsNullOrEmpty(obj.tel_sms) || x.tel_sms == obj.tel_sms)
        //        );





        //        var query = _context.tbt_contact_note.Where(x =>
        //            (string.IsNullOrEmpty(obj.contract_no) || x.contract_no == obj.contract_no)
        //         && (string.IsNullOrEmpty(obj.request_no) || x.request_no == obj.request_no)
        //         && (string.IsNullOrEmpty(obj.telephone_no) || x.telephone_no == obj.telephone_no)
        //         && (string.IsNullOrEmpty(obj.note) || x.note == obj.note)
        //         && (string.IsNullOrEmpty(obj.action_code) || x.action_code == obj.action_code)
        //         && (string.IsNullOrEmpty(obj.system_code) || x.system == Guid.Parse(obj.system_code))
        //         && (string.IsNullOrEmpty(obj.related_dept_code) || x.related_dept_code == obj.related_dept_code)
        //         && (string.IsNullOrEmpty(obj.result_code) || x.result_code == obj.result_code)
        //         && (string.IsNullOrEmpty(obj.contact_date_from) || x.contact_date >= contact_date_from && x.contact_date < contact_date_to)
        //         && (string.IsNullOrEmpty(obj.create_date_from) || x.contact_date >= create_date_from && x.contact_date < create_date_to)
        //         && (string.IsNullOrEmpty(obj.PTP_Amount) || x.PTP_Amount == Convert.ToDecimal(obj.PTP_Amount))
        //         && (string.IsNullOrEmpty(obj.next_action_code) || x.next_action_code == obj.next_action_code)
        //         && (string.IsNullOrEmpty(obj.next_result_code) || x.next_result_code == obj.next_result_code)
        //         && (string.IsNullOrEmpty(obj.collector_code) || x.collector_code == obj.collector_code)
        //         && (string.IsNullOrEmpty(obj.collector_team_code) || x.collector_team_code == obj.collector_team_code)
        //         && (string.IsNullOrEmpty(obj.request_doc_flag) || x.request_doc_flag == obj.request_doc_flag)
        //         && (string.IsNullOrEmpty(obj.request_doc_other) || x.request_doc_other == obj.request_doc_other)
        //         && (string.IsNullOrEmpty(obj.note_dept_code) || x.note_dept_code == obj.note_dept_code)
        //         && (string.IsNullOrEmpty(obj.create_by) || x.create_by == obj.create_by)
        //         && (string.IsNullOrEmpty(obj.update_by) || x.update_by == obj.update_by)
        //         && (string.IsNullOrEmpty(obj.payment_no) || x.payment_no == obj.payment_no)
        //         && (string.IsNullOrEmpty(obj.tel_sms) || x.tel_sms == obj.tel_sms)
        //        );



        //        var sss = _context.tbt_contact_note.Where(x =>
        //             (string.IsNullOrEmpty(obj.contract_no) || x.contract_no == obj.contract_no)
        //          && (string.IsNullOrEmpty(obj.request_no) || x.request_no == obj.request_no)
        //          && (string.IsNullOrEmpty(obj.telephone_no) || x.telephone_no == obj.telephone_no)
        //          && (string.IsNullOrEmpty(obj.note) || x.note == obj.note)
        //          && (string.IsNullOrEmpty(obj.action_code) || x.action_code == obj.action_code)
        //          && (string.IsNullOrEmpty(obj.system_code) || x.system == Guid.Parse(obj.system_code))
        //          && (string.IsNullOrEmpty(obj.related_dept_code) || x.related_dept_code == obj.related_dept_code)
        //          && (string.IsNullOrEmpty(obj.result_code) || x.result_code == obj.result_code)
        //          && (string.IsNullOrEmpty(obj.contact_date_from) || x.contact_date >= contact_date_from && x.contact_date < contact_date_to)
        //          && (string.IsNullOrEmpty(obj.create_date_from) || x.contact_date >= create_date_from && x.contact_date < create_date_to)
        //          && (string.IsNullOrEmpty(obj.PTP_Amount) || x.PTP_Amount == Convert.ToDecimal(obj.PTP_Amount))
        //          && (string.IsNullOrEmpty(obj.next_action_code) || x.next_action_code == obj.next_action_code)
        //          && (string.IsNullOrEmpty(obj.next_result_code) || x.next_result_code == obj.next_result_code)
        //          && (string.IsNullOrEmpty(obj.collector_code) || x.collector_code == obj.collector_code)
        //          && (string.IsNullOrEmpty(obj.collector_team_code) || x.collector_team_code == obj.collector_team_code)
        //          && (string.IsNullOrEmpty(obj.request_doc_flag) || x.request_doc_flag == obj.request_doc_flag)
        //          && (string.IsNullOrEmpty(obj.request_doc_other) || x.request_doc_other == obj.request_doc_other)
        //          && (string.IsNullOrEmpty(obj.note_dept_code) || x.note_dept_code == obj.note_dept_code)
        //          && (string.IsNullOrEmpty(obj.create_by) || x.create_by == obj.create_by)
        //          && (string.IsNullOrEmpty(obj.update_by) || x.update_by == obj.update_by)
        //          && (string.IsNullOrEmpty(obj.payment_no) || x.payment_no == obj.payment_no)
        //          && (string.IsNullOrEmpty(obj.tel_sms) || x.tel_sms == obj.tel_sms)
        //         ).Skip((Convert.ToUInt16(page.page_no) - 1) * Convert.ToInt16(page.page_size))
        //         .Take(20)
        //         .ToList();


        //        var lst_entity = await query.Skip((Convert.ToUInt16(page.page_no) - 1) * Convert.ToInt16(page.page_size)).Take(Convert.ToInt16(page.page_size)).ToListAsync();
        //        var total_row = await query.CountAsync();


        //        List<InquiryWeb_contact_note> lst_cn = new List<InquiryWeb_contact_note>();

        //        foreach (ContactNoteDao cn in lst_entity)
        //        {
        //            InquiryWeb_contact_note obj_cn = new InquiryWeb_contact_note();

        //            var contract = await _context.tbt_contract.Where(x => x.ContractNo == cn.contract_no).FirstOrDefaultAsync();
        //            var collector = await _context.tbm_collector.Where(x => x.collector_code == cn.collector_code).FirstOrDefaultAsync();
        //            var collector_team = await _context.tbm_collector_team.Where(x => x.team_code == cn.collector_team_code).FirstOrDefaultAsync();
        //            var System_code = await _context.tbm_master_system.Where(x => x.System_code == cn.system.ToString()).FirstOrDefaultAsync();

        //            obj_cn.contact_note_id = cn.contact_note_id.ToString();
        //            obj_cn.contract_no = cn.contract_no;
        //            obj_cn.customer_name = contract != null ? contract.CustomerName : "";
        //            obj_cn.system_name = System_code != null ? System_code.System : "";
        //            obj_cn.system_code = cn.system.ToString();
        //            obj_cn.request_no = cn.request_no;
        //            obj_cn.telephone_no = cn.telephone_no;
        //            obj_cn.note = cn.note;
        //            obj_cn.action_code = cn.action_code;
        //            obj_cn.related_dept_code = cn.related_dept_code;
        //            obj_cn.result_code = cn.result_code;
        //            obj_cn.contact_date = cn.contact_date.HasValue ? cn.contact_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        //            obj_cn.remind_date = cn.remind_date.HasValue ? cn.remind_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty; ;
        //            obj_cn.PTP_Amount = cn.PTP_Amount.ToString();
        //            obj_cn.next_action_code = cn.next_action_code;
        //            obj_cn.next_result_code = cn.next_result_code;
        //            obj_cn.collector_code = cn.collector_code;
        //            obj_cn.collector_name = collector != null ? collector.collector_name : "";
        //            obj_cn.collector_team_code = cn.collector_team_code;
        //            obj_cn.collector_team_name = collector_team != null ? collector_team.team_name : "";
        //            obj_cn.request_doc_flag = cn.request_doc_flag;
        //            obj_cn.request_doc_other = cn.request_doc_other;
        //            obj_cn.note_dept_code = cn.note_dept_code;
        //            obj_cn.create_by = cn.create_by;
        //            obj_cn.create_date = cn.create_date.HasValue ? cn.create_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        //            obj_cn.update_by = cn.create_by;
        //            obj_cn.update_date = cn.update_date.HasValue ? cn.update_date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        //            obj_cn.payment_no = cn.payment_no;
        //            obj_cn.tel_sms = cn.tel_sms;



        //            lst_cn.Add(obj_cn);
        //        }



        //        res.data = lst_cn;
        //        res.total_rows = total_row;
        //        res.page_no = page.page_no;

        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError(ex, "{Repo} Insert Method error", typeof(ContactNoteService));
        //        throw ex;
        //    }
        //}


    }
}
