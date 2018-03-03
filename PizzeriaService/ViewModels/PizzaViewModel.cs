using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }

        public string PizzaName { get; set; }

        public decimal Price { get; set; }

        public List<PizzaIngredientViewModel> PizzaIngredients { get; set; }
    }
}
