using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormOrderPizza : Form
    {
        public FormOrderPizza()
        {
            InitializeComponent();
        }

        private void CalcSum()
        {
            if (comboBoxPizza.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPizza.SelectedValue);
                    var responseP = APIClient.GetRequest("api/Pizza/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        PizzaViewModel Pizza = APIClient.GetElement<PizzaViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)Pizza.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxVisitor.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPizza.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/CreateOrderPizza", new OrderPizzaBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    PizzaId = Convert.ToInt32(comboBoxPizza.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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

        private void FormOrderPizza_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Visitor/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<VisitorViewModel> list = APIClient.GetElement<List<VisitorViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxVisitor.DisplayMember = "VisitorFIO";
                        comboBoxVisitor.ValueMember = "Id";
                        comboBoxVisitor.DataSource = list;
                        comboBoxVisitor.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/Pizza/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<PizzaViewModel> list = APIClient.GetElement<List<PizzaViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxPizza.DisplayMember = "PizzaName";
                        comboBoxPizza.ValueMember = "Id";
                        comboBoxPizza.DataSource = list;
                        comboBoxPizza.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
