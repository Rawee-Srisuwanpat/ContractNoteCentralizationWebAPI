namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class InquirySvModel
    {
        public List<ContactNoteDto> data { get; set; }
        public string page_no { get; set; }
        public long total_rows { get; set; }
    }
}
