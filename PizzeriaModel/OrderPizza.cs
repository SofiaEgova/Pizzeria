using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaModel
{
    public class OrderPizza
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public int PizzaId { get; set; }

        public int? CookId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderPizzaStatus Status { get; set; }

        public DateTime TimeCreate { get; set; }

        public DateTime? TimeDone { get; set; }

        public virtual Visitor Visitor { get; set; }

        public virtual Pizza Pizza { get; set; }

        public virtual Cook Cook { get; set; }
    }
}
