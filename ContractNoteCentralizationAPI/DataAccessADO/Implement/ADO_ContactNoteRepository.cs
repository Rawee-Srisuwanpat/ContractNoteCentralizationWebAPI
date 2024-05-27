using AutoMapper.Internal.Mappers;
using ContractNoteCentralizationAPI.DataAccess.Interface;
using ContractNoteCentralizationAPI.DataAccessADO.Utills;
using ContractNoteCentralizationAPI.Helper.DB;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;

using System.Data;
using System.Data.SqlClient;

namespace ContractNoteCentralizationAPI.DataAccessADO.Implement
{
    public class ADO_ContactNoteRepository
    {
        private readonly ConnectionStringsModel _connectionStringsModel;
        private readonly DBHelpers _dBHelpers;


        public ADO_ContactNoteRepository(DBHelpers _dBHelpers, ConnectionStringsModel _connectionStringsModel)
        {
            this._connectionStringsModel = _connectionStringsModel;
            this._dBHelpers = _dBHelpers;
        }
        public int Delete()
        {
            throw new NotImplementedException();
        }

        //public List<ContactNoteDao> Search(string contract_no, string request_no, string telephone_no
        //    , string note, string action_code, string related_dept_code
        //    , DateTime contact_date, DateTime remind_date, decimal PTP_Amount, string next_action_code
        //    , string next_result_code, string collector_code, string request_doc_flag, string request_doc_other, string note_dept_code
        //    , string system, string create_by, DateTime create_date, string update_by, DateTime update_date, string payment_no, string tel_sms)
        //{


        //    List<SqlParameter> lst_param = new List<SqlParameter>();

        //    lst_param.Add(new SqlParameter("@contract_no", contract_no));
        //    lst_param.Add(new SqlParameter("@request_no", request_no));
        //    lst_param.Add(new SqlParameter("@telephone_no", telephone_no));
        //    lst_param.Add(new SqlParameter("@note", note));
        //    lst_param.Add(new SqlParameter("@action_code", action_code));
        //    lst_param.Add(new SqlParameter("@remind_date", remind_date));
        //    lst_param.Add(new SqlParameter("@PTP_Amount", PTP_Amount));
        //    lst_param.Add(new SqlParameter("@next_action_code", next_action_code));
        //    lst_param.Add(new SqlParameter("@next_result_code", next_result_code));
        //    lst_param.Add(new SqlParameter("@collector_code", collector_code));
        //    lst_param.Add(new SqlParameter("@request_doc_flag", request_doc_flag));
        //    lst_param.Add(new SqlParameter("@request_doc_other", request_doc_other));
        //    lst_param.Add(new SqlParameter("@note_dept_code", note_dept_code));
        //    lst_param.Add(new SqlParameter("@system", system));
        //    lst_param.Add(new SqlParameter("@create_by", create_by));
        //    lst_param.Add(new SqlParameter("@create_date", create_date));
        //    lst_param.Add(new SqlParameter("@update_by", update_by));
        //    lst_param.Add(new SqlParameter("@update_date", update_date));
        //    lst_param.Add(new SqlParameter("@payment_no", payment_no));
        //    lst_param.Add(new SqlParameter("@tel_sms", tel_sms));



        //    DataTable dt = _dBHelpers.DBExecuteSP_ReturnDt(_connectionStringsModel.ContractNoteCentralizationConnection, StoredProcedureContractNoteCentralization.spInsertRegister, lst_param);
        //    return ObjectMapper.ConvertDataTableToList<ContactNoteDao>(dt);
        //}


        public DataTable Search(List<SqlParameter> lst_param, string sp_name)
        {
            DataTable dt = _dBHelpers.DBExecuteSP_ReturnDt(_connectionStringsModel.ContractNoteCentralizationConnection, sp_name, lst_param);
            //  return ObjectMapper.ConvertDataTableToList<ContactNoteIquiryDao>(dt);

            return dt;
        }

        public DataSet Insert()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
