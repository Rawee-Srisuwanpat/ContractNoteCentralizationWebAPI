using System.ComponentModel;

namespace ContractNoteCentralizationAPI.Model.ChatBot
{
    public class ContractDetailDao
    {
        public long id { get; set; }
        public string product_type { get; set; }
        public string request_no { get; set; }
        public string contract_no { get; set; }
        public string? license { get; set; }
        public DateTime? contract_date { get; set; }
        public string full_name { get; set; }
        public DateTime? tax_expiration_date { get; set; }
        public DateTime? compulsory_policy_expiration_date { get; set; }
        public decimal? installment_amt { get; set; }
        public decimal? contract_balance { get; set; }
        public string? close_status { get; set; }
        public string? id_card { get; set; }
        public string? mobile { get; set; }
        public string? barcode_01 { get; set; }
        public string? barcode_01_text { get; set; }
        public string? barcode_02 { get; set; }
        public string? barcode_02_text { get; set; }
        public string? barcode_03 { get; set; }
        public string? barcode_03_text { get; set; }
        public string? collection_status { get; set; }
        public DateTime? end_due_date { get; set; }
        public string? mob { get; set; }
        public string? contract_amt { get; set; }
        public string? overdue_amt { get; set; }
        public string? tax_amt { get; set; }
        public string? compulsory_amt { get; set; }
        public DateTime? next_due_date { get; set; }
        public string? license_no { get; set; }
        public DateTime? birthdate { get; set; }
        public string? contract_status { get; set; }
        public int? act_curr_term { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public decimal? collection_fee { get; set; }
        public decimal? penalty_fee { get; set; }
        public decimal? advance_amt { get; set; }
        public decimal? df_credit_usage_charge_amt { get; set; }
        public decimal? df_interest_amt { get; set; }
        public decimal? expected_total_remain_on_due_amt { get; set; }
        public decimal? remain_installment_amt { get; set; }
        public string? payment_card_no { get; set; }
        public string? province { get; set; }
        public string? email_address { get; set; }
        public string? estatement_channel { get; set; }
        public string? person_type_code { get; set; }

    }
}
