using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nubex_DataAccess
{
    public class ProductPremium
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string Condition { get; set; }
        public double PriceAdd { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public string? CreateBy { get; set; }
    }
}
