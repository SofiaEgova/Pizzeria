using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    [DataContract]
    public class PizzaIngredientViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PizzaId { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public string IngredientName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
