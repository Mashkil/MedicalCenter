using System;
using System.Windows;


namespace MedicalCenter.Windows
{
    public partial class create_new_pat : Window
    {
        bool dont_open_admin = false;
        public create_new_pat()
        {
            InitializeComponent();
        }

        private void create_pat_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                    using (medcentrDB db = new medcentrDB())

                    {
                        var pat = new Patients()
                        {
                            Firstname = name.Text,
                            Lastname = surname.Text,
                            Patronymic = patronimyc.Text,
                            Date_of_birth = DateTime.Parse(date_of_birth.Text),
                            Phone = phonenumber.Text,
                            Series_and_number_of_pass = series_and_number.Text,
                            Chronic_deseases = chronic_deseases.Text,
                            Date_of_birth_in_text = DateTime.Parse(date_of_birth.Text).ToShortDateString()
                        };

                        db.Patients.Add(pat);
                        db.SaveChanges();

                        if (MessageBox.Show($"Карта пациента {name.Text} успешно создана\nОткрыть окно записи?", "",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            dont_open_admin = true;
                            if (App.Current.MainWindow != null)
                                App.Current.MainWindow.Close();
                            Windows.add_record add_Record = new add_record();
                            add_Record.Show();
                            this.Close();
                        }
                        else
                        {
                            dont_open_admin = true;
                            Windows.create_new_pat create_New_Pat = new Windows.create_new_pat();
                            create_New_Pat.Show();
                            this.Close();
                        }
                    };
            }
            catch (Exception t)
            {
                MessageBox.Show($"{t.Message}", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!dont_open_admin)
            {
                admin admin = new admin();
                admin.Show();
            }
        }
    }
}
