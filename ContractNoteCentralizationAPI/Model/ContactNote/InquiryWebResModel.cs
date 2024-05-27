using ContractNoteCentralizationAPI.Model.Common;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class InquiryWebResModel
    {

        public InquiryWebResModel()
        {
            status = new StatusModel();
            data = new List<InquiryWeb_contact_note>();
            page = new PageDetailResModel_web();
        }
        public StatusModel status { get; set; }
        public List<InquiryWeb_contact_note> data { get; set; }
        public PageDetailResModel_web page { get; set; }

    }

    public class PageDetailResModel_web
    {
        public string page_no { get; set; }
        public string total_rows { get; set; }
    }
}
