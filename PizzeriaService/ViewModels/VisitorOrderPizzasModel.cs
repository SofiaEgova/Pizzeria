using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    [DataContract]
    public class VisitorOrderPizzasModel
    {
        [DataMember]
        public string VisitorName { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string PizzaName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
