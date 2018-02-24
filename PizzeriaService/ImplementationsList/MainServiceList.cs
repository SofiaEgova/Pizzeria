using PizzeriaModel;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ImplementationsList
{
    public class MainServiceList :IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderPizzaViewModel> GetList()
        {
            List<OrderPizzaViewModel> result = source.OrderPizzas
                .Select(rec => new OrderPizzaViewModel
                {
                    Id = rec.Id,
                    VisitorId = rec.VisitorId,
                    PizzaId = rec.PizzaId,
                    CookId = rec.CookId,
                    TimeCreate = rec.TimeCreate.ToLongDateString(),
                    TimeDone = rec.TimeDone?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    VisitorFIO = source.Visitors
                                    .FirstOrDefault(recC => recC.Id == rec.VisitorId)?.VisitorFIO,
                    PizzaName = source.Pizzas
                                    .FirstOrDefault(recP => recP.Id == rec.PizzaId)?.PizzaName,
                    CookName = source.Cooks
                                    .FirstOrDefault(recI => recI.Id == rec.CookId)?.CookFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrderPizza(OrderPizzaBindingModel model)
        {
            int maxId = source.OrderPizzas.Count > 0 ? source.OrderPizzas.Max(rec => rec.Id) : 0;
            source.OrderPizzas.Add(new OrderPizza
            {
                Id = maxId + 1,
                VisitorId = model.VisitorId,
                PizzaId = model.PizzaId,
                TimeCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderPizzaStatus.Принят
            });
        }

        public void TakeOrderPizzaInWork(OrderPizzaBindingModel model)
        {
            OrderPizza element = source.OrderPizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var PizzaIngredients = source.PizzaIngredients.Where(rec => rec.PizzaId == element.PizzaId);
            foreach (var PizzaIngredient in PizzaIngredients)
            {
                int countOnFridges = source.FridgeIngredients
                                            .Where(rec => rec.IngredientId == PizzaIngredient.IngredientId)
                                            .Sum(rec => rec.Count);
                if (countOnFridges < PizzaIngredient.Count * element.Count)
                {
                    var IngredientName = source.Ingredients
                                    .FirstOrDefault(rec => rec.Id == PizzaIngredient.IngredientId);
                    throw new Exception("Не достаточно компонента " + IngredientName?.IngredientName +
                        " требуется " + PizzaIngredient.Count + ", в наличии " + countOnFridges);
                }
            }
            // списываем
            foreach (var PizzaIngredient in PizzaIngredients)
            {
                int countOnFridges = PizzaIngredient.Count * element.Count;
                var FridgeIngredients = source.FridgeIngredients
                                            .Where(rec => rec.IngredientId == PizzaIngredient.IngredientId);
                foreach (var FridgeIngredient in FridgeIngredients)
                {
                    // компонентов на одном слкаде может не хватать
                    if (FridgeIngredient.Count >= countOnFridges)
                    {
                        FridgeIngredient.Count -= countOnFridges;
                        break;
                    }
                    else
                    {
                        countOnFridges -= FridgeIngredient.Count;
                        FridgeIngredient.Count = 0;
                    }
                }
            }
            element.CookId = model.CookId;
            element.TimeDone = DateTime.Now;
            element.Status = OrderPizzaStatus.Выполняется;
        }

        public void FinishOrderPizza(int id)
        {
            OrderPizza element = source.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Готов;
        }

        public void PayOrderPizza(int id)
        {
            OrderPizza element = source.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Оплачен;
        }

        public void PutIngredientInFridge(FridgeIngredientBindingModel model)
        {
            FridgeIngredient element = source.FridgeIngredients
                                                .FirstOrDefault(rec => rec.FridgeId == model.FridgeId &&
                                                                    rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.FridgeIngredients.Count > 0 ? source.FridgeIngredients.Max(rec => rec.Id) : 0;
                source.FridgeIngredients.Add(new FridgeIngredient
                {
                    Id = ++maxId,
                    FridgeId = model.FridgeId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
        }

    }
}
