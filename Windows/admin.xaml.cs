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
            
            Windows.about_doctors about_Doctors = new Windows.about_doctors();
            about_Doctors.ShowDialog();
        }

        private void change_record_Click(object sender, RoutedEventArgs e)
        {
            Windows.change_patient change_Patient = new Windows.change_patient();
            change_Patient.ShowDialog();
            this.Close();
        }

        private void show_record_Click(object sender, RoutedEventArgs e)
        {
            Windows.Show_records show_Records = new Windows.Show_records();
            show_Records.ShowDialog();
        }

        private void add_record_Click(object sender, RoutedEventArgs e)
        {
            Windows.add_record add_Record = new Windows.add_record();
            add_Record.ShowDialog();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();            
        }
    }
}
