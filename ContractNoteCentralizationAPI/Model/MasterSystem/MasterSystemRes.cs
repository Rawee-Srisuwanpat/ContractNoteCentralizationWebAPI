namespace ContractNoteCentralizationAPI.Model.MasterSystem
{
    public class MasterSystemRes
    {
        public string status_code { get; set; }
        public string status_text { get; set; }
        public List<MasterSystemDto>? payload { get; set; }
    }
}
