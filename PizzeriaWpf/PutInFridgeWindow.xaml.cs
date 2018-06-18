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
    /// Логика взаимодействия для PutInFridgeWindow.xaml
    /// </summary>
    public partial class PutInFridgeWindow : Window
    {

        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IFridgeService serviceF;

        private readonly IIngredientService serviceI;

        private readonly IMainService serviceM;

        public PutInFridgeWindow(IFridgeService serviceF, IIngredientService serviceI, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceF = serviceF;
            this.serviceI = serviceI;
            this.serviceM = serviceM;
            Loaded += PutInFridgeWindow_Load;
        }

        private void PutInFridgeWindow_Load(object sender, EventArgs e)
        {
                try
                {
                    List<IngredientViewModel> listI = serviceI.GetList();
                    if (listI != null)
                    {
                        comboBoxIngredient.DisplayMemberPath = "IngredientName";
                        comboBoxIngredient.SelectedValuePath = "Id";
                        comboBoxIngredient.ItemsSource = listI;
                        comboBoxIngredient.SelectedItem = null;
                    }
                    List<FridgeViewModel> listF = serviceF.GetList();
                    if (listF != null)
                    {
                        comboBoxFridge.DisplayMemberPath = "FridgeName";
                        comboBoxFridge.SelectedValuePath = "Id";
                        comboBoxFridge.ItemsSource = listF;
                        comboBoxFridge.SelectedItem = null;
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
                serviceM.PutIngredientInFridge(new FridgeIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    FridgeId = Convert.ToInt32(comboBoxFridge.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
    }
}
