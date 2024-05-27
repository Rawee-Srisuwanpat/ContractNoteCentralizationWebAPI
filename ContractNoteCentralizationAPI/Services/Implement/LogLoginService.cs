using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class LogLoginService : ILogLoginService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        public LogLoginService(ContractNoteCentralizationDbContext _context)
        {
            this._context = _context;
        }


        public LogLoginRes Search(LogLoginReq req)
        {
            var res = new LogLoginRes();
            try
            {
                using (var ctx = _context)
                {
                    res.status_code = "00";
                    res.payload = this.GetAll();
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        private List<LogLginDto> GetAll()
        {
            var payload = new List<LogLginDto>();
            try
            {
                using (var ctx = _context)
                {
                    long i = 1;
                    foreach (var item in ctx.tbt_log_login)
                    {
                        LogLginDto one = new LogLginDto();
                        one.Id = item.Id;
                        one.user_name = item.user_name;
                        one.authenticate = ctx.tbm_master.FirstOrDefault(z => z.master_type == "008" && z.master_value == item.authenticate)?.master_text ?? "Register";

                        if (item.authenticate == "1")
                            one.email = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == item.user_name)?.email;
                        else if (item.authenticate == "2")
                            one.email = "";
                        else
                            one.email = ctx.tbt_register.FirstOrDefault(z => z.user_name == item.user_name).email;
                       
                        one.email = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == item.user_name)?.email;

                        //one.System_name = ctx.tbm_master_system.FirstOrDefault(z => z.Id.ToString() == item.system).System;

                        //one.request_date = item.request_date;
                        //one.RequestStatus = item.request_status;

                        //one.password = item.password;
                        //one.password_confirm = item.password;

                        //one.Status = item.is_active;
                        //one.Status_text = ctx.tbm_master.FirstOrDefault(z => z.master_type == "001" && z.master_value == item.is_active).master_text;


                        one.create_date = item.create_date.ToString();
                        one.create_by = item.create_by;
                        one.update_by = item.update_by;
                        one.update_date = item.update_date.ToString();

                        //one.RequestStatus_text = ctx.tbm_master.FirstOrDefault(z => z.master_type == "003" && z.master_value == item.request_status).master_text;


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
    }
}
