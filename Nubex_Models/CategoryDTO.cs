using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nubex_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        [MinLength(3,ErrorMessage ="Minimum Char is 3")]
        public string Name { get; set; }
    }
}
