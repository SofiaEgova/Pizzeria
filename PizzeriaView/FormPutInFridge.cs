using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormPutInFridge : Form
    {
        public FormPutInFridge()
        {
            InitializeComponent();
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
            if (comboBoxFridge.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int ingredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue);
                int fridgeId = Convert.ToInt32(comboBoxFridge.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PutIngredientInFridge", new FridgeIngredientBindingModel
                {
                    IngredientId = ingredientId,
                    FridgeId = fridgeId,
                    Count = count
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Холодильник пополнен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);

                Close();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormPutInFridge_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngredientViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<IngredientViewModel>>("api/Ingredient/GetList")).Result;
                if (listC != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = listC;
                    comboBoxIngredient.SelectedItem = null;
                }

                List<FridgeViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<FridgeViewModel>>("api/Fridge/GetList")).Result;
                if (listS != null)
                {
                    comboBoxFridge.DisplayMember = "FridgeName";
                    comboBoxFridge.ValueMember = "Id";
                    comboBoxFridge.DataSource = listS;
                    comboBoxFridge.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
