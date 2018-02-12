using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.Interfaces
{
    public interface IPizzaService
    {
        List<PizzaViewModel> GetList();

        PizzaViewModel GetElement(int id);

        void AddElement(PizzaBindingModel model);

        void UpdElement(PizzaBindingModel model);

        void DelElement(int id);
    }
}
