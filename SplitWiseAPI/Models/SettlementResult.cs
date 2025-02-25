namespace SplitWiseAPI.Models
{
    public class SettlementResult
    {
        public User FromUser { get; set; }           
        public User ToUser { get; set; }             
        public decimal Amount { get; set; }
    }
}
