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
    public class CookServiceList :ICookService
    {
        private DataListSingleton source;

        public CookServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CookViewModel> GetList()
        {
            List<CookViewModel> result = source.Cooks
                .Select(rec => new CookViewModel
                {
                    Id = rec.Id,
                    CookFIO = rec.CookFIO
                })
                .ToList();
            return result;
        }

        public CookViewModel GetElement(int id)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CookViewModel
                {
                    Id = element.Id,
                    CookFIO = element.CookFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CookBindingModel model)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.CookFIO == model.CookFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Cooks.Count > 0 ? source.Cooks.Max(rec => rec.Id) : 0;
            source.Cooks.Add(new Cook
            {
                Id = maxId + 1,
                CookFIO = model.CookFIO
            });
        }

        public void UpdElement(CookBindingModel model)
        {
            Cook element = source.Cooks.FirstOrDefault(rec =>
                                   rec.CookFIO == model.CookFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CookFIO = model.CookFIO;
        }

        public void DelElement(int id)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Cooks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
