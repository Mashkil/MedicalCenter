﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace MedicalCenter.Windows
{

    public partial class registration_admin : Window
    {
        public registration_admin()
        {
            InitializeComponent();
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            using (medcentrDB db = new medcentrDB())
            {
                string log_str = login.Text;
                var log = await db.Logins_and_passwords.FirstOrDefaultAsync(p => p.Login == log_str); // проверка на сущевствование такого же логина
                
                if (log != null)
                {
                    MessageBox.Show($"Пользователь с логином ({login.Text}) уже сущевствует, выберите другой логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
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
                        var admins = new Admins()
                        {
                            Firstname = name.Text,
                            Lastname = surname.Text,
                            Patronimyc = patronimyc.Text,
                            Age = Convert.ToInt32(age.Text),
                            Salary = Convert.ToDecimal(salary.Text),
                            Education = education.Text
                        };
                        var log_and_pass = new Logins_and_passwords()
                        {
                            Login = login.Text,
                            Password = password.Text,
                            Admin_or_doctor = 0,      // Если пользователь администратор , то это поле = 0 , если врач , то 1
                            Id_admin = admins.Id
                        };

                        db.Admins.Add(admins);
                        db.Logins_and_passwords.Add(log_and_pass);
                        await db.SaveChangesAsync();

                        if (MessageBox.Show($"Администратор {name.Text} успешно добавлен в систему", "", MessageBoxButton.OK, MessageBoxImage.None) == MessageBoxResult.OK)
                        {
                            App.Current.MainWindow?.Close();

                            MainWindow main2 = new MainWindow();
                            main2.Show();
                            this.Close();
                        }

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
