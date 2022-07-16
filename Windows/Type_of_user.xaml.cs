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
    public partial class Type_of_user : Window
    {
        public Type_of_user()
        {
            InitializeComponent();
        }

        private void registr_admin_Click(object sender, RoutedEventArgs e)
        {
            Windows.registration_admin registration_Admin = new Windows.registration_admin();
            registration_Admin.Show();
            this.Hide();

        }

        private void registr_doctor_Click(object sender, RoutedEventArgs e)
        {
            Windows.Registration registration = new Windows.Registration();
            registration.Show();
            this.Hide();
        }
    }
}
