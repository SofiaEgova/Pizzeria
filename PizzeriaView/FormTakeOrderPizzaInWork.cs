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
    public partial class FormTakeOrderPizzaInWork : Form
    {
        [Unity.Attributes.Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICookService serviceС;

        private readonly IMainService serviceM;

        private int? id;

        public FormTakeOrderPizzaInWork(ICookService serviceС, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceС = serviceС;
            this.serviceM = serviceM;
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
                List<CookViewModel> listС = serviceС.GetList();
                if (listС != null)
                {
                    comboBoxCook.DisplayMember = "CookFIO";
                    comboBoxCook.ValueMember = "Id";
                    comboBoxCook.DataSource = listС;
                    comboBoxCook.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                serviceM.TakeOrderPizzaInWork(new OrderPizzaBindingModel
                {
                    Id = id.Value,
                    CookId = Convert.ToInt32(comboBoxCook.SelectedValue)
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
