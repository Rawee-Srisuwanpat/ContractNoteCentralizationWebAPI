using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class InquiryReqModel
    {
        public string transaction_id { get; set; }
        public InquiryContactNote data { get; set; }
        public PageDetailReqModel page { get; set; }
    }

    public class PageDetailReqModel
    {
        [Required(ErrorMessage = "page_no  is required")]
        public string page_no { get; set; }

        
        [Required(ErrorMessage = "page_size is required")]
        [Range(1, 20, ErrorMessage = "page_size  range must be  between 1 and 20.")]
        public string page_size { get; set; }
    }

    public class InquiryContactNote
    {
        public string contract_no { get; set; }
        public string request_no { get; set; }
        public string telephone_no { get; set; }
        public string note { get; set; }
        public string action_code { get; set; }
        public string related_dept_code { get; set; }
        public string result_code { get; set; }
        // public string contact_date { get; set; }
        public string remind_date { get; set; }
        public string PTP_Amount { get; set; }
        public string next_action_code { get; set; }
        public string next_result_code { get; set; }
        public string collector_code { get; set; }
        public string collector_team_code { get; set; }
        public string request_doc_flag { get; set; }
        public string request_doc_other { get; set; }
        public string note_dept_code { get; set; }
        public string create_by { get; set; }
        // public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string payment_no { get; set; }
        public string tel_sms { get; set; }
        public string system_code { get; set; }
        public string create_date_from { get; set; }
        public string create_date_to { get; set; }
        public string contact_date_from { get; set; }
        public string contact_date_to { get; set; }
    }
}
