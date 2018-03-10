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
    public class FridgeServiceBD : IFridgeService
    {
        private PizzeriaDbContext context;

        public FridgeServiceBD(PizzeriaDbContext context)
        {
            this.context = context;
        }

        public List<FridgeViewModel> GetList()
        {
            List<FridgeViewModel> result = context.Fridges
                .Select(rec => new FridgeViewModel
                {
                    Id = rec.Id,
                    FridgeName = rec.FridgeName,
                    FridgeIngredients = context.FridgeIngredients
                            .Where(recPC => recPC.FridgeId == rec.Id)
                            .Select(recPC => new FridgeIngredientViewModel
                            {
                                Id = recPC.Id,
                                FridgeId = recPC.FridgeId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public FridgeViewModel GetElement(int id)
        {
            Fridge element = context.Fridges.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new FridgeViewModel
                {
                    Id = element.Id,
                    FridgeName = element.FridgeName,
                    FridgeIngredients = context.FridgeIngredients
                            .Where(recPC => recPC.FridgeId == element.Id)
                            .Select(recPC => new FridgeIngredientViewModel
                            {
                                Id = recPC.Id,
                                FridgeId = recPC.FridgeId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FridgeBindingModel model)
        {
            Fridge element = context.Fridges.FirstOrDefault(rec => rec.FridgeName == model.FridgeName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Fridges.Add(new Fridge
            {
                FridgeName = model.FridgeName
            });
            context.SaveChanges();
        }

        public void UpdElement(FridgeBindingModel model)
        {
            Fridge element = context.Fridges.FirstOrDefault(rec =>
                                        rec.FridgeName == model.FridgeName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Fridges.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FridgeName = model.FridgeName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Fridge element = context.Fridges.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.FridgeIngredients.RemoveRange(
                                            context.FridgeIngredients.Where(rec => rec.FridgeId == id));
                        context.Fridges.Remove(element);
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
