namespace UseCase2.Models
{
    public class StripeBalanceResponse
    {
        public string? Object { get; set; }
        public List<BalanceDetail>? Available { get; set; }
        public List<BalanceDetail>? ConnectReserved { get; set; }
        public bool Livemode { get; set; }
        public List<BalanceDetail>? Pending { get; set; }
    }

    public class BalanceDetail
    {
        public long Amount { get; set; }
        public string? Currency { get; set; }
        public SourceTypes? SourceTypes { get; set; }
    }

    public class SourceTypes
    {
        public long Card { get; set; }
    }
}