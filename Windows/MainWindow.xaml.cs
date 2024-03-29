﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalCenter
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void autorize_Click(object sender, RoutedEventArgs e)
        {        

            using (medcentrDB db = new medcentrDB())
            {
                string log = login.Text;
                string pas= password.Text;
                var autirization = await db.Logins_and_passwords.FirstOrDefaultAsync(p => p.Login == log && p.Password == pas);

                if (autirization != null)
                {
                    if (autirization.Id_admin != null)
                    {
                        Date2 date = db.Date.FirstOrDefault(p => p.Date1 == DateTime.Today);
                        
                        if (date == null)
                        {
                            var new_date = new Date2()
                            {
                                Date1 = DateTime.Today,
                                Type_of_day = DateTime.Today.DayOfWeek.ToString(),
                                Date_in_text = DateTime.Today.ToShortDateString(),
                                adminId = autirization.Id_admin
                            };
                            db.Date.Add(new_date);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            var dat = db.Date.FirstOrDefault(p => p.Date1 == DateTime.Today);
                            dat.adminId = autirization.Id_admin;
                            await db.SaveChangesAsync();
                        }

                        admin admin = new admin();
                        admin.Show();
                        this.Close();
                    }
                    else
                    {
                        Windows.Main_for_doctor main_For_Doctor = new Windows.Main_for_doctor((int)autirization.Id_doc);
                        main_For_Doctor.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }       
        

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            Windows.Type_of_user type_Of_User = new Windows.Type_of_user();
            type_Of_User.Show();
            this.Close();
        }
    }
}
