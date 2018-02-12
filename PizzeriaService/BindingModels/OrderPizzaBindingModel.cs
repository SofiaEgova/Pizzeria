using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.BindingModels
{
    public class OrderPizzaBindingModel
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public int PizzaId { get; set; }

        public int? CookId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
