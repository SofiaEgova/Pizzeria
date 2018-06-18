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
                    var response = APIClient.GetRequest("api/Fridge/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var Fridge = APIClient.GetElement<FridgeViewModel>(response);
                        textBoxName.Text = Fridge.FridgeName;
                        dataGrid.ItemsSource = Fridge.FridgeIngredients;
                        dataGrid.Columns[0].Visibility = Visibility.Hidden;
                        dataGrid.Columns[1].Visibility = Visibility.Hidden;
                        dataGrid.Columns[2].Visibility = Visibility.Hidden;
                        dataGrid.Columns[3].Width = dataGrid.Width - 8;
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
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
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Fridge/UpdElement", new FridgeBindingModel
                    {
                        Id = id.Value,
                        FridgeName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Fridge/AddElement", new FridgeBindingModel
                    {
                        FridgeName = textBoxName.Text
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
