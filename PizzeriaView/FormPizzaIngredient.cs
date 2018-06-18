using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormPizzaIngredient : Form
    {
        public PizzaIngredientViewModel Model { set { model = value; } get { return model; } }

        private PizzaIngredientViewModel model;

        public FormPizzaIngredient()
        {
            InitializeComponent();
        }

        private void FormPizzaIngredient_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIClient.GetRequest("api/Ingredient/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = APIClient.GetElement<List<IngredientViewModel>>(response);
                    comboBoxIngredient.SelectedItem = null;
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
            if (model != null)
            {
                comboBoxIngredient.Enabled = false;
                comboBoxIngredient.SelectedValue = model.IngredientId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new PizzaIngredientViewModel
                    {
                        IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                        IngredientName = comboBoxIngredient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
    }
}
