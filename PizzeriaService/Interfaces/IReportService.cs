using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface IReportService
    {
        void SavePizzaPrice(ReportBindingModel model);

        List<FridgesLoadViewModel> GetFridgesLoad();

        void SaveFridgesLoad(ReportBindingModel model);

        List<VisitorOrderPizzasModel> GetVisitorOrderPizzas(ReportBindingModel model);

        void SaveVisitorOrderPizzas(ReportBindingModel model);
    }
}
