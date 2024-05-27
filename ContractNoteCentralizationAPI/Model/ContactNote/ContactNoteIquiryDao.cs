using System;
﻿using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class ContactNoteIquiryDao
    {
        public Guid contact_note_id { get; set; }
        public string? contract_no { get; set; }
        public string? request_no { get; set; }
        public string? telephone_no { get; set; }
        public string? note { get; set; }
        public string? action_code { get; set; }
        public string? related_dept_code { get; set; }
        public string? result_code { get; set; }
        public DateTime? contact_date { get; set; }
        public DateTime? remind_date { get; set; }
        public decimal? PTP_Amount { get; set; }
        public string? next_action_code { get; set; }
        public string? next_result_code { get; set; }
        public string? collector_code { get; set; }
        public string? collector_team_code { get; set; }      
        public string? request_doc_flag { get; set; }
        public string? request_doc_other { get; set; }
        public string? note_dept_code { get; set; }
        public Guid? system { get; set; }
        public string? create_by { get; set; }
        public DateTime? create_date { get; set; }
        public string? update_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? payment_no { get; set; }
        public string? tel_sms { get; set; }
        public string? ip_request { get; set; }
        public DateTime? create_date_system { get; set; }
        public string? transaction_id { get; set; }
        public int total_rows { get; set; }


    }
}
