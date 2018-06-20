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
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<OrderPizzaViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateOrderPizza(OrderPizzaBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeOrderPizzaInWork(OrderPizzaBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishOrderPizza(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayOrderPizza(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutIngredientInFridge(FridgeIngredientBindingModel model);
    }
}
