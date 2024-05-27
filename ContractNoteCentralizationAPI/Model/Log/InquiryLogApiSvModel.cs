using ContractNoteCentralizationAPI.Model.ContactNote;

namespace ContractNoteCentralizationAPI.Model.Log
{
    public class InquiryLogApiSvModel
    {
        public List<InquiryLogApiSv_data> data { get; set; }
        public string page_no { get; set; }
        public int total_rows { get; set; }
    }

    public class InquiryLogApiSv_data
    {
        public string id { get; set; }
        public string transaction_id { get; set; }
        public string method { get; set; }
        public string controller { get; set; }
        public string request_json { get; set; }
        public string response_json { get; set; }
        public string internal_status_code { get; set; }
        public string internal_status_desc { get; set; }
        public string http_status_code { get; set; }
        public string http_status_desc { get; set; }
        public string error_desc { get; set; }
        public string remark { get; set; }
        public string ip_request { get; set; }
        public string system_code { get; set; }
        public string system_name { get; set; }
        public string create_date { get; set; }
        public string update_date { get; set; }
    }


}
