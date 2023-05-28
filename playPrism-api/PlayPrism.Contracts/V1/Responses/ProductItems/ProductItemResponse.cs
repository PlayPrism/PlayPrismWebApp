using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.V1.Responses.ProductItems
{
    public class ProductItemResponse
    {
        public Guid Id { get; set; }

        public ProductResponse Product { get; set; }
        
        public string Value { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
