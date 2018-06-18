using PizzeriaModel;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ImplementationsBD
{
    public class PizzaServiceBD : IPizzaService
    {
        private PizzeriaDbContext context;

        public PizzaServiceBD(PizzeriaDbContext context)
        {
            this.context = context;
        }

        public List<PizzaViewModel> GetList()
        {
            List<PizzaViewModel> result = context.Pizzas
                .Select(rec => new PizzaViewModel
                {
                    Id = rec.Id,
                    PizzaName = rec.PizzaName,
                    Price = rec.Price,
                    PizzaIngredients = context.PizzaIngredients
                            .Where(recPC => recPC.PizzaId == rec.Id)
                            .Select(recPC => new PizzaIngredientViewModel
                            {
                                Id = recPC.Id,
                                PizzaId = recPC.PizzaId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public PizzaViewModel GetElement(int id)
        {
            Pizza element = context.Pizzas.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new PizzaViewModel
                {
                    Id = element.Id,
                    PizzaName = element.PizzaName,
                    Price = element.Price,
                    PizzaIngredients = context.PizzaIngredients
                            .Where(recPC => recPC.PizzaId == element.Id)
                            .Select(recPC => new PizzaIngredientViewModel
                            {
                                Id = recPC.Id,
                                PizzaId = recPC.PizzaId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(PizzaBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Pizza element = context.Pizzas.FirstOrDefault(rec => rec.PizzaName == model.PizzaName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Pizza
                    {
                        PizzaName = model.PizzaName,
                        Price = model.Price
                    };
                    context.Pizzas.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupIngredients = model.PizzaIngredients
                                                .GroupBy(rec => rec.IngredientId)
                                                .Select(rec => new
                                                {
                                                    IngredientId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupIngredient in groupIngredients)
                    {
                        context.PizzaIngredients.Add(new PizzaIngredient
                        {
                            PizzaId = element.Id,
                            IngredientId = groupIngredient.IngredientId,
                            Count = groupIngredient.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(PizzaBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Pizza element = context.Pizzas.FirstOrDefault(rec =>
                                        rec.PizzaName == model.PizzaName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.PizzaName = model.PizzaName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var ingredIds = model.PizzaIngredients.Select(rec => rec.IngredientId).Distinct();
                    var updateIngredients = context.PizzaIngredients
                                                    .Where(rec => rec.PizzaId == model.Id &&
                                                        ingredIds.Contains(rec.IngredientId));
                    foreach (var updateIngredient in updateIngredients)
                    {
                        updateIngredient.Count = model.PizzaIngredients
                                                        .FirstOrDefault(rec => rec.Id == updateIngredient.Id).Count;
                    }
                    context.SaveChanges();
                    context.PizzaIngredients.RemoveRange(
                                        context.PizzaIngredients.Where(rec => rec.PizzaId == model.Id &&
                                                                            !ingredIds.Contains(rec.IngredientId)));
                    context.SaveChanges();
                    // новые записи
                    var groupIngredients = model.PizzaIngredients
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.IngredientId)
                                                .Select(rec => new
                                                {
                                                    IngredientId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupIngredient in groupIngredients)
                    {
                        PizzaIngredient elementPC = context.PizzaIngredients
                                                .FirstOrDefault(rec => rec.PizzaId == model.Id &&
                                                                rec.IngredientId == groupIngredient.IngredientId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupIngredient.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.PizzaIngredients.Add(new PizzaIngredient
                            {
                                PizzaId = model.Id,
                                IngredientId = groupIngredient.IngredientId,
                                Count = groupIngredient.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Pizza element = context.Pizzas.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.PizzaIngredients.RemoveRange(
                                            context.PizzaIngredients.Where(rec => rec.PizzaId == id));
                        context.Pizzas.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
