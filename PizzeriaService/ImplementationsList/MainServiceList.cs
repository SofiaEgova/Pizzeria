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
            List<OrderPizzaViewModel> result = new List<OrderPizzaViewModel>();
            for (int i = 0; i < source.OrderPizzas.Count; ++i)
            {
                string visitorFIO = string.Empty;
                for (int j = 0; j < source.Visitors.Count; ++j)
                {
                    if (source.Visitors[j].Id == source.OrderPizzas[i].VisitorId)
                    {
                        visitorFIO = source.Visitors[j].VisitorFIO;
                        break;
                    }
                }
                string pizzaName = string.Empty;
                for (int j = 0; j < source.Pizzas.Count; ++j)
                {
                    if (source.Pizzas[j].Id == source.OrderPizzas[i].PizzaId)
                    {
                        pizzaName = source.Pizzas[j].PizzaName;
                        break;
                    }
                }
                string cookFIO = string.Empty;
                if (source.OrderPizzas[i].CookId.HasValue)
                {
                    for (int j = 0; j < source.Cooks.Count; ++j)
                    {
                        if (source.Cooks[j].Id == source.OrderPizzas[i].CookId.Value)
                        {
                            cookFIO = source.Cooks[j].CookFIO;
                            break;
                        }
                    }
                }
                result.Add(new OrderPizzaViewModel
                {
                    Id = source.OrderPizzas[i].Id,
                    VisitorId = source.OrderPizzas[i].VisitorId,
                    VisitorFIO = visitorFIO,
                    PizzaId = source.OrderPizzas[i].PizzaId,
                    PizzaName = pizzaName,
                    CookId = source.OrderPizzas[i].CookId,
                    CookName = cookFIO,
                    Count = source.OrderPizzas[i].Count,
                    Sum = source.OrderPizzas[i].Sum,
                    TimeCreate = source.OrderPizzas[i].TimeCreate.ToLongDateString(),
                    TimeDone = source.OrderPizzas[i].TimeDone?.ToLongDateString(),
                    Status = source.OrderPizzas[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateOrderPizza(OrderPizzaBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.OrderPizzas.Count; ++i)
            {
                if (source.OrderPizzas[i].Id > maxId)
                {
                    maxId = source.Visitors[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.OrderPizzas.Count; ++i)
            {
                if (source.OrderPizzas[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == source.OrderPizzas[index].PizzaId)
                {
                    int countOnStocks = 0;
                    for (int j = 0; j < source.FridgeIngredients.Count; ++j)
                    {
                        if (source.FridgeIngredients[j].IngredientId == source.PizzaIngredients[i].IngredientId)
                        {
                            countOnStocks += source.FridgeIngredients[j].Count;
                        }
                    }
                    if (countOnStocks < source.PizzaIngredients[i].Count * source.OrderPizzas[index].Count)
                    {
                        for (int j = 0; j < source.Ingredients.Count; ++j)
                        {
                            if (source.Ingredients[j].Id == source.PizzaIngredients[i].IngredientId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Ingredients[j].IngredientName +
                                    " требуется " + source.PizzaIngredients[i].Count + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == source.OrderPizzas[index].PizzaId)
                {
                    int countOnStocks = source.PizzaIngredients[i].Count * source.OrderPizzas[index].Count;
                    for (int j = 0; j < source.FridgeIngredients.Count; ++j)
                    {
                        if (source.FridgeIngredients[j].IngredientId == source.PizzaIngredients[i].IngredientId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.FridgeIngredients[j].Count >= countOnStocks)
                            {
                                source.FridgeIngredients[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.FridgeIngredients[j].Count;
                                source.FridgeIngredients[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.OrderPizzas[index].CookId = model.CookId;
            source.OrderPizzas[index].TimeDone = DateTime.Now;
            source.OrderPizzas[index].Status = OrderPizzaStatus.Выполняется;
        }

        public void FinishOrderPizza(int id)
        {
            int index = -1;
            for (int i = 0; i < source.OrderPizzas.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.OrderPizzas[index].Status = OrderPizzaStatus.Готов;
        }

        public void PayOrderPizza(int id)
        {
            int index = -1;
            for (int i = 0; i < source.OrderPizzas.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.OrderPizzas[index].Status = OrderPizzaStatus.Оплачен;
        }

        public void PutIngredientInFridge(FridgeIngredientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.FridgeIngredients.Count; ++i)
            {
                if (source.FridgeIngredients[i].FridgeId == model.FridgeId &&
                    source.FridgeIngredients[i].IngredientId == model.IngredientId)
                {
                    source.FridgeIngredients[i].Count += model.Count;
                    return;
                }
                if (source.FridgeIngredients[i].Id > maxId)
                {
                    maxId = source.FridgeIngredients[i].Id;
                }
            }
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
