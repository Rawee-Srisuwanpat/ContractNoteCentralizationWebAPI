using ContractNoteCentralizationAPI.Model.ContactNote;

namespace ContractNoteCentralizationAPI.Model.Log
{
    public class InquiryLogApiReqModel
    {
        public string transaction_id { get; set; }
        public InquiryLogApiReq_data data { get; set; }
        public PageDetailReqModel_web page { get; set; }
    }

    public class InquiryLogApiReq_data
    {
        public string transaction_id { get; set; }
        public string method { get; set; }
        public string controller { get; set; }
        public string internal_status_code { get; set; }
        public string http_status_code { get; set; }
        public string ip_request { get; set; }
        public string system_code { get; set; }
        public string create_date { get; set; }
        public string update_date { get; set; }
    }


}
