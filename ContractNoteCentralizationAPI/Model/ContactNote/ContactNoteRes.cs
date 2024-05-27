using ContractNoteCentralizationAPI.Model.LogLogin;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class ContactNoteRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<ContactNoteDto>? payload { get; set; }
    }
}
