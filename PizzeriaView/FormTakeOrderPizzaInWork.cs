using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormTakeOrderPizzaInWork : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormTakeOrderPizzaInWork()
        {
            InitializeComponent();
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/TakeOrderPizzaInWork", new OrderPizzaBindingModel
                {
                    Id = id.Value,
                    CookId = Convert.ToInt32(comboBoxCook.SelectedValue)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormTakeOrderPizzaInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                var response = APIClient.GetRequest("api/Cook/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CookViewModel> list = APIClient.GetElement<List<CookViewModel>>(response);
                    if (list != null)
                    {
                        comboBoxCook.DisplayMember = "CookFIO";
                        comboBoxCook.ValueMember = "Id";
                        comboBoxCook.DataSource = list;
                        comboBoxCook.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
