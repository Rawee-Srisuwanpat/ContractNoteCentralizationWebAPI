using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IManageRoleService
    {
        ManageRoleRes Create(ManageRoleReq req);

        ManageRoleRes Edit(ManageRoleReq req);

        ManageRoleRes Delete(ManageRoleDeleteReq req);

        ManageRoleRes Search(ManageRoleReq req);

        ManageRoleRes SearchRoleByUser(SearchRoleByUserReq req);
    }
}
