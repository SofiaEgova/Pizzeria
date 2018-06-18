using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    [DataContract]
    public class OrderPizzaViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int VisitorId { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }

        [DataMember]
        public int PizzaId { get; set; }

        [DataMember]
        public string PizzaName { get; set; }

        [DataMember]
        public int? CookId { get; set; }

        [DataMember]
        public string CookName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string TimeCreate { get; set; }

        [DataMember]
        public string TimeDone { get; set; }
    }
}
