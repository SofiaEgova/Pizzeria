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
    /// Логика взаимодействия для OrderPizzaWindow.xaml
    /// </summary>
    public partial class OrderPizzaWindow : Window
    {
        public OrderPizzaWindow()
        {
            InitializeComponent();
            Loaded += OrderPizzaWindow_Load;
            comboBoxPizza.SelectionChanged += comboBoxPizza_SelectionChanged;
            comboBoxPizza.SelectionChanged += new SelectionChangedEventHandler(comboBoxPizza_SelectionChanged);
        }

        private void OrderPizzaWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Visitor/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<VisitorViewModel> list = APIClient.GetElement<List<VisitorViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                        comboBoxVisitor.SelectedValuePath = "Id";
                        comboBoxVisitor.ItemsSource = list;
                        comboBoxVisitor.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/Pizza/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<PizzaViewModel> list = APIClient.GetElement<List<PizzaViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxPizza.DisplayMemberPath = "PizzaName";
                        comboBoxPizza.SelectedValuePath = "Id";
                        comboBoxPizza.ItemsSource = list;
                        comboBoxPizza.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxPizza.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPizza.SelectedValue);
                    var responseP = APIClient.GetRequest("api/Pizza/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        PizzaViewModel Pizza = APIClient.GetElement<PizzaViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)Pizza.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
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
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxVisitor.SelectedValue == null)
            {
                MessageBox.Show("Выберите посетителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxPizza.SelectedValue == null)
            {
                MessageBox.Show("Выберите пиццу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/CreateOrderPizza", new OrderPizzaBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    PizzaId = Convert.ToInt32(comboBoxPizza.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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

        private void comboBoxPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSum();
        }
    }
}
