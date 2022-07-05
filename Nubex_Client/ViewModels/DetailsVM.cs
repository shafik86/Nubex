using Nubex_Models;
using System.ComponentModel.DataAnnotations;

namespace Nubex_Client.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            ProductPrice = new();
            Count = 1;
        }
        [Range(1, int.MaxValue, ErrorMessage ="Please enter greater value")]
        public int Count { get; set; }
        [Required]
        public int SelectedProductPremiumId { get; set; }
        public ProductPremiumDTO  ProductPrice { get; set; }
    }
}
