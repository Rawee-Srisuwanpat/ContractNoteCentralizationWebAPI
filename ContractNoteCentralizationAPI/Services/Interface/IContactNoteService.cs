using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.LogLogin;

namespace ContractNoteCentralizationAPI.Services.Interface
{
    public interface IContactNoteService
    {
        Task<bool> Insert(List<ContactNoteDto> lst_obj, string system_code, string request_ip, string transaction_id);

        Task<bool> InsertToIC5(List<ContactNoteDto> lst_obj, string system_code, string request_ip, string transaction_id);

        //Task<InquirySvModel> Inquiry(ContactNoteDto obj, PageDetailReqModel page);
        Task<InquirySvModel> Inquiry(InquiryContactNote obj, PageDetailReqModel page);
        Task<InquiryWebSvModel> InquiryWeb(InquiryContactNote_web obj, PageDetailReqModel_web page);
        Task<InquirySvModel> Inquiry_ADO(InquiryContactNote obj, PageDetailReqModel page);
        Task<InquiryWebSvModel> InquiryWeb_ADO(InquiryContactNote_web obj, PageDetailReqModel_web page);
        ContactNoteRes Search(ContactNoteReq req);
    }
}
