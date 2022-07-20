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

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{data} {lastname.Text}  {data.Text}", "", MessageBoxButton.OK);
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
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
                                     };
                        Grid.ItemsSource = record.ToList();
                    }
                    //поля фамилия и имя пациента отсутсвуют , остальные заполнены
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
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
                                     select new
                                     {
                                         Data = date.Date1,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
                                     select new
                                     {
                                         Data = date.Date1,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
                                     select new
                                     {
                                         Data = data_str,
                                         Time = time.Time1,
                                         Lastname_doc = doc.Lastname,
                                         Lastname_pat = pat.Lastname,
                                         Phone_pat = pat.Phone
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
    }
}
