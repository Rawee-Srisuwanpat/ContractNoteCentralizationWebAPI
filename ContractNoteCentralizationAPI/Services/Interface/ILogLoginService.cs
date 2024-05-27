using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Model.ManagerRegister;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface ILogLoginService
    {
        LogLoginRes Search(LogLoginReq req);
    }
}
