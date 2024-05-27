using ContractNoteCentralizationAPI.Model.MasterSystem;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IMasterSystemService
    {
        MasterSystemRes Create(MasterSystemReq req);

        MasterSystemRes Edit(MasterSystemReq req);

        MasterSystemRes Delete(MasterSystemReq req);

        MasterSystemRes SearchMasterSystem(MasterSystemReq req);

    }
}
