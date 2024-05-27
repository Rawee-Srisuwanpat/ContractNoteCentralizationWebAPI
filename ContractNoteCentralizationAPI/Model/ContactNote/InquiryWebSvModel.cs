namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class InquiryWebSvModel
    {
        public List<InquiryWeb_contact_note> data { get; set; }
        public string page_no { get; set; }
        public long total_rows { get; set; }

    }

    public class InquiryWeb_contact_note
    {
        
        public string contact_note_id { get; set; }
        public string contract_no { get; set; }
        public string customer_name { get; set; }
        public string request_no { get; set; }
        public string telephone_no { get; set; }
        public string note { get; set; }
        public string action_code { get; set; }
        public string related_dept_code { get; set; }
        public string result_code { get; set; }
        public string contact_date { get; set; }
        public string remind_date { get; set; }
        public string PTP_Amount { get; set; }
        public string next_action_code { get; set; }
        public string next_result_code { get; set; }
        public string collector_code { get; set; }
        public string collector_name { get; set; }
        public string collector_team_code { get; set; }
        public string collector_team_name { get; set; }
        public string request_doc_flag { get; set; }
        public string request_doc_other { get; set; }
        public string note_dept_code { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string payment_no { get; set; }
        public string tel_sms { get; set; }
        public string system_code { get; set; }
        public string system_name { get; set; }

    }


}
