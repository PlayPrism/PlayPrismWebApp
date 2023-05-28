namespace PlayPrism.Contracts.V1.Responses.Orders
{
    public class HistoryItemResponse
    {
        public string ProductId { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public decimal Price { get; set; }

        public string HeaderImage { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Value { get; set; }
    }
}
