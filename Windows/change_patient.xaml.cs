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

namespace MedicalCenter.Windows
{
    
    public partial class change_patient : Window
    {
        public change_patient()
        {
            InitializeComponent();
        }

        private void find_button_Click(object sender, RoutedEventArgs e)
        {
            using (medcentrDB db = new medcentrDB())
            {
                
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.StartsWith("Time"))
                e.Column.Header = "Время";
            if (e.PropertyName.StartsWith("Lastname_doc"))
                e.Column.Header = "Фамилия врача";
            if (e.PropertyName.StartsWith("Lastname_pat"))
                e.Column.Header = "Фамилия пациента";
            if (e.PropertyName.StartsWith("Phone_pat"))
                e.Column.Header = "Номер телефона пациента";
            if (e.PropertyName.StartsWith("Data"))
                e.Column.Header = "Дата";
        }
    }
}
