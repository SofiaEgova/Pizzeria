using Microsoft.Win32;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using Unity.Attributes;

namespace PizzeriaWpf
{
    /// <summary>
    /// Логика взаимодействия для VisitorOrderPizzasWindow.xaml
    /// </summary>
    public partial class VisitorOrderPizzasWindow : Window
    {

        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IReportService service;

        private bool _isReportViewerLoaded;

        public VisitorOrderPizzasWindow(IReportService service)
        {
            InitializeComponent();
            this.service = service;
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                if (!_isReportViewerLoaded)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource();

                    Microsoft.Reporting.WinForms.ReportDataSource source = new
                        Microsoft.Reporting.WinForms.ReportDataSource("DataSetOrderPizzas");
                    _reportViewer.LocalReport.DataSources.Add(source);
                    _reportViewer.RefreshReport();
                    _isReportViewerLoaded = true;
                }
            }
        }

        private void buttonMake_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Microsoft.Reporting.WinForms.ReportParameter parameter =
                    new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePickerFrom.SelectedDate.ToString() +
                                            " по " + dateTimePickerTo.SelectedDate.ToString());
                _reportViewer.LocalReport.SetParameters(parameter);

                var dataSource = service.GetVisitorOrderPizzas(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                });
                Microsoft.Reporting.WinForms.ReportDataSource source = new
                    Microsoft.Reporting.WinForms.ReportDataSource("DataSetOrderPizzas", dataSource);
                _reportViewer.LocalReport.DataSources.Add(source);

                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void buttonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    service.SaveVisitorOrderPizzas(new ReportBindingModel
                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePickerFrom.SelectedDate,
                        DateTo = dateTimePickerTo.SelectedDate
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
