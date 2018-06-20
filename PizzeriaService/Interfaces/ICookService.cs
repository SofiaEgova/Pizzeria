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
    [CustomInterface("Интерфейс для работы с поварами")]
    public interface ICookService
    {
        [CustomMethod("Метод получения списка поваров")]
        List<CookViewModel> GetList();

        [CustomMethod("Метод получения повара по id")]
        CookViewModel GetElement(int id);

        [CustomMethod("Метод добавления повара")]
        void AddElement(CookBindingModel model);

        [CustomMethod("Метод изменения данных по повару")]
        void UpdElement(CookBindingModel model);

        [CustomMethod("Метод удаления повара")]
        void DelElement(int id);
    }
}
