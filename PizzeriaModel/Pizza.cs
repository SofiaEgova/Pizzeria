using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string PizzaName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("PizzaId")]
        public virtual List<OrderPizza> OrderPizzas { get; set; }

        [ForeignKey("PizzaId")]
        public virtual List<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
