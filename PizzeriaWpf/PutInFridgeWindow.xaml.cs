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
                List<IngredientViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<IngredientViewModel>>("api/Ingredient/GetList")).Result;
                if (listC != null)
                {
                    comboBoxIngredient.DisplayMemberPath = "IngredientName";
                    comboBoxIngredient.SelectedValuePath = "Id";
                    comboBoxIngredient.ItemsSource = listC;
                    comboBoxIngredient.SelectedItem = null;
                }

                List<FridgeViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<FridgeViewModel>>("api/Fridge/GetList")).Result;
                if (listS != null)
                {
                    comboBoxFridge.DisplayMemberPath = "FridgeName";
                    comboBoxFridge.SelectedValuePath = "Id";
                    comboBoxFridge.ItemsSource = listS;
                    comboBoxFridge.SelectedItem = null;
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
                int ingredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue);
                int fridgeId = Convert.ToInt32(comboBoxFridge.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PutIngredientInFridge", new FridgeIngredientBindingModel
                {
                    IngredientId = ingredientId,
                    FridgeId = fridgeId,
                    Count = count
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Холодильник пополнен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
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
