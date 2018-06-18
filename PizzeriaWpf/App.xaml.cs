using PizzeriaService;
using PizzeriaService.ImplementationsBD;
using PizzeriaService.ImplementationsList;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            APIClient.Connect();
            var application = new App();
            application.Run(new MainWindow());
        }
    }
}
