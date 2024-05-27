using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.CollectorTeamCode;
using ContractNoteCentralizationAPI.Model.Master;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Model.ResultCode;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class MasterService : IMasterService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        public MasterService(ContractNoteCentralizationDbContext _context)
        {
            this._context = _context;
        }
        public MasterRes GetAll(MasterReq req)
        {
            var res= new MasterRes();
            try
			{
                using (var ctx = _context)
                {

                    res.status_code = "00";
                    res.payload = this.GetAllData();
                }
            }
			catch (Exception)
			{

				throw;
			}
            return res;
        }

        public ActionCodeRes GetAllActionCode(ActionCodeReq req)
        {
            var res = new ActionCodeRes();
            try
            {
                var payload = new List<ActionCodeDto>();
                using (var ctx = _context)
                {
                    var filter = ctx.tbm_action_code.Where(z => z.active_flag == 1);
                    foreach (var item in filter)
                    {
                        ActionCodeDto one = new ActionCodeDto();
                        //one.Id = item.id;

                        one.action_code = item.action_code;
                        one.description = item.description;
                        //one.master_text = item.master_text;
                        //one.master_value = item.master_value;
                        //one.orderList = item.orderList;

                        payload.Add(one);

                    }
                }
                res.status_code = "00";
                res.payload = payload;
            }
            catch (Exception)
            {
                throw;
            }
            return res; //  payload;
        }

        public CollectorCodeRes GetAllCollectorCode(CollectorCodeReq req)
        {
            var res = new CollectorCodeRes();
            try
            {
                var payload = new List<CollectorCodeDto>();
                using (var ctx = _context)
                {
                    var filter = ctx.tbm_collector.Where(z => z.active_flag == 1);
                    foreach (var item in filter)
                    {
                        CollectorCodeDto one = new CollectorCodeDto();
                        //one.Id = item.id;

                        one.collector_code = item.collector_code;
                        one.collector_name = item.collector_name;
                        //one.master_text = item.master_text;
                        //one.master_value = item.master_value;
                        //one.orderList = item.orderList;

                        payload.Add(one);

                    }
                }
                res.status_code = "00";
                res.payload = payload;
            }
            catch (Exception)
            {
                throw;
            }
            return res; //  payload;
        }

        public CollectorTeamCodeRes GetAllCollectorTeamCode(CollectorTeamCodeReq req)
        {
            var res = new CollectorTeamCodeRes();
            try
            {
                var payload = new List<CollectorTeamCodeDto>();
                using (var ctx = _context)
                {
                    var filter = ctx.tbm_collector_team.Where(z => z.active_flag == 1);
                    foreach (var item in filter)
                    {
                        CollectorTeamCodeDto one = new CollectorTeamCodeDto();
                        //one.Id = item.id;

                        one.team_code = item.team_code;
                        one.team_name = item.team_name;
                        //one.master_text = item.master_text;
                        //one.master_value = item.master_value;
                        //one.orderList = item.orderList;

                        payload.Add(one);

                    }
                }
                res.status_code = "00";
                res.payload = payload;
            }
            catch (Exception)
            {
                throw;
            }
            return res; //  payload;
        }

        public ResultCodeRes GetAllResultCode(ResultCodeReq req)
        {
            var res = new ResultCodeRes();
            try
            {
                var payload = new List<ResultCodeDto>();
                using (var ctx = _context)
                {
                    var filter = ctx.tbm_result_code.Where(z => z.active_flag == 1);
                    foreach (var item in filter)
                    {
                        var one = new ResultCodeDto();
                        //one.Id = item.id;

                        one.result_code = item.result_code;
                        one.description = item.description;
                        //one.master_text = item.master_text;
                        //one.master_value = item.master_value;
                        //one.orderList = item.orderList;

                        payload.Add(one);

                    }
                }
                res.status_code = "00";
                res.payload = payload;
            }
            catch (Exception)
            {
                throw;
            }
            return res; //  payload;
        }

        private List<MasterDto>? GetAllData()
        {
            List<MasterDto> payload = new List<MasterDto>();
            try
            {
                using (var ctx = _context)
                {
                    long i = 1;
                    foreach (var item in ctx.tbm_master)
                    {
                        MasterDto one = new MasterDto();
                        one.Id = item.id;
                     
                        one.master_type = item.master_type;
                        one.master_type_name = item.master_type_name;
                        one.master_text = item.master_text;
                        one.master_value = item.master_value;
                        one.orderList = item.orderList;

                        one.create_date = item.create_date;
                        one.create_by = item.create_by;
                        one.update_by = item.update_by;
                        one.update_date = item.update_date;


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
