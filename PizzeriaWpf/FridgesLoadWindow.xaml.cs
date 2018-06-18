using Microsoft.Win32;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для FridgesLoadWindow.xaml
    /// </summary>
    public partial class FridgesLoadWindow : Window
    {
        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IReportService service;

        public FridgesLoadWindow(IReportService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += FridgesLoadWindow_Load;
        }

        private void buttonToExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    service.SaveFridgesLoad(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FridgesLoadWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetFridgesLoad();
                if (dict != null)
                {
                    dataGrid.Items.Clear();

                    foreach (var elem in dict)
                    {
                        dataGrid.Items.Add(new object[] { elem.FridgeName, "", "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGrid.Items.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGrid.Items.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGrid.Items.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
