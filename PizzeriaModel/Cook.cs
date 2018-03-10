using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class Cook
    {
        public int Id { get; set; }

        [Required]
        public string CookFIO { get; set; }

        [ForeignKey("CookId")]
        public virtual List<OrderPizza> OrderPizzas { get; set; }
    }
}
