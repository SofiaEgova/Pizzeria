using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface ICookService
    {
        List<CookViewModel> GetList();

        CookViewModel GetElement(int id);

        void AddElement(CookBindingModel model);

        void UpdElement(CookBindingModel model);

        void DelElement(int id);
    }
}
