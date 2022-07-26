using System;
using System.Linq;
using System.Windows;


namespace MedicalCenter.Windows
{
    public partial class change_or_delete : Window
    {
        int id_pat, id_doc, data_id;
        TimeSpan time1 = new TimeSpan();
        string phone;
        
        public change_or_delete()
        {
            InitializeComponent();
        }
        public change_or_delete(int id_pat, int id_doc, int data_id, TimeSpan time, string phone) //передача данных для удаления записи
        {
            this.id_pat = id_pat;
            this.id_doc = id_doc;
            this.data_id = data_id;
            this.time1 = time;
            this.phone = phone;
            InitializeComponent();
        }

        private void change_Click(object sender, RoutedEventArgs e) // удаление из базы данных с переходом в окно добавления 
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    Time del_record = db.Time.FirstOrDefault(p => p.Time1 == time1 && p.DateId == data_id
                    && p.DoctorId == id_doc && p.PatientId == id_pat);

                    int? id_vis = del_record.VisitId;
                    Visits vis = db.Visits.FirstOrDefault(p => p.Id == id_vis);

                    db.Time.Remove(del_record);
                    db.Visits.Remove(vis);
                    db.SaveChanges();
                }
                Windows.add_record add_Record = new Windows.add_record(phone);
                add_Record.Show();
                Owner.Close();
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }            
            
        }

        private void del_Click(object sender, RoutedEventArgs e) //простое удаление из базы данных
        {

            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    Time del_record = db.Time.FirstOrDefault(p => p.Time1 == time1 && p.DateId == data_id
                    && p.DoctorId == id_doc && p.PatientId == id_pat);
                    
                    int? id_vis = del_record.VisitId;
                    Visits vis = db.Visits.FirstOrDefault(p => p.Id == id_vis);

                    db.Time.Remove(del_record);
                    db.Visits.Remove(vis);
                    db.SaveChanges();
                }
                admin admin = new admin();
                admin.Show();
                Owner.Close();
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }
            
        }
    }
}
