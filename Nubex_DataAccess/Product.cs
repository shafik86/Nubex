using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nubex_DataAccess
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string? ProductSKU { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public double MetalWeight { get; set; }
        public string? MetalBrand { get; set; }
        public bool IsHighlighted { get; set; }
        public double Weight { get; set; }
        public double Purify { get; set; }
        public string? Manufacture { get; set; }
        public string? Certificate { get; set; }
        public bool IsTax { get; set; } = false;
        public string? Featured { get; set; }
        //public double? Price { get; set; }
        public string? Color { get; set; }
        public double? Size { get; set; }
        public string? ProductTag { get; set; }
        public string Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        //public int Quantity { get; set; } = 0;
        public string? remark_1 { get; set; }
        public string? remark_2 { get; set; }
        public string? remark_3 { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "Administrators";
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; } = "Admin";
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<ProductPremium> ProductPrices { get; set; }

    }
}
