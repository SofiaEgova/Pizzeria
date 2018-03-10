using PizzeriaService;
using PizzeriaService.ImplementationsBD;
using PizzeriaService.ImplementationsList;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace PizzeriaView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, PizzeriaDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVisitorService, VisitorServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookService, CookServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaService, PizzaServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFridgeService, FridgeServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
