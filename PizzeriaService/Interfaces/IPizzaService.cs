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
    [CustomInterface("Интерфейс для работы с пиццами")]
    public interface IPizzaService
    {
        [CustomMethod("Метод получения списка пицц")]
        List<PizzaViewModel> GetList();

        [CustomMethod("Метод получения пиццы по id")]
        PizzaViewModel GetElement(int id);

        [CustomMethod("Метод добавления пиццы")]
        void AddElement(PizzaBindingModel model);

        [CustomMethod("Метод изменения данных по пицце")]
        void UpdElement(PizzaBindingModel model);

        [CustomMethod("Метод удаления пиццы")]
        void DelElement(int id);
    }
}
