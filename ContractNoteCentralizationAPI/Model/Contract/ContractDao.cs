namespace ContractNoteCentralizationAPI.Model.Contract
{
    public class ContractDao
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set;}
        public DateTime? UpdatedDate { get; set;}
        public string? UpdatedBy { get;}
        public bool IsActive { get; set;}
        public string? ContractNo { get; set;}
        public string? CustomerName { get; set;}

    }
}
