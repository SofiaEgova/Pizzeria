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
    [CustomInterface("Интерфейс для работы с посетителями")]
    public interface IVisitorService
    {
        [CustomMethod("Метод получения списка посетителей")]
        List<VisitorViewModel> GetList();

        [CustomMethod("Метод получения посетителя по id")]
        VisitorViewModel GetElement(int id);

        [CustomMethod("Метод добавления посетителя")]
        void AddElement(VisitorBindingModel model);

        [CustomMethod("Метод изменения данных по посетителю")]
        void UpdElement(VisitorBindingModel model);

        [CustomMethod("Метод удаления посетителя")]
        void DelElement(int id);
    }
}
