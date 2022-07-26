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
    
    public partial class visits_for_admin : Window
    {
        public visits_for_admin(int id_vis, string name_pat, string surname_pat, string patr_pat, string date_of_b_pat,string fio_doc, string specialiation, string date_of_rec, string time_of_rec)
        {            
            InitializeComponent();
            name.Text = name_pat;
            surname.Text = surname_pat;
            patr.Text = patr_pat;
            date_of_birth.Text = date_of_b_pat;
            this.fio_doc.Text = fio_doc;
            this.specialization.Text = specialiation;
            this.date_of_rec.Text = date_of_rec;
            this.time_of_rec.Text = time_of_rec;
            
            using (medcentrDB db = new medcentrDB())
            {
                var vis = db.Visits.FirstOrDefault(p => p.Id == id_vis);
                this.complaints.Text = vis.Complaint;
                this.appointment.Text= vis.Appointment;
                this.therapy.Text = vis.Therapy;
            }
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
