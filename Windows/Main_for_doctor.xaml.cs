using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicalCenter.Windows
{
    public partial class Main_for_doctor : Window
    {
        int id_doc;
        string name_pat, surname_pat, patr_pat, phone_apt, date_of_bth;
        int age, id_visit;

        public Main_for_doctor(int id)
        {
            InitializeComponent();
            id_doc = id;
            using (medcentrDB db = new medcentrDB())
            {
                var f = db.Doctors.FirstOrDefault(p => p.Id == id);
                greeting.Text = $"Здравствуйте, {f.Lastname} {f.Firstname} {f.Patronymic}";
                cout_of_vis();
            }
        }
        
        private void cout_of_vis()
        {
            using (medcentrDB db = new medcentrDB())
            {
                DateTime d1 = DateTime.Now.Date;
                Doctors doc1 = db.Doctors.FirstOrDefault(p => p.Id == id_doc);

                var records1 = from rec in db.Time
                               where rec.DoctorId == doc1.Id
                               join pat in db.Patients on rec.PatientId equals pat.Id
                               where pat.Id == rec.PatientId
                               join dat in db.Date on rec.DateId equals dat.Id
                               where dat.Date1 == d1
                               join serv in db.Services on rec.Id_service equals serv.Id
                               where serv.Id == rec.Id_service
                               orderby rec.Time1 ascending
                               join vis in db.Visits on rec.VisitId equals vis.Id
                               where vis.Id == rec.VisitId && (vis.Appointment == null || vis.Complaint == null || vis.Therapy == null)
                               select new
                               {
                                   id_vis = rec.VisitId
                               };

                int ee = records1.Count();
                count_of.Text = $"Сегодня к вам записано {records1.Count()} пациентов";
            }
        }

        private void find_Click(object sender, RoutedEventArgs e) // поиск записи по дате  
        { //TODO: НЕ ПОКАЗЫВАЕТ СПИСОК ПАЦИЕНТОВ , ИСПРАВИТЬ
            try
            {
                if (date.Text == "") //при отсутствии даты поиск идет по текущей дате
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
                                  join serv in db.Services on rec.Id_service equals serv.Id
                                  where serv.Id == rec.Id_service
                                  orderby rec.Time1 ascending
                                  select new
                                  {
                                      Date = dat.Date_in_text,
                                      Time = rec.Time_in_text,
                                      Serv_name = serv.Name_of_service,
                                      Name_pat = pat.Firstname,
                                      Surname_pat = pat.Lastname,
                                      patr_pat = pat.Patronymic,
                                      date_of_brth = pat.Date_of_birth_in_text,
                                      phone_num = pat.Phone,
                                      id_vis = rec.VisitId
                                  };

                    grid.ItemsSource = records.ToList();
                }
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
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
            if (e.PropertyName.StartsWith("id_vis"))
                e.Column.Header = "Id посещения";
            if (e.PropertyName.StartsWith("Serv_name"))
                e.Column.Header = "Наименование услуги";
        }

        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)// выбор пациента из списка
        {
            try
            {
                var ci = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[3]);
                var name = ci.Column.GetCellContent(ci.Item) as TextBlock;
                name_pat = name.Text;

                var cc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[4]);
                var surname = cc.Column.GetCellContent(cc.Item) as TextBlock;
                surname_pat = surname.Text;

                var ccc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[5]);
                var patr = ccc.Column.GetCellContent(ccc.Item) as TextBlock;
                patr_pat = patr.Text;

                var cic = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[6]);
                var dat_brth = cic.Column.GetCellContent(cic.Item) as TextBlock;
                date_of_bth = dat_brth.Text;

                var cicc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[7]);
                var ph = cicc.Column.GetCellContent(cicc.Item) as TextBlock;
                phone_apt = ph.Text;

                var ciccc = new DataGridCellInfo(grid.Items[grid.SelectedIndex], grid.Columns[8]);
                var vis = ciccc.Column.GetCellContent(ciccc.Item) as TextBlock;
                id_visit = Convert.ToInt32(vis.Text);


                #region Вычисление возраста
                DateTime now = DateTime.Now;
                age = now.Year - DateTime.Parse(dat_brth.Text).Year;
                if (DateTime.Parse(date_of_bth) > now.AddYears(-age))
                    age--;
                #endregion

                visit visit = new visit(name_pat, surname_pat, patr_pat, phone_apt, date_of_bth, age, id_visit);
                visit.Show();
                cout_of_vis();
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}");
                throw;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Перейти в окно входа в учетную запись?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
