using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MedicalCenter.Windows
{
    public partial class Show_records : Window
    {

        public Show_records()
        {
            InitializeComponent();
        }

        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            if (e.PropertyName.StartsWith("Name_of_serv"))
                e.Column.Header = "Услуга";
            if (e.PropertyName.StartsWith("Cost_of_serv"))
                e.Column.Header = "Стоимость";

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    // заполнены все поля 
                    if (data.Text != "" && lastname.Text != "" && pat_surname.Text != "" && pat_name.Text != "")
                    {
                        DateTime data1 = DateTime.Parse(data.Text);
                        string data_str = DateTime.Parse(data.Text).ToString("d");
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     where doc.Lastname == lastname.Text
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Firstname == pat_name.Text
                                     where pat.Lastname == pat_surname.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     where date.Date1 == data1
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id==time.Id_service
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv= serv.Cost,
                                         id_ = doc.Id
                                     };
                        Grid.ItemsSource = record.ToList();
                    }
                    //поля фамилия и имя пациента отсутствуют , остальные заполнены
                    else if (data.Text != "" && lastname.Text != "" && pat_surname.Text == "" && pat_name.Text == "")
                    {
                        DateTime data1 = DateTime.Parse(data.Text);
                        string data_str = DateTime.Parse(data.Text).ToString("d");
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     where doc.Lastname == lastname.Text
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     join date in db.Date on time.DateId equals date.Id
                                     where date.Date1 == data1
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };

                        Grid.ItemsSource = record.ToList();
                    }
                    // поле дата отсутствует, остальные поля заполнены
                    else if (data.Text == "" && lastname.Text != "" && pat_surname.Text != "" && pat_name.Text != "")
                    {
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     where doc.Lastname == lastname.Text
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Firstname == pat_name.Text
                                     where pat.Lastname == pat_surname.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = date.Date_in_text,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };
                        Grid.ItemsSource = record.ToList();
                    }
                    // поле фамилия врача и поле дата отсутствуют , отстальные поля заполнены
                    else if (lastname.Text == "" && data.Text == "" && pat_surname.Text != "" && pat_name.Text != "")
                    {
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Firstname == pat_name.Text
                                     where pat.Lastname == pat_surname.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = date.Date_in_text,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };

                        Grid.ItemsSource = record.ToList();
                    }
                    // поле фамилия врача отсутсвует, остальные поля заполнены
                    else if (lastname.Text == "" && data.Text != "" && pat_surname.Text != "" && pat_name.Text != "")
                    {
                        DateTime data1 = DateTime.Parse(data.Text);
                        string data_str = DateTime.Parse(data.Text).ToString("d");
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Firstname == pat_name.Text
                                     where pat.Lastname == pat_surname.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     where date.Date1 == data1
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };

                        Grid.ItemsSource = record.ToList();
                    }

                    // поле фамилия пациента отсутствует , остальные поля заполнены
                    else if (pat_surname.Text == "" && data.Text != "" && lastname.Text != "" && pat_name.Text != "")
                    {
                        DateTime data1 = DateTime.Parse(data.Text);
                        string data_str = DateTime.Parse(data.Text).ToString("d");
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     where doc.Lastname == lastname.Text
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Firstname == pat_name.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     where date.Date1 == data1
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };

                        Grid.ItemsSource = record.ToList();
                    }
                    // поле имя пациента отсутствует , остальные поля заполнены
                    else if (pat_name.Text == "" && data.Text != "" && lastname.Text != "" && pat_surname.Text != "")
                    {
                        DateTime data1 = DateTime.Parse(data.Text);
                        string data_str = DateTime.Parse(data.Text).ToString("d");
                        var record = from time in db.Time
                                     join doc in db.Doctors on time.DoctorId equals doc.Id
                                     where doc.Lastname == lastname.Text
                                     join pat in db.Patients on time.PatientId equals pat.Id
                                     where pat.Lastname == pat_surname.Text
                                     join date in db.Date on time.DateId equals date.Id
                                     where date.Date1 == data1
                                     join serv in db.Services on time.Id_service equals serv.Id
                                     where serv.Id == time.Id_service
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time_in_text,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone,
                                         Name_of_serv = serv.Name_of_service,
                                         Cost_of_serv = serv.Cost,
                                         id_ = doc.Id
                                     };

                        Grid.ItemsSource = record.ToList();
                    }
                    else
                        MessageBox.Show($"Проверьте корректность ввдененных данных", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void Grid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//просмотр посещения с назанчениями
        {

            try
            {
                int id_vis;
                string name_pat, surname_pat, patr_pat, date_of_b, serv_name, date_of_rec, time_of_rec, fio_doc;
                var buf = new DataGridCellInfo(Grid.Items[Grid.SelectedIndex], Grid.Columns[0]);
                var dat = buf.Column.GetCellContent(buf.Item) as TextBlock;
                DateTime d1 = DateTime.Parse(dat.Text);

                var buf1 = new DataGridCellInfo(Grid.Items[Grid.SelectedIndex], Grid.Columns[1]);
                var time = buf1.Column.GetCellContent(buf1.Item) as TextBlock;
                TimeSpan t1 = TimeSpan.Parse(time.Text);

                var buf2 = new DataGridCellInfo(Grid.Items[Grid.SelectedIndex], Grid.Columns[4]);
                var nom = buf2.Column.GetCellContent(buf2.Item) as TextBlock;

                var buf4 = new DataGridCellInfo(Grid.Items[Grid.SelectedIndex], Grid.Columns[5]);
                var serv = buf4.Column.GetCellContent(buf4.Item) as TextBlock;

                var buf3 = new DataGridCellInfo(Grid.Items[Grid.SelectedIndex], Grid.Columns[7]);
                var id_doctor = buf3.Column.GetCellContent(buf3.Item) as TextBlock;

                int id1 = Convert.ToInt32(id_doctor.Text);

                using (medcentrDB db = new medcentrDB())
                {
                    var dat1 = db.Date.FirstOrDefault(p => p.Date1 == d1);
                    var pat = db.Patients.FirstOrDefault(p => p.Phone == nom.Text);
                    var doc1 = db.Doctors.FirstOrDefault(p => p.Id == id1);
                    var time1 = db.Time.FirstOrDefault(p => p.DateId == dat1.Id && p.DoctorId == doc1.Id && p.PatientId == pat.Id);
                    var vis = db.Visits.FirstOrDefault(p => p.Id == time1.VisitId);
                    var sr = db.Services.FirstOrDefault(p => p.Id == vis.Id_service);

                    id_vis = vis.Id;
                    name_pat = pat.Firstname;
                    surname_pat = pat.Lastname;
                    patr_pat = pat.Patronymic;
                    date_of_b = pat.Date_of_birth_in_text;
                    serv_name=sr.Name_of_service;
                    date_of_rec = dat.Text;
                    time_of_rec = time.Text;
                    fio_doc = doc1.Lastname + " " + doc1.Firstname.Substring(0, 1) + "." + doc1.Patronymic.Substring(0, 1) + ".";
                }
                Windows.visits_for_admin visits_For_Admin = new visits_for_admin(id_vis, name_pat, surname_pat, patr_pat, date_of_b, fio_doc, serv_name, date_of_rec, time_of_rec);
                visits_For_Admin.ShowDialog();
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }

        }
    }
}
