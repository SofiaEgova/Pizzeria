using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface IMainService
    {
        List<OrderPizzaViewModel> GetList();

        void CreateOrderPizza(OrderPizzaBindingModel model);

        void TakeOrderPizzaInWork(OrderPizzaBindingModel model);

        void FinishOrderPizza(int id);

        void PayOrderPizza(int id);

        void PutIngredientInFridge(FridgeIngredientBindingModel model);
    }
}
