using PizzeriaModel;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ImplementationsBD
{
    public class MainServiceBD : IMainService
    {
        private PizzeriaDbContext context;

        public MainServiceBD(PizzeriaDbContext context)
        {
            this.context = context;
        }

        public List<OrderPizzaViewModel> GetList()
        {
            List<OrderPizzaViewModel> result = context.OrderPizzas
                .Select(rec => new OrderPizzaViewModel
                {
                    Id = rec.Id,
                    VisitorId = rec.VisitorId,
                    PizzaId = rec.PizzaId,
                    CookId = rec.CookId,
                    TimeCreate = SqlFunctions.DateName("dd", rec.TimeCreate) + " " +
                                SqlFunctions.DateName("mm", rec.TimeCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.TimeCreate),
                    TimeDone = rec.TimeDone == null ? "" :
                                        SqlFunctions.DateName("dd", rec.TimeDone.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.TimeDone.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.TimeDone.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    VisitorFIO = rec.Visitor.VisitorFIO,
                    PizzaName = rec.Pizza.PizzaName,
                    CookName = rec.Cook.CookFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrderPizza(OrderPizzaBindingModel model)
        {
            context.OrderPizzas.Add(new OrderPizza
            {
                VisitorId = model.VisitorId,
                PizzaId = model.PizzaId,
                TimeCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderPizzaStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeOrderPizzaInWork(OrderPizzaBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    OrderPizza element = context.OrderPizzas.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var PizzaIngredients = context.PizzaIngredients
                                                .Where(rec => rec.PizzaId == element.PizzaId);
                    // списываем
                    foreach (var PizzaIngredient in PizzaIngredients)
                    {
                        int countOnFridges = PizzaIngredient.Count * element.Count;
                        var FridgeIngredients = context.FridgeIngredients
                                                    .Where(rec => rec.IngredientId == PizzaIngredient.IngredientId);
                        foreach (var FridgeIngredient in FridgeIngredients)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (FridgeIngredient.Count >= countOnFridges)
                            {
                                FridgeIngredient.Count -= countOnFridges;
                                countOnFridges = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnFridges -= FridgeIngredient.Count;
                                FridgeIngredient.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnFridges > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                PizzaIngredient.Ingredient.IngredientName + " требуется " +
                                PizzaIngredient.Count + ", не хватает " + countOnFridges);
                        }
                    }
                    element.CookId = model.CookId;
                    element.TimeDone = DateTime.Now;
                    element.Status = OrderPizzaStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrderPizza(int id)
        {
            OrderPizza element = context.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Готов;
            context.SaveChanges();
        }

        public void PayOrderPizza(int id)
        {
            OrderPizza element = context.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutIngredientInFridge(FridgeIngredientBindingModel model)
        {
            FridgeIngredient element = context.FridgeIngredients
                                                .FirstOrDefault(rec => rec.FridgeId == model.FridgeId &&
                                                                    rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.FridgeIngredients.Add(new FridgeIngredient
                {
                    FridgeId = model.FridgeId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
