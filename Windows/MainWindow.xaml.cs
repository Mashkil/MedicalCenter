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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalCenter
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void autorize_Click(object sender, RoutedEventArgs e)
        {
            //Windows.change_patient change_Patient = new Windows.change_patient();
            //change_Patient.Show();
            //Hide();
            Windows.add_record type = new Windows.add_record();
            type.Show();
            Hide();
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            Windows.Type_of_user type_Of_User = new Windows.Type_of_user();
            type_Of_User.Show();
            this.Close();
        }
    }
}
