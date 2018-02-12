using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.BindingModels
{
    public class PizzaBindingModel
    {
        public int Id { get; set; }

        public string PizzaName { get; set; }

        public decimal Price { get; set; }

        public List<PizzaIngredientBindingModel> PizzaIngredients { get; set; }
    }
}
