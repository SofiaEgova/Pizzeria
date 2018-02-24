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
            List<FridgeViewModel> result = source.Fridges
               .Select(rec => new FridgeViewModel
               {
                   Id = rec.Id,
                   FridgeName = rec.FridgeName,
                   FridgeIngredients = source.FridgeIngredients
                           .Where(recPC => recPC.FridgeId == rec.Id)
                           .Select(recPC => new FridgeIngredientViewModel
                           {
                               Id = recPC.Id,
                               FridgeId = recPC.FridgeId,
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

        public FridgeViewModel GetElement(int id)
        {
            Fridge element = source.Fridges.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new FridgeViewModel
                {
                    Id = element.Id,
                    FridgeName = element.FridgeName,
                    FridgeIngredients = source.FridgeIngredients
                            .Where(recPC => recPC.FridgeId == element.Id)
                            .Select(recPC => new FridgeIngredientViewModel
                            {
                                Id = recPC.Id,
                                FridgeId = recPC.FridgeId,
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

        public void AddElement(FridgeBindingModel model)
        {
            Fridge element = source.Fridges.FirstOrDefault(rec => rec.FridgeName == model.FridgeName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Fridges.Count > 0 ? source.Fridges.Max(rec => rec.Id) : 0;
            source.Fridges.Add(new Fridge
            {
                Id = maxId + 1,
                FridgeName = model.FridgeName
            });
        }

        public void UpdElement(FridgeBindingModel model)
        {
            Fridge element = source.Fridges.FirstOrDefault(rec =>
                                        rec.FridgeName == model.FridgeName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Fridges.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FridgeName = model.FridgeName;
        }

        public void DelElement(int id)
        {
            Fridge element = source.Fridges.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.FridgeIngredients.RemoveAll(rec => rec.FridgeId == id);
                source.Fridges.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
