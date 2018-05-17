using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class Visitor
    {
        public int Id { get; set; }

        public string Mail { get; set; }

        [Required]
        public string VisitorFIO { get; set; }

        [ForeignKey("VisitorId")]
        public virtual List<OrderPizza> OrderPizzas { get; set; }
    }
}
