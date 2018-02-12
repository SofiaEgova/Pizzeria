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
using Unity;

namespace PizzeriaView
{
    public partial class FormPutInFridge : Form
    {
        [Unity.Attributes.Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IFridgeService serviceF;

        private readonly IIngredientService serviceI;

        private readonly IMainService serviceM;

        public FormPutInFridge(IFridgeService serviceF, IIngredientService serviceI, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceF = serviceF;
            this.serviceI = serviceI;
            this.serviceM = serviceM;
        }

        private void FormPutInFridge_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngredientViewModel> listI = serviceI.GetList();
                if (listI != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = listI;
                    comboBoxIngredient.SelectedItem = null;
                }
                List<FridgeViewModel> listF = serviceF.GetList();
                if (listF != null)
                {
                    comboBoxFridge.DisplayMember = "FridgeName";
                    comboBoxFridge.ValueMember = "Id";
                    comboBoxFridge.DataSource = listF;
                    comboBoxFridge.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFridge.SelectedValue == null)
            {
                MessageBox.Show("Выберите холодильник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
