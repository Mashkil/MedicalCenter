using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace MedicalCenter.Windows
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        Dictionary<string, int> list_of_services = new Dictionary<string, int>();

        private void save_Click(object sender, RoutedEventArgs e) //сохранение данных в бд
        {
            try
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

                        var log_and_pass = new Logins_and_passwords()
                        {
                            Login = login.Text,
                            Password = password.Text,
                            Admin_or_doctor = 1,      // Если пользователь администратор , то это поле = 0 , если врач , то 1
                            Id_doc = doc.Id
                        };                        

                        foreach (var ss in list_of_services)
                        {
                            var services = new Services();
                            services.Name_of_service = ss.Key;
                            services.Cost = ss.Value;
                            services.Id_doc = doc.Id;
                            db.Services.Add(services);
                        }
                        
                        db.Doctors.Add(doc);
                        db.Logins_and_passwords.Add(log_and_pass);
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
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void del_click(object sender, RoutedEventArgs e) // подменю удаления 
        {
            var ci = new DataGridCellInfo(services.Items[services.SelectedIndex], services.Columns[0]);
            var name = ci.Column.GetCellContent(ci.Item) as TextBlock;
            list_of_services.Remove(name.Text);
            services.ItemsSource = null;
            services.ItemsSource = list_of_services;
        }

        private void edit_click(object sender, RoutedEventArgs e) // подменю редактирования
        {
            var ci = new DataGridCellInfo(services.Items[services.SelectedIndex], services.Columns[0]);
            var name = ci.Column.GetCellContent(ci.Item) as TextBlock;
            var ci1 = new DataGridCellInfo(services.Items[services.SelectedIndex], services.Columns[1]);
            var cost = ci1.Column.GetCellContent(ci1.Item) as TextBlock;
            list_of_services.Remove(name.Text);
            service_name.Text = name.Text;
            service_cost.Text = cost.Text;
        }

        private void add_service_Click(object sender, RoutedEventArgs e) //добавление записей в datagrid
        {
            try
            {
                if (int.TryParse(service_cost.Text, out int res))
                {
                    list_of_services.Add(service_name.Text, Convert.ToInt32(service_cost.Text));
                    services.ItemsSource = null;
                    services.ItemsSource = list_of_services;
                    service_name.Text = "";
                    service_cost.Text = "";
                }

                else
                    MessageBox.Show("Поле Стоимость услуги может содержать только цифры");
            }

            catch (ArgumentException)
            {
                MessageBox.Show("Такая услуга уже существует");
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void services_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.StartsWith("Key"))
                e.Column.Header = "Название услуги";
            if (e.PropertyName.StartsWith("Value"))
                e.Column.Header = "Стоимость (руб)";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.Current.MainWindow != null)
                App.Current.MainWindow.Close();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void position_LostFocus(object sender, RoutedEventArgs e)
        {
            using (medcentrDB db = new medcentrDB())
            {
                var serv = from s in db.Services
                           select new { s.Name_of_service};
                foreach (var service in serv)                
                    service_name.Items.Add(service.Name_of_service);
                
            }
        }
    }
}
