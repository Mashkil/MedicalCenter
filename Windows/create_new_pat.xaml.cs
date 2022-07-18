﻿using System;
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
    public partial class create_new_pat : Window
    {
        bool button_no = false;
        public create_new_pat()
        {
            InitializeComponent();
        }

        private void create_pat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!decimal.TryParse(series_and_number.Text, out decimal h))
                    MessageBox.Show("Серия и номер паспорта могут содержать только числа", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                else if (series_and_number.Text.Length > 10)
                    MessageBox.Show("Серия и номер паспорта должно состоять из 10 символов", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                else
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
                            Chronic_deseases = chronic_deseases.Text
                        };

                        db.Patients.Add(pat);
                        db.SaveChanges();

                        if (MessageBox.Show($"Карта пациента {name.Text} успешно создана\nОткрыть окно записи?", "",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (App.Current.MainWindow != null)
                                App.Current.MainWindow.Close();
                            Windows.add_record add_Record = new add_record();
                            add_Record.Show();
                            this.Close();
                        }
                        else
                        {
                            button_no = true;
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
            if (!button_no)
            {
                admin admin = new admin();
                admin.Show();
            }
        }
    }
}
