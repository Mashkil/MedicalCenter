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

    public partial class add_record : Window
    {
        public add_record()
        {
            InitializeComponent();
        }        

        private void serial_and_number_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {                    
                    Patients pat = db.Patients.FirstOrDefault(p => p.Series_and_number_of_pass == serial_and_number.Text);
                    name.Text = pat.Firstname;
                    surname.Text = pat.Lastname;
                    patronimyc.Text = pat.Patronymic;
                    phone_number.Text = pat.Phone;
                };
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Пациета с такими паспортными данными не существует", "Ошибка", MessageBoxButton.OK);
            }
            catch (Exception t)
            {              
                
                MessageBox.Show($"{t.Message} ", "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
