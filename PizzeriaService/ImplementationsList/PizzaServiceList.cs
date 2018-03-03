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
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<PizzaIngredientViewModel> pizzaComponents = new List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                {
                    if (source.PizzaIngredients[j].PizzaId == source.Pizzas[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                componentName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        pizzaComponents.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredients[j].Id,
                            PizzaId = source.PizzaIngredients[j].PizzaId,
                            IngredientId = source.PizzaIngredients[j].IngredientId,
                            IngredientName = componentName,
                            Count = source.PizzaIngredients[j].Count
                        });
                    }
                }
                result.Add(new PizzaViewModel
                {
                    Id = source.Pizzas[i].Id,
                    PizzaName = source.Pizzas[i].PizzaName,
                    Price = source.Pizzas[i].Price,
                    PizzaIngredients = pizzaComponents
                });
            }
            return result;
        }

        public PizzaViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<PizzaIngredientViewModel> pizzaComponents = new List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                {
                    if (source.PizzaIngredients[j].PizzaId == source.Pizzas[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                componentName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        pizzaComponents.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredients[j].Id,
                            PizzaId = source.PizzaIngredients[j].PizzaId,
                            IngredientId = source.PizzaIngredients[j].IngredientId,
                            IngredientName = componentName,
                            Count = source.PizzaIngredients[j].Count
                        });
                    }
                }
                if (source.Pizzas[i].Id == id)
                {
                    return new PizzaViewModel
                    {
                        Id = source.Pizzas[i].Id,
                        PizzaName = source.Pizzas[i].PizzaName,
                        Price = source.Pizzas[i].Price,
                        PizzaIngredients = pizzaComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(PizzaBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id > maxId)
                {
                    maxId = source.Pizzas[i].Id;
                }
                if (source.Pizzas[i].PizzaName == model.PizzaName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Pizzas.Add(new Pizza
            {
                Id = maxId + 1,
                PizzaName = model.PizzaName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                for (int j = 1; j < model.PizzaIngredients.Count; ++j)
                {
                    if (model.PizzaIngredients[i].IngredientId ==
                        model.PizzaIngredients[j].IngredientId)
                    {
                        model.PizzaIngredients[i].Count +=
                            model.PizzaIngredients[j].Count;
                        model.PizzaIngredients.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                source.PizzaIngredients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = maxId + 1,
                    IngredientId = model.PizzaIngredients[i].IngredientId,
                    Count = model.PizzaIngredients[i].Count
                });
            }
        }

        public void UpdElement(PizzaBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Pizzas[i].PizzaName == model.PizzaName &&
                    source.Pizzas[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Pizzas[index].PizzaName = model.PizzaName;
            source.Pizzas[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.PizzaIngredients.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.PizzaIngredients[i].Id == model.PizzaIngredients[j].Id)
                        {
                            source.PizzaIngredients[i].Count = model.PizzaIngredients[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.PizzaIngredients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                if (model.PizzaIngredients[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                    {
                        if (source.PizzaIngredients[j].PizzaId == model.Id &&
                            source.PizzaIngredients[j].IngredientId == model.PizzaIngredients[i].IngredientId)
                        {
                            source.PizzaIngredients[j].Count += model.PizzaIngredients[i].Count;
                            model.PizzaIngredients[i].Id = source.PizzaIngredients[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.PizzaIngredients[i].Id == 0)
                    {
                        source.PizzaIngredients.Add(new PizzaIngredient
                        {
                            Id = ++maxPCId,
                            PizzaId = model.Id,
                            IngredientId = model.PizzaIngredients[i].IngredientId,
                            Count = model.PizzaIngredients[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == id)
                {
                    source.PizzaIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
