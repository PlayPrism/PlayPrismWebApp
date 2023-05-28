namespace PlayPrism.Contracts.V1.Responses.Orders
{
    public class HistoryItemResponse
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public decimal Price { get; set; }

        public string HeaderImage { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Value { get; set; }
    }
}
