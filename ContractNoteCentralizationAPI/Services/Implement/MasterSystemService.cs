using AutoMapper;
using ContractNoteCentralizationAPI.Helper.AutoMapper;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.AccessControl;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class MasterSystemService : IMasterSystemService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        public MasterSystemService(ContractNoteCentralizationDbContext _context)
        {
            this._context = _context;
        }
        public MasterSystemRes Create(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All master_system
                    var all = ctx.tbm_master_system;

                    //var obj = all.FirstOrDefault(z => z.System == req.System);

                    var obj = all.FirstOrDefault(z => EF.Functions.Collate(z.System, "SQL_Latin1_General_CP1_CS_AS") ==  req.System);

                    

                    if (obj != null) throw new Exception("master_system have already created");

                    if (req.create_by.Contains("|"))
                    {

                        int position = req.create_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.create_by = req.create_by.Substring(0, position);
                        }

                    }

                    if (req.update_by.Contains("|"))
                    {

                        int position = req.update_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.update_by = req.update_by.Substring(0, position);
                        }

                    }



                    IMapper mapper = MapperConfig.InitializeAutomapper();
                    MasterSystemDto stud = mapper.Map<MasterSystemReq, MasterSystemDto>(req);

                    stud.System_code = Guid.NewGuid().ToString();

                    ctx.tbm_master_system.Add(stud);
                    ctx.SaveChanges();

                    res.status_code = "00";
                    res.payload = this.GetAllMasterSystem();

                 
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        public MasterSystemRes Delete(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All master_system
                    var all = ctx.tbm_master_system;

                    var obj = all.FirstOrDefault(z => z.Id == req.Id) ?? throw new Exception("master_system Id not found");

                    ctx.Remove(obj);


                    ctx.SaveChanges();

                    res.status_code = "00";
                    res.status_text = "OK";

                    res.payload = this.GetAllMasterSystem();
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        public MasterSystemRes Edit(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All master_system
                    var all = ctx.tbm_master_system;

                    var obj = all.FirstOrDefault(z => z.Id == req.Id) ?? throw new Exception("master_system Id not found");


                    //obj = all.FirstOrDefault(z => z.System == req.System && z.Id != req.Id);

                    //obj = all.FirstOrDefault(z => EF.Functions.Collate(z.System, "SQL_Latin1_General_CP1_CS_AS") == req.System && z.Id != req.Id) ;

                    //if (obj != null) 
                    //    throw new Exception("master_system is Duplicate");

                    //obj = all.FirstOrDefault(z => z.Id == req.Id);
                    //obj.System = req.System;
                    obj.Description = req.Description;
                    obj.status = req.status;
                    obj.update_by = req.update_by;
                    obj.update_date = req.update_date;

                    if (req.update_by.Contains("|"))
                    {

                        int position = req.update_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.update_by = req.update_by.Substring(0, position);
                        }

                    }

                    ctx.SaveChanges();

                    res.status_code = "00";
                    res.status_text = "OK";

                    res.payload = this.GetAllMasterSystem();
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        public MasterSystemRes SearchMasterSystem(MasterSystemReq req)
        {
            var res = new MasterSystemRes();
            try
            {
                using (var ctx = _context)
                {

                    res.status_code = "00";
                    res.payload = this.GetAllMasterSystem();
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        private List<MasterSystemDto> GetAllMasterSystem() 
        {
            List<MasterSystemDto> payload = new List<MasterSystemDto>();
            try
            {
                using (var ctx = _context) 
                {
                    long i = 1;
                    foreach (var item in ctx.tbm_master_system)
                    {
                        MasterSystemDto one = new MasterSystemDto();
                        one.Id = item.Id;
                        one.System = item.System;
                        one.Description = item.Description;
                        one.create_date = item.create_date;
                        one.create_by = item.create_by;
                        one.update_by = item.update_by;
                        one.update_date = item.update_date;


                        if (one.create_by.Contains("|"))
                        {
                            int position = one.create_by.IndexOf("|");
                            if (position != -1)
                            {
                                one.create_by = one.create_by.Substring(0, position) + " | " + one.create_date?.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        else
                        {
                            one.create_by = one.create_by + " | " + one.create_date?.ToString("yyyy-MM-dd HH:mm:ss");
                        }


                        if (one.update_by.Contains("|"))
                        {
                            int position = one.update_by.IndexOf("|");
                            if (position != -1)
                            {
                                one.update_by = one.update_by.Substring(0, position) + " | " + one.update_date?.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        else
                        {
                            one.update_by = one.update_by + " | " + one.update_date?.ToString("yyyy-MM-dd HH:mm:ss");
                        }



                        one.status = item.status;

                        one.System_code = item.System_code;

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
