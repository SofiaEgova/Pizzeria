using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class FridgeIngredient
    {
        public int Id { get; set; }

        public int FridgeId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }

        public virtual Fridge Fridge { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
