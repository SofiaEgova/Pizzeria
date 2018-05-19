using PizzeriaService.ImplementationsList;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {/// <summary>
     /// Главная точка входа для приложения.
     /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<MainWindow>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IVisitorService, VisitorServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookService, CookServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaService, PizzaServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFridgeService, FridgeServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
