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
    public partial class add_record : Window
    {
        int patient_id;
        public add_record()
        {
            InitializeComponent();
            Select_depart();
        }

        public void Select_depart()      //вывод специальностей врачей
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    var dep_doc = from t in db.Doctors
                                  select new { t.Position };

                    foreach (var dep in dep_doc.Distinct())
                        doctors_depart.Items.Add(dep.Position);
                };
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message} ", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void serial_and_number_LostFocus(object sender, RoutedEventArgs e)       //автозаполнение полей данных пациента
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    Patients pat = db.Patients.FirstOrDefault(p => p.Series_and_number_of_pass == serial_and_number.Text);
                    patient_id = pat.Id;
                    name.Text = pat.Firstname;
                    surname.Text = pat.Lastname;
                    patronimyc.Text = pat.Patronymic;
                    phone_number.Text = pat.Phone;
                };
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Пациета с такими паспортными данными не существует", "Ошибка", MessageBoxButton.OK);
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message} ", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void doctors_depart_DropDownClosed(object sender, EventArgs e)  //Показ докторов
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    var doc = from d in db.Doctors
                              where d.Position == doctors_depart.Text
                              select new { d.Firstname, d.Lastname };

                    foreach (var y in doc)
                        doctors.Items.Add(y.Lastname + " " + y.Firstname);
                };
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message} ", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void doctors_depart_GotFocus(object sender, RoutedEventArgs e)  // очистка списка докторов , при изменении специальности
        {
            doctors.Items.Clear();
        }

        private void date_of_record_LostFocus(object sender, RoutedEventArgs e)  // Проверка на выходные дни и предыдущие дни
        {
            if (date_of_record.Text != "")
            {
                if (DateTime.Parse(date_of_record.Text).DayOfWeek.Equals(DayOfWeek.Sunday) ||
                    DateTime.Parse(date_of_record.Text).DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    MessageBox.Show("Вы выбрали выходной день", "Ошибка", MessageBoxButton.OK);
                    date_of_record.Text = "";
                }
                else if (DateTime.Parse(date_of_record.Text) < DateTime.Now)
                {
                    MessageBox.Show("Вы не можете создать запись на предыдущее число", "Ошибка", MessageBoxButton.OK);
                    date_of_record.Text = "";
                }
            }
        }

        private void time_of_record_GotFocus(object sender, RoutedEventArgs e)  // показ времени
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    string[] name_and_surname = doctors.Text.Split(' ');  //разделение фио врача на отдельные имя и фамилию
                    var surname_doc = name_and_surname[0];
                    var name_doc = name_and_surname[1];

                    Doctors doc = db.Doctors.FirstOrDefault(p => p.Lastname == surname_doc && p.Firstname == name_doc && p.Position == doctors_depart.Text);
                    var for_date_id = DateTime.Parse(date_of_record.Text);

                    #region  Определение свободного времени по дате                    
                    Date date = db.Date.FirstOrDefault(p => p.Date1 == for_date_id);
                    List<Time> time_for_record = new List<Time>();

                    if (date != null)
                        time_for_record = db.Time.Where(t => t.DoctorId == doc.Id && t.DateId == date.Id).ToList();
                    #endregion

                    List<string> time_str = new List<string>
                    {
                            "09:00:00", "09:30:00","10:00:00","10:30:00","11:00:00", "11:30:00", "12:00:00","12:30:00",
                            "14:00:00","14:30:00","15:00:00","15:30:00","16:00:00","16:30:00","17:00:00","17:30:00",
                            "18:00:00"
                    };
                    List<string> time_str2 = new List<string>();
                    foreach (var time in time_for_record)
                    {
                        time_str2.Add(time.Time1.ToString());
                    }
                    var time_str_result = time_str.Except(time_str2); //вычисление свободного времени
                    #region Удаление :00 в каждой строке, для корректного отображения времени
                    string[] time_str3 = new string[time_str_result.Count()];
                    int i2 = 0;
                    foreach (var time in time_str_result)
                    {
                        time_str3[i2] = time.ToString();
                        i2++;
                    }
                    for (int i = 0; i < time_str3.Length; i++)
                    {
                        string ii = time_str3[i];
                        time_str3[i] = ii.Remove(5, 3);
                    }
                    #endregion
                    time_of_record.ItemsSource = time_str3;
                };
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message} ", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void create_record_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
