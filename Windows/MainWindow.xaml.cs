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
            //using (medcentrDB db = new medcentrDB())
            //{
            //    var autirization = db.Logins_and_passwords.FirstOrDefault(p => p.Login == login.Text && p.Password == password.Text);
            //    if (autirization != null)
            //    {
            //        if (autirization.Id_admin != null)
            //        {
            //            Date date = db.Date.FirstOrDefault(p => p.Date1 == DateTime.Today);
            //            if (date == null)
            //            {
            //                var new_date = new Date()
            //                {
            //                    Date1 = DateTime.Today,
            //                    Type_of_day = DateTime.Today.DayOfWeek.ToString(),
            //                    Date_in_text = DateTime.Today.ToShortDateString()
            //                };
            //            }
            //            else
            //            {
            //                var dat = db.Date.FirstOrDefault(p => p.Date1 == DateTime.Today);
            //                dat.adminId = autirization.Id_admin;
            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Вы ввели неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            admin adm = new admin();
            adm.Show();
            this.Hide();
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            Windows.Type_of_user type_Of_User = new Windows.Type_of_user();
            type_Of_User.Show();
            this.Close();
        }
    }
}
