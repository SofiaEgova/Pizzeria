using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.BindingModels
{
    public class FridgeIngredientBindingModel
    {
        public int Id { get; set; }

        public int FridgeId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
