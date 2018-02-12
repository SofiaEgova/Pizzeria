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
    public partial class FormMain : Form
    {
        [Unity.Attributes.Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public FormMain(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void посетителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormVisitors>();
            form.ShowDialog();
        }

        private void LoadData()
        {
            try
            {
                List<OrderPizzaViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIngredients>();
            form.ShowDialog();
        }

        private void повараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCooks>();
            form.ShowDialog();
        }

        private void холодильникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFridges>();
            form.ShowDialog();
        }

        private void пиццыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPizzas>();
            form.ShowDialog();
        }

        private void пополнитьХолодильникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPutInFridge>();
            form.ShowDialog();
        }

        private void buttonOrderPizza_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOrderPizza>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormTakeOrderPizzaInWork>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.FinishOrderPizza(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.PayOrderPizza(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
