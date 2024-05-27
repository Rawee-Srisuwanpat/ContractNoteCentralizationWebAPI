using AutoMapper;
using ContractNoteCentralizationAPI.Helper.AD;
using ContractNoteCentralizationAPI.Helper.AutoMapper;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Services.Interface;
using System.DirectoryServices.AccountManagement;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class ManageUserService : IManageUserService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        private readonly PrincipalModel _principal;
        public ManageUserService(ContractNoteCentralizationDbContext _context, PrincipalModel principal)
        {
            this._context = _context;
            this._principal = principal;
        }
        public ManageUserRes Create(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All User
                    var all = ctx.tbt_manage_user;

                    var user = all.FirstOrDefault(z => z.user_name == req.user_name);

                    if (user != null) throw new Exception("User have already created");

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
                    ManageUserDao stud = mapper.Map<ManageUserReq, ManageUserDao>(req);

                    if (req.authenticate == "2")
                    {
                        stud.email = GetEmailFromAD(req.user_name).ToUpper();
                        stud.password = null;
                        stud.authenticate = "2";
                        stud.system_code = "280EE861-B289-443D-8EB1-A43D364124EE"; // AD
                    }
                    else {
                        stud.email = req.email.ToUpper();

                    }

                    stud.user_name = req.user_name.ToUpper();

                    


                    ctx.tbt_manage_user.Add(stud);
                    ctx.SaveChanges();

                    res.status_code = "00";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public ManageUserRes Delete(ManageUserDeleteReq req)
        {
            var res = new ManageUserRes();
            try
            {
                using (var ctx = _context)
                {
                    var all = ctx.tbt_manage_user;
                    var user = all.FirstOrDefault(z => z.id == req.Id) ?? throw new Exception("User not found");

                    ctx.Remove(user);
                    ctx.SaveChanges();
                    res.status_code = "00";
                    res.status_text = "OK";

                    res.payload = this.GetAll();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public ManageUserRes Edit(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All User
                    var all = ctx.tbt_manage_user;

                    var user = all.FirstOrDefault(z => z.id == req.Id) ?? throw new Exception("User not found");

                    user.email = req.email;
                    user.role = req.role;

                    if (req.update_by.Contains("|"))
                    {

                        int position = req.update_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.update_by = req.update_by.Substring(0, position);
                        }

                    }



                    user.update_by = req.update_by;
                    user.update_date = DateTime.Now;

                    user.password = req.password;
                    user.authenticate = req.authenticate;
                    user.status = req.status;
                    user.system_code = req.system_code;
                    user.request_status = req.request_status;




                    ctx.SaveChanges();

                    res.payload = this.GetAll();


                    res.status_code = "00";
                    res.status_text = "OK";

                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public ManageUserRes Search(ManageUserReq req)
        {
            var res = new ManageUserRes();
            try
            {
                using (var ctx = _context)
                {
                    res.status_code = "00";

                    if (req.authenticate == "1")
                    {
                        res.payload = this.GetAll().Where(a => a.authenticate == "1").OrderBy(z => z.request_status).ThenByDescending(y => y.create_date).ToList();
                    }
                    else if (req.authenticate == "2") {
                        res.payload = this.GetAll().Where(a => a.authenticate == "2").OrderByDescending(z => z.status).ThenByDescending(y => y.update_date).ToList();
                    }
                    else 
                        res.payload = this.GetAll().OrderByDescending(z => z.status).ThenByDescending(y => y.update_date).ToList();
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }

        public List<ManageUserDto> GetAll()
        {
            var payload = new List<ManageUserDto>();
            try
            {
                using (var ctx = _context)
                {
                    long i = 1;
                    foreach (var item in ctx.tbt_manage_user)
                    {
                        var one = new ManageUserDto();
                        one.Id = item.id;
                        one.UserName = item.user_name;
                        one.Email = item.email;

                        one.RoleId = item.role;

                        one.RoleName = ctx.tbm_role.FirstOrDefault(z => z.Id.ToString() == item.role)?.Role_name ?? "Default";

                        one.password = item.password;

                        one.authenticate = item.authenticate;

                        one.status = item.status;

                        one.statusText = ctx.tbm_master.FirstOrDefault(z => z.master_type == "001" && z.master_value == item.status).master_text;

                        one.request_status = item.request_status;
                        one.request_status_text = ctx.tbm_master.FirstOrDefault(z => z.master_type == "003" && z.master_value == item.request_status)?.master_text;


                        one.system = item.system_code;
                        one.systemName = ctx.tbm_master_system.FirstOrDefault(z => z.System_code == item.system_code)?.System;


                        //var group = ctx.tbt_manage_role
                        //          .GroupBy(x => new { x.Role_code, x.description, x.is_active })
                        //          .Select(g => new
                        //          {
                        //              Role_name = g.Key.Role_code,
                        //              is_active = g.Key.is_active,
                        //          });
                        // one.RoleName = group.FirstOrDefault(z => z.Id.ToString() == item.Role).Role_name;


                        one.create_date = item.create_date;
                        one.create_by = item.create_by;
                        one.update_by = item.update_by;
                        one.update_date = item.update_date;

                        one.request_date_text = item.create_date?.ToString("yyyy-MM-dd HH:mm:ss");

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

        private string GetEmailFromAD(string username) 
        {
            string? email = string.Empty;
            try
            {
                var Domain = _principal.domain;
                var UserAdmin = _principal.userAdmin;
                var PassAdmin = _principal.passAdmin;

                bool isValid = false;
                UserPrincipal usr;

                PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain, null, UserAdmin, PassAdmin);


                usr = UserPrincipal.FindByIdentity(pc, username) ?? throw new Exception($"Not Found User Name ({username}) on AD.");


                email = usr.EmailAddress;
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return email;
        }


        public ManageUserRes edit_refresh_token(ManageUserReq req, string refresh_token)
        {
            var res = new ManageUserRes();
            try
            {
                using (var ctx = _context)
                {
                    // Get All User
                    var all = ctx.tbt_manage_user;

                    var user = all.FirstOrDefault(z => z.id == req.Id) ?? throw new Exception("User not found");

                    user.email = req.email;
                    user.role = req.role;

                    if (req.update_by.Contains("|"))
                    {

                        int position = req.update_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.update_by = req.update_by.Substring(0, position);
                        }

                    }


                    user.update_by = req.update_by;
                    user.update_date = DateTime.Now;

                    user.password = req.password;
                    user.authenticate = req.authenticate;
                    user.status = req.status;
                    user.system_code = req.system_code;
                    user.request_status = req.request_status;


                    ctx.SaveChanges();

                    res.payload = this.GetAll();


                    res.status_code = "00";
                    res.status_text = "OK";


                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }
    }
}
