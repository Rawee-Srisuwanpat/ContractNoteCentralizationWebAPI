using ContractNoteCentralizationAPI.Model.Common;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class InquiryResModel
    {

        public InquiryResModel()
        {
            status = new StatusModel();
            data = new List<ContactNoteDto>();
            page = new PageDetailResModel();
        }
        public StatusModel status { get; set; }
        public List<ContactNoteDto> data { get; set; }
        public PageDetailResModel page { get; set; }

    }

    public class PageDetailResModel
    {
        public string page_no { get; set; }
        public string total_rows { get; set; }
    }
}
