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
    /// Логика взаимодействия для TakeOrderPizzaInWorkWindow.xaml
    /// </summary>
    public partial class TakeOrderPizzaInWorkWindow : Window
    {
        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICookService serviceС;

        private readonly IMainService serviceM;

        private int? id;
        public TakeOrderPizzaInWorkWindow(ICookService serviceС, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceС = serviceС;
            this.serviceM = serviceM;
            Loaded += TakeOrderPizzaInWorkWindow_Load;
        }

        private void TakeOrderPizzaInWorkWindow_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                List<CookViewModel> listС = serviceС.GetList();
                if (listС != null)
                {
                    comboBoxCook.DisplayMemberPath = "CookFIO";
                    comboBoxCook.SelectedValuePath = "Id";
                    comboBoxCook.ItemsSource = listС;
                    comboBoxCook.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.TakeOrderPizzaInWork(new OrderPizzaBindingModel
                {
                    Id = id.Value,
                    CookId = Convert.ToInt32(comboBoxCook.SelectedValue)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
