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
    /// Логика взаимодействия для PutInFridgeWindow.xaml
    /// </summary>
    public partial class PutInFridgeWindow : Window
    {

        public PutInFridgeWindow()
        {
            InitializeComponent();
            Loaded += PutInFridgeWindow_Load;
        }

        private void PutInFridgeWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Ingredient/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<IngredientViewModel> list = APIClient.GetElement<List<IngredientViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxIngredient.DisplayMemberPath = "IngredientName";
                        comboBoxIngredient.SelectedValuePath = "Id";
                        comboBoxIngredient.ItemsSource = list;
                        comboBoxIngredient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/Fridge/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<FridgeViewModel> list = APIClient.GetElement<List<FridgeViewModel>>(responseS);
                    if (list != null)
                    {
                        comboBoxFridge.DisplayMemberPath = "FridgeName";
                        comboBoxFridge.SelectedValuePath = "Id";
                        comboBoxFridge.ItemsSource = list;
                        comboBoxFridge.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxFridge.SelectedValue == null)
            {
                MessageBox.Show("Выберите холодильник", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/PutIngredientInFridge", new FridgeIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    FridgeId = Convert.ToInt32(comboBoxFridge.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
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
