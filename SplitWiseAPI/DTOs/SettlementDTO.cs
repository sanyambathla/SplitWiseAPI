using SplitWiseAPI.Models;

namespace SplitWiseAPI.DTOs
{
    public class SettlementResponseDTO
    {
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public decimal Amount { get; set; }

        public SettlementResponseDTO(SettlementResult settlement)
        {
            FromUserName = settlement.FromUser.Name;
            ToUserName = settlement.ToUser.Name;
            Amount = settlement.Amount;
        }
    }
}