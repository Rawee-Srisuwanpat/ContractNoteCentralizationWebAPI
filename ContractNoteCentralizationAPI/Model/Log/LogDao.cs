using System;

namespace ContractNoteCentralizationAPI.Model.Log
{
    public class LogDao
    {
        public long id { get; set; }
        public string? transaction_id { get; set; }
        public string? method { get; set; }
        public string? controller { get; set; }
        public string? request_json { get; set; }
        public string? response_json { get; set; }
        public string? internal_status_code { get; set; }
        public string? internal_status_desc { get; set; }
        public string? http_status_code { get; set; }
        public string? http_status_desc { get; set; }
        public string? error_desc { get; set; }
        public string? remark { get; set; }
        public string? ip_request { get; set; }
        public Guid system_code { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? update_date { get; set; }

    }

}
