using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.Log;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface ILogService
    {
        Task<LogDao> Insert(LogDao obj);
        Task<bool> Update(LogDao obj);
        Task<InquiryLogApiSvModel> Inquiry(InquiryLogApiReq_data obj, PageDetailReqModel_web page);
    }
}
