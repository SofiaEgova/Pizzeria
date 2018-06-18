using Microsoft.Win32;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
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

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для FridgesLoadWindow.xaml
    /// </summary>
    public partial class FridgesLoadWindow : Window
    {
        public FridgesLoadWindow()
        {
            InitializeComponent();
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
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SaveFridgesLoad", new ReportBindingModel
                {
                    FileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void FridgesLoadWindow_Load(object sender, EventArgs e)
        {
            try
            {
                    dataGrid.Items.Clear();
                foreach (var elem in Task.Run(() => APIClient.GetRequestData<List<FridgesLoadViewModel>>("api/Report/GetFridgessLoad")).Result)
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
