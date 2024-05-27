using ContractNoteCentralizationAPI.Model.Common;
using ContractNoteCentralizationAPI.Model.ContactNote;

namespace ContractNoteCentralizationAPI.Model.Log
{
    public class InquiryLogApiResModel
    {

        public InquiryLogApiResModel()
        {
            status = new StatusModel();
            data = new List<InquiryLogApiSv_data>();
            page = new PageDetailResModel_web();
        }
        public StatusModel status { get; set; }
        public List<InquiryLogApiSv_data> data { get; set; }
        public PageDetailResModel_web page { get; set; }

    }


}
