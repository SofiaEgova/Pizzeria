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
    public class FridgeServiceList : IFridgeService
    {
        private DataListSingleton source;

        public FridgeServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<FridgeViewModel> GetList()
        {
            List<FridgeViewModel> result = new List<FridgeViewModel>();
            for (int i = 0; i < source.Fridges.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<FridgeIngredientViewModel> FridgeIngredients = new List<FridgeIngredientViewModel>();
                for (int j = 0; j < source.FridgeIngredients.Count; ++j)
                {
                    if (source.FridgeIngredients[j].FridgeId == source.Fridges[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.FridgeIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        FridgeIngredients.Add(new FridgeIngredientViewModel
                        {
                            Id = source.FridgeIngredients[j].Id,
                            FridgeId = source.FridgeIngredients[j].FridgeId,
                            IngredientId = source.FridgeIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.FridgeIngredients[j].Count
                        });
                    }
                }
                result.Add(new FridgeViewModel
                {
                    Id = source.Fridges[i].Id,
                    FridgeName = source.Fridges[i].FridgeName,
                    FridgeIngredients = FridgeIngredients
                });
            }
            return result;
        }

        public FridgeViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Fridges.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<FridgeIngredientViewModel> FridgeIngredients = new List<FridgeIngredientViewModel>();
                for (int j = 0; j < source.FridgeIngredients.Count; ++j)
                {
                    if (source.FridgeIngredients[j].FridgeId == source.Fridges[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.FridgeIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        FridgeIngredients.Add(new FridgeIngredientViewModel
                        {
                            Id = source.FridgeIngredients[j].Id,
                            FridgeId = source.FridgeIngredients[j].FridgeId,
                            IngredientId = source.FridgeIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.FridgeIngredients[j].Count
                        });
                    }
                }
                if (source.Fridges[i].Id == id)
                {
                    return new FridgeViewModel
                    {
                        Id = source.Fridges[i].Id,
                        FridgeName = source.Fridges[i].FridgeName,
                        FridgeIngredients = FridgeIngredients
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FridgeBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Fridges.Count; ++i)
            {
                if (source.Fridges[i].Id > maxId)
                {
                    maxId = source.Fridges[i].Id;
                }
                if (source.Fridges[i].FridgeName == model.FridgeName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Fridges.Add(new Fridge
            {
                Id = maxId + 1,
                FridgeName = model.FridgeName
            });
        }

        public void UpdElement(FridgeBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Fridges.Count; ++i)
            {
                if (source.Fridges[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Fridges[i].FridgeName == model.FridgeName &&
                    source.Fridges[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Fridges[index].FridgeName = model.FridgeName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.FridgeIngredients.Count; ++i)
            {
                if (source.FridgeIngredients[i].FridgeId == id)
                {
                    source.FridgeIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Fridges.Count; ++i)
            {
                if (source.Fridges[i].Id == id)
                {
                    source.Fridges.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

    }
}
