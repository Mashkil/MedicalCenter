using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MedicalCenter.Windows
{

    public partial class change_patient : Window
    {
        int pat_id, doc_id, date_id;
        TimeSpan time_of_rec;
        string phone_number_pat;
        bool open_admin = false;
        
        public change_patient()
        {
            InitializeComponent();
        }

        private void find_button_Click(object sender, RoutedEventArgs e) // поиск и отображение записей 
        {
            try
            {
                if (data.Text == "")
                {
                    using (medcentrDB db = new medcentrDB())
                    {
                        var patients = from pat in db.Time
                                       where pat.PatientId == pat_id
                                       join doc in db.Doctors on pat.DoctorId equals doc.Id
                                       join data in db.Date on pat.DateId equals data.Id
                                       from patient in db.Patients
                                       where patient.Id == pat_id
                                       select new
                                       {
                                           Data = data.Date_in_text,
                                           Time = pat.Time1,
                                           Name = patient.Firstname,
                                           Surname = patient.Lastname,
                                           Doc_name = doc.Firstname,
                                           Doc_surname = doc.Lastname,
                                           id_doc = doc.Id
                                       };

                        grid.ItemsSource = patients.ToList();
                    }
                }
                else
                {
                    using (medcentrDB db = new medcentrDB())
                    {
                        DateTime dat = DateTime.Parse(data.Text);
                        var patients = from pat in db.Time
                                       where pat.PatientId == pat_id
                                       join doc in db.Doctors on pat.DoctorId equals doc.Id
                                       join data in db.Date on pat.DateId equals data.Id
                                       where data.Date1 == dat
                                       from patient in db.Patients
                                       where patient.Id == pat_id
                                       select new
                                       {
                                           Data = data.Date_in_text,
                                           Time = pat.Time1,
                                           Name = patient.Firstname,
                                           Surname = patient.Lastname,
                                           Doc_name = doc.Firstname,
                                           Doc_surname = doc.Lastname,
                                           id_doc = doc.Id
                                       };

                        grid.ItemsSource = patients.ToList();
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }

        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.StartsWith("Time"))
                e.Column.Header = "Время";
            if (e.PropertyName.StartsWith("Data"))
                e.Column.Header = "Дата записи";
            if (e.PropertyName.StartsWith("Name"))
                e.Column.Header = "Имя пациента";
            if (e.PropertyName.StartsWith("Surname"))
                e.Column.Header = "Фамилия пациента";
            if (e.PropertyName.StartsWith("Doc_name"))
                e.Column.Header = "Имя врача";
            if (e.PropertyName.StartsWith("Doc_surname"))
                e.Column.Header = "Фамилия врача";
            if (e.PropertyName.StartsWith("id_doc"))
                e.Column.Header = "Id врача";
        }

        private void phone_number_LostFocus(object sender, RoutedEventArgs e)//автозаполнение данных пациента по номеру телефона
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    Patients pat = db.Patients.FirstOrDefault(p => p.Phone == phone_number.Text);
                    surname_pat.Text = pat.Lastname;
                    name_pat.Text = pat.Firstname;
                    pat_id = pat.Id;
                    phone_number_pat = pat.Phone;
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show($"Пациента с таким номером телефона не существует");
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
            }

        }

        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e) // выбор определенной записи для редактирования
        {
            var ci = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[1]); //получение времени из ячейки
            var time = ci.Column.GetCellContent(ci.Item) as TextBlock;
            time_of_rec = TimeSpan.Parse(time.Text);

            var cc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[6]); //получение id доктора из ячейки
            var id_doc = cc.Column.GetCellContent(cc.Item) as TextBlock;
            doc_id = Convert.ToInt32(id_doc.Text);

            var ccc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[0]); //получение даты из ячейки
            var dat = ccc.Column.GetCellContent(ccc.Item) as TextBlock;

            DateTime date_ = DateTime.Parse(dat.Text);

            using (medcentrDB db = new medcentrDB())
            {
                Date dd = db.Date.FirstOrDefault(p => p.Date1 == date_);    //получение id даты
                date_id = dd.Id;
            }

            Windows.change_or_delete change_Or_Delete = new change_or_delete(pat_id, doc_id, date_id, time_of_rec, phone_number_pat);
            change_Or_Delete.Owner = this;
            open_admin = true;
            change_Or_Delete.ShowDialog();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!open_admin)
            {
                admin Admin = new admin();
                Admin.Show();
            }
        }
    }
}
