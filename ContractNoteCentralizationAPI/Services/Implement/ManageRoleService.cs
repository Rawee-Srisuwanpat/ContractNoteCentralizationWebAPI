using AutoMapper;
using ContractNoteCentralizationAPI.Helper.AutoMapper;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class ManageRoleService : IManageRoleService
    {
        private readonly ContractNoteCentralizationDbContext _context;
        public ManageRoleService(ContractNoteCentralizationDbContext _context) {
            this._context = _context;
        }
        public ManageRoleRes Create(ManageRoleReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                var tmp_screen = new List<Screen>(req.screens);
                foreach (var item in req.screens)
                {
                    if (tmp_screen.Count == 1) break;
                    if (item.actions.Count == 0)
                        tmp_screen.Remove(item);
                }
                if (req.screens.Count == 0) throw new Exception("Please select one screen at least");
                foreach (var item in req.screens) 
                {
                    if (item.actions.Count == 0) {
                        throw new Exception($"Please select one action of {item.Screen_name} at least");
                    }
                }

                using (var ctx = _context)
                {

                    var role = ctx.tbm_role.FirstOrDefault(z => z.Role_name == req.role_name)  ;

                    if (role != null) throw new Exception("Role Name Duplicate");

                    var screen = ctx.tbm_master.Where(z => z.master_type == "006");


                    TbmRoleDoa tbmRoleDoa = new TbmRoleDoa();


                    tbmRoleDoa.Role_code = Guid.NewGuid().ToString();
                    tbmRoleDoa.Role_name = req.role_name;
                    tbmRoleDoa.description = req.Description;
                    tbmRoleDoa.is_active = req.is_active;

                    if (req.create_by.Contains("|")) { 

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

                    tbmRoleDoa.create_by = req.create_by;
                    tbmRoleDoa.update_by = req.update_by;
                    tbmRoleDoa.create_date = DateTime.Now;
                    tbmRoleDoa.update_date = DateTime.Now;

                    ctx.tbm_role.Add(tbmRoleDoa);

                    

                    foreach (var i in req.screens) 
                    {
                        string? screen_code = screen.FirstOrDefault(z => z.master_text == i.Screen_name).master_value ?? throw new Exception("screen not found");
                        ManageRoleDao manageRoleDao = new ManageRoleDao();
                        manageRoleDao.Id = 0;
                        manageRoleDao.Role_code = tbmRoleDoa.Role_code;
                        manageRoleDao.Screen_code = screen_code;

                        manageRoleDao.is_active = req.is_active;



                        manageRoleDao.view_data = i.actions.FirstOrDefault(z => z.Action_name == "View") != null ?  "1"  : null;
                        manageRoleDao.create_data = i.actions.FirstOrDefault(z => z.Action_name == "Save") != null ? "1" : null;
                        manageRoleDao.edit_data = i.actions.FirstOrDefault(z => z.Action_name == "Edit") != null ? "1" : null;
                        manageRoleDao.delete_data = i.actions.FirstOrDefault(z => z.Action_name == "Delete") != null ? "1" : null;
                        manageRoleDao.visible_menu = i.actions.FirstOrDefault(z => z.Action_name == "Visible") != null ? "1" : null;






                        ctx.tbt_manage_role.Add(manageRoleDao);

                    }


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

        public ManageRoleRes Delete(ManageRoleDeleteReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                using (var ctx = _context)
                {
                    var Has_User = ctx.tbt_manage_user.Where(x => x.role == req.Id.ToString());
                    if (Has_User.Count() > 0) {

                        //string userListName = string.Join(",", Has_User.Select(x => x.user_name));
                        throw new Exception($"Cannot Delete the role.Because there are users still use in the role.");
                    }


                    var role = ctx.tbm_role.FirstOrDefault(z => z.Id == req.Id);
                    var objListDelete = ctx.tbt_manage_role.Where(z => z.Role_code == role.Role_code);
                    foreach (var obj in objListDelete)
                    {
                        ctx.Remove(obj);
                    }

                    ctx.Remove(role);
                    ctx.SaveChanges();

                    res.payload = this.GetAll();

                    res.status_code = "00";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public ManageRoleRes Edit(ManageRoleReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                using (var ctx = _context)
                {
                    var screen = ctx.tbm_master.Where(z => z.master_type == "006");
                    var role = ctx.tbm_role.Where(z => z.Id == req.Id).FirstOrDefault();

                    role.description = req.Description;
                    role.is_active = req.is_active;
                    role.Role_name = req.role_name;


                    if (req.update_by.Contains("|"))
                    {

                        int position = req.update_by.IndexOf("|");
                        if (position != -1)
                        {
                            req.update_by = req.update_by.Substring(0, position);
                        }

                    }


                    role.update_by = req.update_by;
                    role.update_date = DateTime.Now;



                    foreach (var i in req.screens)
                    {
                        string? screen_code = screen.FirstOrDefault(z => z.master_text == i.Screen_name).master_value ?? throw new Exception("screen not found");
                        string? role_code = role.Role_code;

                        var manageRoleDao = ctx.tbt_manage_role.FirstOrDefault(z => z.Role_code == role_code && z.Screen_code == screen_code);

                        if (manageRoleDao == null)
                        {
                            manageRoleDao = new ManageRoleDao();
                            manageRoleDao.Id = 0;
                            manageRoleDao.Role_code = role_code;
                            manageRoleDao.Screen_code = screen_code;

                            manageRoleDao.is_active = req.is_active;

                            manageRoleDao.view_data = i.actions.FirstOrDefault(z => z.Action_name == "View") != null ? "1" : null;
                            manageRoleDao.create_data = i.actions.FirstOrDefault(z => z.Action_name == "Save") != null ? "1" : null;
                            manageRoleDao.edit_data = i.actions.FirstOrDefault(z => z.Action_name == "Edit") != null ? "1" : null;
                            manageRoleDao.delete_data = i.actions.FirstOrDefault(z => z.Action_name == "Delete") != null ? "1" : null;

                            manageRoleDao.visible_menu = i.actions.FirstOrDefault(z => z.Action_name == "Visible") != null ? "1" : null;



                            ctx.tbt_manage_role.Add(manageRoleDao);

                        }
                        else {

                            manageRoleDao.is_active = req.is_active;

                            manageRoleDao.view_data = i.actions.FirstOrDefault(z => z.Action_name == "View") != null ? "1" : null;
                            manageRoleDao.create_data = i.actions.FirstOrDefault(z => z.Action_name == "Save") != null ? "1" : null;
                            manageRoleDao.edit_data = i.actions.FirstOrDefault(z => z.Action_name == "Edit") != null ? "1" : null;
                            manageRoleDao.delete_data = i.actions.FirstOrDefault(z => z.Action_name == "Delete") != null ? "1" : null;

                            manageRoleDao.visible_menu = i.actions.FirstOrDefault(z => z.Action_name == "Visible") != null ? "1" : null;


                        }

                    }

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

        public ManageRoleRes Search(ManageRoleReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                using (var ctx = _context)
                {
                    res.status_code = "00";
                    res.payload = this.GetAll();

                    res.payload = res.payload.OrderByDescending(z => z.is_active).ThenByDescending(y => y.update_date).ToList() ;
                }
            }
            catch (Exception ex)
            {
                res.status_code = "99";
                res.status_text = ex.Message;
            }
            return res;
        }


        public ManageRoleRes SearchRoleByUser(SearchRoleByUserReq req)
        {
            var res = new ManageRoleRes();
            try
            {
                using (var ctx = _context)
                {
                    

                    string? roleId = ctx.tbt_manage_user.Where(z => z.user_name == req.user).FirstOrDefault().role;

                    res.payload = this.GetAll();

                    res.payload = res.payload.Where(z => z.Id_tbm_Role.ToString() == roleId).ToList();
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

        private List<ManageRoleDto> GetAll()
        {
            List<ManageRoleDto> payload = new List<ManageRoleDto>();
            try
            {
                using (var ctx = _context)
                {


                    var allRole = ctx.tbm_role;



                    foreach (var item in allRole)
                    {
                        ManageRoleDto one = new ManageRoleDto();
                        one.Id_tbm_Role = item.Id;
                        one.Role_code = item.Role_code;
                        one.Role_name = item.Role_name;
                        one.description = item.description;
                        one.is_active = item.is_active;
                        one.is_active_text = ctx.tbm_master.FirstOrDefault(z => z.master_type == "001" && z.master_value == item.is_active).master_text;

                        one.create_date = item.create_date?.ToString("yyyy-MM-dd HH:mm:ss");
                        one.update_date = item.update_date?.ToString("yyyy-MM-dd HH:mm:ss");
                        one.create_by = item.create_by;
                        one.update_by = item.update_by;


                        if (one.create_by.Contains("|"))
                        {
                            int position = one.create_by.IndexOf("|");
                            if (position != -1)
                            {
                                one.create_by = one.create_by.Substring(0, position) + " | " + one.create_date;
                            }
                        }
                        else {
                            one.create_by = one.create_by + " | " + one.create_date;
                        }


                        if (one.update_by.Contains("|"))
                        {
                            int position = one.update_by.IndexOf("|");
                            if (position != -1)
                            {
                                one.update_by = one.update_by.Substring(0, position) + " | " + one.update_date;
                            }
                        }
                        else
                        {
                            one.update_by = one.update_by + " | " + one.update_date;
                        }


                        one.screens = new List<Screen>();


                        foreach (var item2 in ctx.tbt_manage_role.Where(z => z.Role_code == item.Role_code ))
                        {
                            Screen screen = new Screen();
                            screen.Screen_name = ctx.tbm_master.FirstOrDefault(z => z.master_type == "006" && z.master_value == item2.Screen_code).master_text;
                            screen.actions = new List<Model.ManageRole.Action>();

                            

                            if (item2.view_data == "1")
                            {
                                Model.ManageRole.Action action = new Model.ManageRole.Action();
                                action.Action_name = "View";
                                action.Row_no = 1;
                                screen.actions.Add(action);

                            }

                            if (item2.create_data == "1")
                            {
                                Model.ManageRole.Action action = new Model.ManageRole.Action();
                                action.Action_name = "Save";
                                action.Row_no = 2;
                                screen.actions.Add(action);

                            }

                            if (item2.edit_data == "1")
                            {
                                Model.ManageRole.Action action = new Model.ManageRole.Action();
                                action.Action_name = "Edit";
                                action.Row_no = 3;
                                screen.actions.Add(action);

                            }

                            if (item2.delete_data == "1")
                            {
                                Model.ManageRole.Action action = new Model.ManageRole.Action();
                                action.Action_name = "Delete";
                                action.Row_no = 4;
                                screen.actions.Add(action);

                            }



                            if (item2.visible_menu == "1")
                            {
                                Model.ManageRole.Action action = new Model.ManageRole.Action();
                                action.Action_name = "Visible";
                                action.Row_no = 5;
                                screen.actions.Add(action);

                            }

                            one.screens.Add(screen);

                        }












                        //one.view_data = item.view_data;

                        //one.Screen_text = "";
                        //one.Screen_code = item.Screen_code;

                        //one.delete_data = item.delete_data;



                        //one.create_date = item.create_date;
                        //one.create_by = item.create_by;
                        //one.update_by = item.update_by;
                        //one.update_date = item.update_date;



                        payload.Add(one);

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
