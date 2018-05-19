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
    /// Логика взаимодействия для PizzaIngredientWindow.xaml
    /// </summary>
    public partial class PizzaIngredientWindow : Window
    {
        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IIngredientService service;

        public PizzaIngredientViewModel Model { set { model = value; } get { return model; } }

        private PizzaIngredientViewModel model;

        public PizzaIngredientWindow(IIngredientService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += PizzaIngredientWindow_Load;
        }

        private void PizzaIngredientWindow_Load(object sender, EventArgs e)
        {
            List<IngredientViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBox.DisplayMemberPath = "IngredientName";
                    comboBox.SelectedValuePath = "Id";
                    comboBox.ItemsSource = list;
                    comboBox.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (model != null)
            {
                comboBox.IsEnabled = false;
                comboBox.SelectedValue = model.IngredientId;
                textBoxFIO.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new PizzaIngredientViewModel
                    {
                        IngredientId = Convert.ToInt32(comboBox.SelectedValue),
                        IngredientName = comboBox.Text,
                        Count = Convert.ToInt32(textBoxFIO.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxFIO.Text);
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
