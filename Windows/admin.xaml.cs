using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MedicalCenter
{
    public partial class admin : Window
    {
        public admin()
        {
            InitializeComponent();
        }

        private void adout_doctors_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            Windows.about_doctors about_Doctors = new Windows.about_doctors();
            about_Doctors.ShowDialog();
        }
    }
}
