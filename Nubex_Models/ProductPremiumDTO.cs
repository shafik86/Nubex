using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nubex_Models
{
    public class ProductPremiumDTO
    {

        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Product")]
        public int ProductId { get; set; }
        [Required]
        public string Condition { get; set; }
        public double PriceAdd { get; set; } = 0;
        public double? Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreateBy { get; set; }

        
    }
}
