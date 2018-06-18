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
using Unity;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для OrderPizzaWindow.xaml
    /// </summary>
    public partial class OrderPizzaWindow : Window
    {

        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IVisitorService serviceV;

        private readonly IPizzaService serviceP;

        private readonly IMainService serviceM;

        public OrderPizzaWindow(IVisitorService serviceV, IPizzaService serviceP, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceV = serviceV;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
            Loaded += OrderPizzaWindow_Load;
            comboBoxPizza.SelectionChanged += comboBoxPizza_SelectionChanged;
            comboBoxPizza.SelectionChanged += new SelectionChangedEventHandler(comboBoxPizza_SelectionChanged);
        }

        private void OrderPizzaWindow_Load(object sender, EventArgs e)
        {
            try
            {
                List<VisitorViewModel> listV = serviceV.GetList();
                if (listV != null)
                {
                    comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                    comboBoxVisitor.SelectedValuePath = "Id";
                    comboBoxVisitor.ItemsSource = listV;
                    comboBoxVisitor.SelectedItem = null;
                }
                List<PizzaViewModel> listP = serviceP.GetList();
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
                    PizzaViewModel pizza = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * pizza.Price).ToString();
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
                serviceM.CreateOrderPizza(new OrderPizzaBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    PizzaId = Convert.ToInt32(comboBoxPizza.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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
