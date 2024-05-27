using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.CollectorTeamCode;
using ContractNoteCentralizationAPI.Model.Master;
using ContractNoteCentralizationAPI.Model.ResultCode;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IMasterService
    {
        MasterRes GetAll(MasterReq req);

        ActionCodeRes GetAllActionCode(ActionCodeReq req);

        ResultCodeRes GetAllResultCode(ResultCodeReq req);

        CollectorTeamCodeRes GetAllCollectorTeamCode(CollectorTeamCodeReq req);

        CollectorCodeRes GetAllCollectorCode(CollectorCodeReq req);
    }
}
