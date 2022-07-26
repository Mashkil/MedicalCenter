using System;
using System.Windows;

namespace MedicalCenter.Windows
{

    public partial class registration_admin : Window
    {
        public registration_admin()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            bool isInt = int.TryParse(age.Text, out int age1);

            if (name.Text.Length == 0)
                MessageBox.Show($"Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (surname.Text.Length == 0)
                MessageBox.Show($"Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (patronimyc.Text.Length == 0)
                MessageBox.Show($"Введите отчество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (age.Text.Length == 0)
                MessageBox.Show($"Введите возраст", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (salary.Text.Length == 0)
                MessageBox.Show($"Введите заработную плату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (education.Text.Length == 0)
                MessageBox.Show($"Введите образование", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!isInt)
                MessageBox.Show($"В поле Возраст введены неккоректные данные", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            else

            {
                using (medcentrDB db = new medcentrDB())
                {
                    var admins = new Admins()
                    {
                        Firstname = name.Text,
                        Lastname = surname.Text,
                        Patronimyc = patronimyc.Text,
                        Age = Convert.ToInt32(age.Text),
                        Salary = Convert.ToDecimal(salary.Text),
                        Education = education.Text
                    };
                    var pass = new Passwords()
                    {
                        Admin_or_doctor = 0,      // Если пользователь администратор , то это поле = 0 , если врач , то 1
                        Password = password.Text,
                        Id_admin=admins.Id
                    };
                    var log = new Logins()
                    {
                        Login = login.Text,
                        PassId = pass.Id
                    };

                    db.Admins.Add(admins);
                    db.Passwords.Add(pass);
                    db.Logins.Add(log);
                    db.SaveChanges();

                    if (MessageBox.Show($"Администратор {name.Text} успешно добавлен в систему", "", MessageBoxButton.OK, MessageBoxImage.None) == MessageBoxResult.OK)
                    {
                        if (App.Current.MainWindow != null)
                            App.Current.MainWindow.Close();

                        MainWindow main2 = new MainWindow();
                        main2.Show();
                        this.Close();
                    }

                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.Current.MainWindow != null)
                App.Current.MainWindow.Close();
            MainWindow main = new MainWindow();
            main.Show();            
        }
    }
}
