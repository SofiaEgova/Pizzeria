using PizzeriaService.BindingModels;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormPizza : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<PizzaIngredientViewModel> PizzaIngredients;

        public FormPizza()
        {
            InitializeComponent();
        }

        private void FormPizza_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Pizza/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var Pizza = APIClient.GetElement<PizzaViewModel>(response);
                        textBoxName.Text = Pizza.PizzaName;
                        textBoxPrice.Text = Pizza.Price.ToString();
                        PizzaIngredients = Pizza.PizzaIngredients;
                        LoadData();
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
            else
            {
                PizzaIngredients = new List<PizzaIngredientViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (PizzaIngredients != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = PizzaIngredients;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormPizzaIngredient();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.PizzaId = id.Value;
                    }
                    PizzaIngredients.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormPizzaIngredient();
                form.Model = PizzaIngredients[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PizzaIngredients[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        PizzaIngredients.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (PizzaIngredients == null || PizzaIngredients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<PizzaIngredientBindingModel> PizzaIngredientBM = new List<PizzaIngredientBindingModel>();
                for (int i = 0; i < PizzaIngredients.Count; ++i)
                {
                    PizzaIngredientBM.Add(new PizzaIngredientBindingModel
                    {
                        Id = PizzaIngredients[i].Id,
                        PizzaId = PizzaIngredients[i].PizzaId,
                        IngredientId = PizzaIngredients[i].IngredientId,
                        Count = PizzaIngredients[i].Count
                    });
                }
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Pizza/UpdElement", new PizzaBindingModel
                    {
                        Id = id.Value,
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredients = PizzaIngredientBM
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Pizza/AddElement", new PizzaBindingModel
                    {
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredients = PizzaIngredientBM
                    });
                }
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
    }
}
