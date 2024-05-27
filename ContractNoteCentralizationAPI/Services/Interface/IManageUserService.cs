using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.ManageUser;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IManageUserService
    {
        ManageUserRes Create(ManageUserReq req);

        ManageUserRes Edit(ManageUserReq req);

        ManageUserRes Delete(ManageUserDeleteReq req);

        ManageUserRes Search(ManageUserReq req);
    }
}
