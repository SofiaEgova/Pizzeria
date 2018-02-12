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
    public partial class FormOrderPizza : Form
    {
        [Unity.Attributes.Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IVisitorService serviceV;

        private readonly IPizzaService serviceP;

        private readonly IMainService serviceM;

        public FormOrderPizza(IVisitorService serviceV, IPizzaService serviceP, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceV = serviceV;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }

        private void FormOrderPizza_Load(object sender, EventArgs e)
        {
            try
            {
                List<VisitorViewModel> listC = serviceV.GetList();
                if (listC != null)
                {
                    comboBoxVisitor.DisplayMember = "VisitorFIO";
                    comboBoxVisitor.ValueMember = "Id";
                    comboBoxVisitor.DataSource = listC;
                    comboBoxVisitor.SelectedItem = null;
                }
                List<PizzaViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBoxPizza.DisplayMember = "PizzaName";
                    comboBoxPizza.ValueMember = "Id";
                    comboBoxPizza.DataSource = listP;
                    comboBoxPizza.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxPizza.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPizza.SelectedValue);
                    PizzaViewModel pizza = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * pizza.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                MessageBox.Show("Выберите посетителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPizza.SelectedValue == null)
            {
                MessageBox.Show("Выберите пиццу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.CreateOrderPizza(new OrderPizzaBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    PizzaId = Convert.ToInt32(comboBoxPizza.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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

        private void comboBoxPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
    }
}
