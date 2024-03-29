﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Collections.ObjectModel;

namespace MedicalCenter.Windows
{
    public partial class about_doctors : Window
    {
        public about_doctors()
        {
            InitializeComponent();
            medcentrDB db = new medcentrDB();

            var query =
            from doc in db.Doctors            
            orderby doc.Lastname            
            select new { doc.Firstname, doc.Lastname, doc.Patronymic, doc.Age, doc.Salary, doc.Experience, 
                doc.Education, doc.Position };            
            Grid.ItemsSource = query.ToList();      
            
        }

        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)        
        {

            if (e.PropertyName.StartsWith("Firstname"))
                  e.Column.Header = "Имя";            
            if (e.PropertyName.StartsWith("Lastname"))
                e.Column.Header = "Фамилия";
            if (e.PropertyName.StartsWith("Patronymic"))
                e.Column.Header = "Отчество";
            if (e.PropertyName.StartsWith("Age"))
                e.Column.Header = "Возраст";
            if (e.PropertyName.StartsWith("Salary"))
                e.Column.Header = "Заработная плата";
            if (e.PropertyName.StartsWith("Experience"))
                e.Column.Header = "Опыт работы";
            if (e.PropertyName.StartsWith("Education"))
                e.Column.Header = "Образование";
            if (e.PropertyName.StartsWith("Position"))
                e.Column.Header = "Должность";
        }

    }
}
