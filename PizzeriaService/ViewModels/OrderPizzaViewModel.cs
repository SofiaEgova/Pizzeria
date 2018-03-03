using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    public class OrderPizzaViewModel
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public string VisitorFIO { get; set; }

        public int PizzaId { get; set; }

        public string PizzaName { get; set; }

        public int? CookId { get; set; }

        public string CookName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string TimeCreate { get; set; }

        public string TimeDone { get; set; }
    }
}
