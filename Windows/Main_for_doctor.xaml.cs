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
    public partial class Main_for_doctor : Window
    {
        int id_doc;
        public Main_for_doctor(int id)
        {
            InitializeComponent();
            id_doc = id;
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            if (date.Text == "")
            {
                date.Text = DateTime.Now.ToString();
            }

            using (medcentrDB db = new medcentrDB())
            {
                DateTime d = DateTime.Parse(date.Text);
                Doctors doc = db.Doctors.FirstOrDefault(p => p.Id == id_doc);

                var records = from rec in db.Time
                              where rec.DoctorId == doc.Id
                              join pat in db.Patients on rec.PatientId equals pat.Id
                              where pat.Id == rec.PatientId
                              join dat in db.Date on rec.DateId equals dat.Id
                              where dat.Date1 == d
                              orderby rec.Time1 descending
                              select new
                              {
                                  Date = dat.Date_in_text,
                                  Time = rec.Time1,
                                  Name_pat = pat.Firstname,
                                  Surname_pat = pat.Lastname,
                                  patr_pat = pat.Patronymic,
                                  date_of_brth = pat.Date_of_birth,
                                  phone_num = pat.Phone
                              };

                grid.ItemsSource = records.ToList();
            }
        }

        private void grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.StartsWith("Date"))
                e.Column.Header = "Дата";
            if (e.PropertyName.StartsWith("Time"))
                e.Column.Header = "Время записи";
            if (e.PropertyName.StartsWith("Name_pat"))
                e.Column.Header = "Имя пациента";
            if (e.PropertyName.StartsWith("Surname_pat"))
                e.Column.Header = "Фамилия пациента";
            if (e.PropertyName.StartsWith("patr_pat"))
                e.Column.Header = "Отчество пациента";
            if (e.PropertyName.StartsWith("date_of_brth"))
                e.Column.Header = "Дата рождения пациента";
            if (e.PropertyName.StartsWith("phone_num"))
                e.Column.Header = "Номер телефона пациента";
        }

        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
