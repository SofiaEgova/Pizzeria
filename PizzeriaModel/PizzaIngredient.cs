using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class PizzaIngredient
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
