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
                List<VisitorViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<VisitorViewModel>>("api/Visitor/GetList")).Result;
                if (listC != null)
                {
                    comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                    comboBoxVisitor.SelectedValuePath = "Id";
                    comboBoxVisitor.ItemsSource = listC;
                    comboBoxVisitor.SelectedItem = null;
                }
                List<PizzaViewModel> listP = Task.Run(() => APIClient.GetRequestData<List<PizzaViewModel>>("api/Pizza/GetList")).Result;
                if (listP != null)
                {
                    comboBoxPizza.DisplayMemberPath = "PizzaName";
                    comboBoxPizza.SelectedValuePath = "Id";
                    comboBoxPizza.ItemsSource = listP;
                    comboBoxPizza.SelectedItem = null;
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
                    PizzaViewModel product = Task.Run(() => APIClient.GetRequestData<PizzaViewModel>("api/Pizza/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)product.Price).ToString();
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
            int visitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue);
            int pizzaId = Convert.ToInt32(comboBoxPizza.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/Main/CreateOrderPizza", new OrderPizzaBindingModel
            {
                VisitorId = visitorId,
                PizzaId = pizzaId,
                Count = count,
                Sum = sum
            }));

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
