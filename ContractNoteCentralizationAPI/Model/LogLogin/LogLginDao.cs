namespace ContractNoteCentralizationAPI.Model.LogLogin
{
    public class LogLginDao
    {
      public long Id { get ; set; }
      public string? user_name { get ; set; }
      public string? authenticate { get ; set; }
      public string? create_by { get ; set; }
      public DateTime? create_date { get ; set; }
      public string? update_by { get ; set; }
      public DateTime? update_date { get ; set; }
    }
}
