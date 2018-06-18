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
    /// Логика взаимодействия для PizzaWindow.xaml
    /// </summary>
    public partial class PizzaWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<PizzaIngredientViewModel> pizzaIngredients;

        public PizzaWindow()
        {
            InitializeComponent();
            Loaded += PizzaWindow_Load;
        }

        private void PizzaWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Pizza/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var Pizza = APIClient.GetElement<PizzaViewModel>(response);
                        textBoxName.Text = Pizza.PizzaName;
                        textBoxPrice.Text = Pizza.Price.ToString();
                        pizzaIngredients = Pizza.PizzaIngredients;
                        LoadData();
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
            else
            {
                pizzaIngredients = new List<PizzaIngredientViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (pizzaIngredients != null)
                {
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = pizzaIngredients;
                    dataGrid.Columns[0].Visibility = Visibility.Hidden;
                    dataGrid.Columns[1].Visibility = Visibility.Hidden;
                    dataGrid.Columns[2].Visibility = Visibility.Hidden;
                    dataGrid.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form =new PizzaIngredientWindow();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.PizzaId = id.Value;
                    pizzaIngredients.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var form = new PizzaIngredientWindow();
                form.Model = pizzaIngredients[dataGrid.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    pizzaIngredients[dataGrid.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        pizzaIngredients.RemoveAt(dataGrid.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните Название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pizzaIngredients == null || pizzaIngredients.Count == 0)
            {
                MessageBox.Show("Заполните ингредиенты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<PizzaIngredientBindingModel> productComponentBM = new List<PizzaIngredientBindingModel>();
                for (int i = 0; i < pizzaIngredients.Count; ++i)
                {
                    productComponentBM.Add(new PizzaIngredientBindingModel
                    {
                        Id = pizzaIngredients[i].Id,
                        PizzaId = pizzaIngredients[i].PizzaId,
                        IngredientId = pizzaIngredients[i].IngredientId,
                        Count = pizzaIngredients[i].Count
                    });
                }
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Pizza/UpdElement", new PizzaBindingModel
                    {
                        Id = id.Value,
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredients = productComponentBM
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Pizza/AddElement", new PizzaBindingModel
                    {
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredients = productComponentBM
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
