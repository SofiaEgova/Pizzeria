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
    /// Логика взаимодействия для IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IIngredientService service;

        public IngredientsWindow(IIngredientService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += IngredientsWindow_Load;
        }

        private void IngredientsWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<IngredientViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGrid.ItemsSource = list;
                    dataGrid.Columns[0].Visibility = Visibility.Hidden;
                    dataGrid.Columns[1].Width = dataGrid.Width - 8;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<IngredientWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var form = Container.Resolve<IngredientWindow>();
                form.Id = ((IngredientViewModel)dataGrid.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((IngredientViewModel)dataGrid.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
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
    }
}
