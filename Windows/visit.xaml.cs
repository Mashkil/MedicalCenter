using System;
using System.Linq;
using System.Windows;

namespace MedicalCenter.Windows
{

    public partial class visit : Window
    {
        bool show_messsage = true;
        int id_visit;
        public visit()
        {
            InitializeComponent();            
        }

        public visit(string name, string surname, string patr, string phone, string date_of_bt, int age, int id_visit)
        {
            InitializeComponent();
            
            this.name.Text = name;
            this.surname.Text = surname;
            this.patr.Text = patr;
            this.phone_n.Text = phone;
            this.date_of_birth.Text = date_of_bt;
            this.age.Text = Convert.ToString(age);
            this.id_visit = id_visit;
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    var visit = db.Visits.FirstOrDefault(p => p.Id == id_visit);
                    if (visit.Therapy != "")
                    {
                        therapy.Text = visit.Therapy;
                    }
                    if (visit.Appointment != "")
                    {
                        appointment.Text = visit.Appointment;
                    }
                    if (visit.Complaint != "")
                    {
                        complaints.Text = visit.Complaint;
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }
            
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                show_messsage = false;
                using (medcentrDB db = new medcentrDB())
                {
                    var visit = db.Visits.FirstOrDefault(p => p.Id == id_visit);
                    if (visit != null)
                    {
                        visit.Appointment = appointment.Text;
                        visit.Therapy = therapy.Text;
                        visit.Complaint = complaints.Text;

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (complaints.Text != "" || appointment.Text != "" || therapy.Text != "")
                if (show_messsage)
                {
                    if (MessageBox.Show("Вы уверены, что хотите закрыть окно?\nДанные не будут сохранены", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        e.Cancel = true;
                }
        }
    }
}
