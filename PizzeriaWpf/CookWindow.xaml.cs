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
    /// Логика взаимодействия для CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public CookWindow()
        {
            InitializeComponent();
            Loaded += CookWindow_Load;
        }

        private void CookWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Cook/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var Cook = APIClient.GetElement<CookViewModel>(response);
                        textBoxFIO.Text = Cook.CookFIO;
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
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Cook/UpdElement", new CookBindingModel
                    {
                        Id = id.Value,
                        CookFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Cook/AddElement", new CookBindingModel
                    {
                        CookFIO = textBoxFIO.Text
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
