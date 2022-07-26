using System;
using System.Windows;

namespace MedicalCenter.Windows
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            bool isInt = int.TryParse(age.Text, out int age1);
            bool isInt2 = int.TryParse(expirience.Text, out int age2);
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
            else if (expirience.Text.Length == 0)
                MessageBox.Show($"Введите опыт работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (education.Text.Length == 0)
                MessageBox.Show($"Введите образование", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (position.Text.Length == 0)
                MessageBox.Show($"Введите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (login.Text.Length < 3)
                MessageBox.Show($"Логин должен состоять из 3х и более символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (password.Text.Length < 5)
                MessageBox.Show($"Пароль должен состоять из 5х и более символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!isInt)
                MessageBox.Show($"В поле Возраст введены неккоректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!isInt2)
                MessageBox.Show($"В поле Опыт работы введены неккоректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            else

            {
                using (medcentrDB db = new medcentrDB())
                {
                    var doc = new Doctors()
                    {
                        Firstname = name.Text,
                        Lastname = surname.Text,
                        Patronymic = patronimyc.Text,
                        Age = Convert.ToInt32(age.Text),
                        Salary = Convert.ToDecimal(salary.Text),
                        Experience = Convert.ToInt32(expirience.Text),
                        Education = education.Text,
                        Position = position.Text
                    };
                    var pass = new Passwords()
                    {
                        Admin_or_doctor = 1,      // Если пользователь администратор , то это поле = 0 , если врач , то 1
                        Password = password.Text,
                        Id_doc= doc.Id
                    };
                    var log = new Logins()
                    {
                        Login = login.Text,
                        PassId = pass.Id
                    };

                    db.Doctors.Add(doc);
                    db.Passwords.Add(pass);
                    db.Logins.Add(log);
                    db.SaveChanges();

                    if (MessageBox.Show($"Врач {name.Text} успешно добавлен в систему", "", MessageBoxButton.OK, MessageBoxImage.None) == MessageBoxResult.OK)
                    {
                        if (App.Current.MainWindow != null)
                            App.Current.MainWindow.Close();
                        MainWindow main = new MainWindow();
                        main.Show();
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
