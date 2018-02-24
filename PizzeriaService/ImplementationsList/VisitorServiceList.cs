using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using PizzeriaModel;

namespace PizzeriaService.ImplementationsList
{
    public class VisitorServiceList : IVisitorService
    {

        private DataListSingleton source;

        public VisitorServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<VisitorViewModel> GetList()
        {
            List<VisitorViewModel> result = source.Visitors
                .Select(rec => new VisitorViewModel
                {
                    Id = rec.Id,
                    VisitorFIO = rec.VisitorFIO
                })
                .ToList();
            return result;
            
        }

        public VisitorViewModel GetElement(int id)
        {
            Visitor element = source.Visitors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new VisitorViewModel
                {
                    Id = element.Id,
                    VisitorFIO = element.VisitorFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(VisitorBindingModel model)
        {
            Visitor element = source.Visitors.FirstOrDefault(rec => rec.VisitorFIO == model.VisitorFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Visitors.Count > 0 ? source.Visitors.Max(rec => rec.Id) : 0;
            source.Visitors.Add(new Visitor
            {
                Id = maxId + 1,
                VisitorFIO = model.VisitorFIO
            });
        }

        public void DelElement(int id)
        {
            Visitor element = source.Visitors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Visitors.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void UpdElement(VisitorBindingModel model)
        {
            Visitor element = source.Visitors.FirstOrDefault(rec =>
                                   rec.VisitorFIO == model.VisitorFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Visitors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.VisitorFIO = model.VisitorFIO;
        }
    }
}
