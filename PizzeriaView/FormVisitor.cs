using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormVisitor : Form
    {

        public int Id { set { id = value; } }

        private int? id;

        public FormVisitor()
        {
            InitializeComponent();
        }

        private void FormVisitor_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var visitor = Task.Run(() => APIClient.GetRequestData<VisitorViewModel>("api/Visitor/Get/" + id.Value)).Result;
                    textBoxFIO.Text = visitor.VisitorFIO;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fio = textBoxFIO.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Visitor/UpdElement", new VisitorBindingModel
                {
                    Id = id.Value,
                    VisitorFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Visitor/AddElement", new VisitorBindingModel
                {
                    VisitorFIO = fio
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
