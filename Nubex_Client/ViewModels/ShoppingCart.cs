using Nubex_Models;

namespace Nubex_Client.ViewModels
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPremiumDTO ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
