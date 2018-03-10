using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class Fridge
    {
        public int Id { get; set; }

        [Required]
        public string FridgeName { get; set; }

        [ForeignKey("FridgeId")]
        public virtual List<FridgeIngredient> FridgeIngredients { get; set; }
    }
}
