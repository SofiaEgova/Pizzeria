using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface IVisitorService
    {
        List<VisitorViewModel> GetList();

        VisitorViewModel GetElement(int id);

        void AddElement(VisitorBindingModel model);

        void UpdElement(VisitorBindingModel model);

        void DelElement(int id);
    }
}
