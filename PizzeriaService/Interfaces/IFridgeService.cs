using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface IFridgeService
    {
        List<FridgeViewModel> GetList();

        FridgeViewModel GetElement(int id);

        void AddElement(FridgeBindingModel model);

        void UpdElement(FridgeBindingModel model);

        void DelElement(int id);
    }
}
