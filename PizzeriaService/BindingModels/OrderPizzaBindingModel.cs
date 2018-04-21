using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.BindingModels
{
    [DataContract]
    public class OrderPizzaBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int VisitorId { get; set; }

        [DataMember]
        public int PizzaId { get; set; }

        [DataMember]
        public int? CookId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}
