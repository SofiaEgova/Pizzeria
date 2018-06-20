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
    [CustomInterface("Интерфейс для работы с холодильниками")]
    public interface IFridgeService
    {
        [CustomMethod("Метод получения списка холодильников")]
        List<FridgeViewModel> GetList();

        [CustomMethod("Метод получения холодильника по id")]
        FridgeViewModel GetElement(int id);

        [CustomMethod("Метод добавления холодильника")]
        void AddElement(FridgeBindingModel model);

        [CustomMethod("Метод изменения данных по холодильнику")]
        void UpdElement(FridgeBindingModel model);

        [CustomMethod("Метод удаления холодильника")]
        void DelElement(int id);
    }
}
