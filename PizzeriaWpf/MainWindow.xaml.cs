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
using Unity;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IMainService service;
        
        public MainWindow(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }
        
        private void LoadData()
        {
            try
            {
                List<OrderPizzaViewModel> list = service.GetList();
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void посетителиToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<VisitorsWindow>();
            form.ShowDialog();
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<IngredientsWindow>();
            form.ShowDialog();
        }

        private void пиццыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<PizzasWindow>();
            form.ShowDialog();
        }

        private void холодильникиToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FridgesWindow>();
            form.ShowDialog();
        }

        private void повараToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<CooksWindow>();
            form.ShowDialog();
        }

        private void пополнитьХолодильникToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<PutInFridgeWindow>();
            form.ShowDialog();
        }

        private void buttonOrderPizza_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderPizzaWindow>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeOrderInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVeiw.SelectedItem != null)
            {
                var form = Container.Resolve<TakeOrderPizzaInWorkWindow>();
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
                try
                {
                    service.FinishOrderPizza(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonPayOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVeiw.SelectedItem != null)
            {
                int id = ((OrderPizzaViewModel)dataGridVeiw.SelectedItem).Id;
                try
                {
                    service.PayOrderPizza(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
