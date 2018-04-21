using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    [DataContract]
    public class FridgeViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FridgeName { get; set; }

        [DataMember]
        public List<FridgeIngredientViewModel> FridgeIngredients { get; set; }
    }
}
