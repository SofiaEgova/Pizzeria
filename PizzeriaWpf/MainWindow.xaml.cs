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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                List<OrderPizzaViewModel> list = Task.Run(() => APIClient.GetRequestData<List<OrderPizzaViewModel>>("api/Main/GetList")).Result;
                if (list != null)
                {
                    dataGridVeiw.ItemsSource = list;
                    dataGridVeiw.Columns[0].Visibility = Visibility.Hidden;
                    dataGridVeiw.Columns[1].Visibility = Visibility.Hidden;
                    dataGridVeiw.Columns[3].Visibility = Visibility.Hidden;
                    dataGridVeiw.Columns[5].Visibility = Visibility.Hidden;
                    dataGridVeiw.Columns[1].Width = DataGridLength.Auto;
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

        private void посетителиToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new VisitorsWindow();
            form.ShowDialog();
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new IngredientsWindow();
            form.ShowDialog();
        }

        private void пиццыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new PizzasWindow();
            form.ShowDialog();
        }

        private void холодильникиToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new FridgesWindow();
            form.ShowDialog();
        }

        private void повараToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new CooksWindow();
            form.ShowDialog();
        }

        private void пополнитьХолодильникToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new PutInFridgeWindow();
            form.ShowDialog();
        }

        private void buttonOrderPizza_Click(object sender, RoutedEventArgs e)
        {
            var form = new OrderPizzaWindow();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeOrderInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVeiw.SelectedItem != null)
            {
                var form = new TakeOrderPizzaInWorkWindow();
                form.Id = ((OrderPizzaViewModel)dataGridVeiw.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonOrderReady_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVeiw.SelectedItem != null)
            {
                int id = ((OrderPizzaViewModel)dataGridVeiw.SelectedItem).Id;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/FinishOrderPizza", new OrderPizzaBindingModel
                {
                    Id = id
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Статус заказа изменен. Обновите список", "Успех", MessageBoxButton.OK, MessageBoxImage.Information),
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

        private void buttonPayOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVeiw.SelectedItem != null)
            {
                int id = ((OrderPizzaViewModel)dataGridVeiw.SelectedItem).Id;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PayOrderPizza", new OrderPizzaBindingModel
                {
                    Id = id
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Статус заказа изменен. Обновите список", "Успех", MessageBoxButton.OK, MessageBoxImage.Information),
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

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == true)
            {
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SavePizzaPrice", new ReportBindingModel
                {
                    FileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information),
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

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var form = new FridgesLoadWindow();
            form.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new VisitorOrderPizzasWindow();
            form.ShowDialog();
        }
    }
}
