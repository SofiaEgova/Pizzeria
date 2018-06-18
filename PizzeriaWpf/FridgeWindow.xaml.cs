using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для FridgeWindow.xaml
    /// </summary>
    public partial class FridgeWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;

        public FridgeWindow()
        {
            InitializeComponent();
            Loaded += FridgeWindow_Load;
        }

        private void FridgeWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var Fridge = Task.Run(() => APIClient.GetRequestData<FridgeViewModel>("api/Fridge/Get/" + id.Value)).Result;
                    textBoxName.Text = Fridge.FridgeName;
                        dataGrid.ItemsSource = Fridge.FridgeIngredients;
                        dataGrid.Columns[0].Visibility = Visibility.Hidden;
                        dataGrid.Columns[1].Visibility = Visibility.Hidden;
                        dataGrid.Columns[2].Visibility = Visibility.Hidden;
                        dataGrid.Columns[3].Width = dataGrid.Width - 8;
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

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните Название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Fridge/UpdElement", new FridgeBindingModel
                {
                    Id = id.Value,
                    FridgeName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Fridge/AddElement", new FridgeBindingModel
                {
                    FridgeName = name
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
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

            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
