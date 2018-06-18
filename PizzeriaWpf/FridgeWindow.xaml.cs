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
    /// Логика взаимодействия для FridgeWindow.xaml
    /// </summary>
    public partial class FridgeWindow : Window
    {

        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IFridgeService service;

        private int? id;

        public FridgeWindow(IFridgeService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += FridgeWindow_Load;
        }

        private void FridgeWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    FridgeViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.FridgeName;
                        dataGrid.ItemsSource = view.FridgeIngredients;
                        dataGrid.Columns[0].Visibility = Visibility.Hidden;
                        dataGrid.Columns[1].Visibility = Visibility.Hidden;
                        dataGrid.Columns[2].Visibility = Visibility.Hidden;
                        dataGrid.Columns[3].Width = dataGrid.Width - 8;
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
                if (id.HasValue)
                {
                    service.UpdElement(new FridgeBindingModel
                    {
                        Id = id.Value,
                        FridgeName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new FridgeBindingModel
                    {
                        FridgeName = textBoxName.Text
                    });
                }
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
    }
}
