using ContractNoteCentralizationAPI.DataAccessADO.Utills;
using ContractNoteCentralizationAPI.Utills;
using System.ComponentModel.DataAnnotations;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class AddReqModel
    {
        public string transaction_id { get; set; }
        public List<AddContactNote> data { get; set; }
    }



    public class AddContactNote
    {
        [Required(ErrorMessage = "contract_no is required")]
        [StringLength(30, MinimumLength = 13, ErrorMessage = "contract_no length much be between 13 and 30 characters in length.")]
        public string contract_no { get; set; }


        [StringLength(10, MinimumLength = 0, ErrorMessage = "request_no must be no more than 10 characters.")]
        public string request_no { get; set; }


        [StringLength(10, MinimumLength = 0, ErrorMessage = "telephone_no must be no more than 10 characters.")]
        public string telephone_no { get; set; }



        [Required(ErrorMessage = "note is required")]
        [EmojiValidation]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "note length much be between 1 and 1000 characters in length.")]
        public string note { get; set; }



        [Required(ErrorMessage = "action_code is required")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "action_code length much be between 1 and 10 characters in length.")]
        public string action_code { get; set; }


        [StringLength(10, MinimumLength = 0, ErrorMessage = "related_dept_code must be no more than 10 characters.")]
        public string related_dept_code { get; set; }



        [Required(ErrorMessage = "result_code is required")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "result_code length much be between 1 and 10 characters in length.")]
        public string result_code { get; set; }



        [Required(ErrorMessage = "contact_date is required")]
        public string contact_date { get; set; }



        [Required(ErrorMessage = "remind_date is required")]
        public string remind_date { get; set; }



        [Required(ErrorMessage = "PTP_Amount must be a number or decimal.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "PTP_Amount must be a number or decimal.")]
        public string PTP_Amount { get; set; }


        [StringLength(20, MinimumLength = 0, ErrorMessage = "next_action_code must be no more than 20 characters.")]
        public string next_action_code { get; set; }


        [StringLength(20, MinimumLength = 0, ErrorMessage = "next_result_code must be no more than 20 characters.")]
        public string next_result_code { get; set; }


        [StringLength(50, MinimumLength = 0, ErrorMessage = "collector_code must be no more than 10 characters.")]
        public string collector_code { get; set; }

        
        [StringLength(10, MinimumLength = 0, ErrorMessage = "collector_team_code must be no more than 10 characters.")]
        public string collector_team_code { get; set; }


        [StringLength(5, MinimumLength = 0, ErrorMessage = "request_doc_flag must be no more than 5 characters.")]
        public string request_doc_flag { get; set; }


        [StringLength(5, MinimumLength = 0, ErrorMessage = "request_doc_other must be no more than 5 characters.")]
        public string request_doc_other { get; set; }


        [StringLength(5, MinimumLength = 0, ErrorMessage = "note_dept_code must be no more than 5 characters.")]
        public string note_dept_code { get; set; }


        [Required(ErrorMessage = "create_by is required")]
        public string create_by { get; set; }


        [Required(ErrorMessage = "create_date is required")]
        public string create_date { get; set; }


        [StringLength(30, MinimumLength = 0, ErrorMessage = "update_by must be no more than 30 characters.")]
        public string update_by { get; set; }


        public string update_date { get; set; }



        [StringLength(30, MinimumLength = 0, ErrorMessage = "payment_no must be no more than 30 characters.")]
        public string payment_no { get; set; }


        [StringLength(30, MinimumLength = 0, ErrorMessage = "tel_sms must be no more than 30 characters.")]
        public string tel_sms { get; set; }
    }



}
