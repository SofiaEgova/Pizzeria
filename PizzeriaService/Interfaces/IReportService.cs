using PizzeriaService.Attributies;
using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SavePizzaPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<FridgesLoadViewModel> GetFridgesLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveFridgesLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<VisitorOrderPizzasModel> GetVisitorOrderPizzas(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveVisitorOrderPizzas(ReportBindingModel model);
    }
}
