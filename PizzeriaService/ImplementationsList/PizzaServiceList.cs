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
    public class PizzaServiceList : IPizzaService
    {
        private DataListSingleton source;

        public PizzaServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<PizzaViewModel> GetList()
        {
            List<PizzaViewModel> result = source.Pizzas
                .Select(rec => new PizzaViewModel
                {
                    Id = rec.Id,
                    PizzaName = rec.PizzaName,
                    Price = rec.Price,
                    PizzaIngredients = source.PizzaIngredients
                            .Where(recPC => recPC.PizzaId == rec.Id)
                            .Select(recPC => new PizzaIngredientViewModel
                            {
                                Id = recPC.Id,
                                PizzaId = recPC.PizzaId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = source.Ingredients
                                    .FirstOrDefault(recC => recC.Id == recPC.IngredientId)?.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public PizzaViewModel GetElement(int id)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new PizzaViewModel
                {
                    Id = element.Id,
                    PizzaName = element.PizzaName,
                    Price = element.Price,
                    PizzaIngredients = source.PizzaIngredients
                            .Where(recPC => recPC.PizzaId == element.Id)
                            .Select(recPC => new PizzaIngredientViewModel
                            {
                                Id = recPC.Id,
                                PizzaId = recPC.PizzaId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = source.Ingredients
                                        .FirstOrDefault(recC => recC.Id == recPC.IngredientId)?.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.PizzaName == model.PizzaName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Pizzas.Count > 0 ? source.Pizzas.Max(rec => rec.Id) : 0;
            source.Pizzas.Add(new Pizza
            {
                Id = maxId + 1,
                PizzaName = model.PizzaName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.PizzaIngredients.Count > 0 ?
                                    source.PizzaIngredients.Max(rec => rec.Id) : 0;
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
                source.PizzaIngredients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = maxId + 1,
                    IngredientId = groupIngredient.IngredientId,
                    Count = groupIngredient.Count
                });
            }
        }

        public void UpdElement(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec =>
                                        rec.PizzaName == model.PizzaName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.PizzaName = model.PizzaName;
            element.Price = model.Price;

            int maxPCId = source.PizzaIngredients.Count > 0 ? source.PizzaIngredients.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.PizzaIngredients.Select(rec => rec.IngredientId).Distinct();
            var updateIngredients = source.PizzaIngredients
                                            .Where(rec => rec.PizzaId == model.Id &&
                                           compIds.Contains(rec.IngredientId));
            foreach (var updateIngredient in updateIngredients)
            {
                updateIngredient.Count = model.PizzaIngredients
                                                .FirstOrDefault(rec => rec.Id == updateIngredient.Id).Count;
            }
            source.PizzaIngredients.RemoveAll(rec => rec.PizzaId == model.Id &&
                                       !compIds.Contains(rec.IngredientId));
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
                PizzaIngredient elementPC = source.PizzaIngredients
                                        .FirstOrDefault(rec => rec.PizzaId == model.Id &&
                                                        rec.IngredientId == groupIngredient.IngredientId);
                if (elementPC != null)
                {
                    elementPC.Count += groupIngredient.Count;
                }
                else
                {
                    source.PizzaIngredients.Add(new PizzaIngredient
                    {
                        Id = ++maxPCId,
                        PizzaId = model.Id,
                        IngredientId = groupIngredient.IngredientId,
                        Count = groupIngredient.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.PizzaIngredients.RemoveAll(rec => rec.PizzaId == id);
                source.Pizzas.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
