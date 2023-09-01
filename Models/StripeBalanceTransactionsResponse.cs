namespace UseCase2.Models
{
    public class StripeBalanceTransactionsResponse
    {
        public string Object { get; set; } = default!;
        public List<StripeBalanceTransaction> Data { get; set; } = new List<StripeBalanceTransaction>();
        public bool HasMore { get; set; }
        public string Url { get; set; } = default!;
    }

    public class StripeBalanceTransaction
    {
        public string Id { get; set; } = default!;
        public string Object { get; set; } = default!;
        public long Amount { get; set; }
        public long AvailableOn { get; set; }
        public long Created { get; set; }
        public string Currency { get; set; } = default!;
        public string? Description { get; set; }
        public double? ExchangeRate { get; set; }
        public long Fee { get; set; }
        public List<FeeDetail> FeeDetails { get; set; } = new List<FeeDetail>();
        public long Net { get; set; }
        public string ReportingCategory { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Type { get; set; } = default!;
    }

    public class FeeDetail
    {
        public long Amount { get; set; }
        public string? Application { get; set; }
        public string Currency { get; set; } = default!;
        public string? Description { get; set; }
        public string Type { get; set; } = default!;
    }

}
