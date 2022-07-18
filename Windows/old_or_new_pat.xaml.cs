using System;
using System.Windows;


namespace MedicalCenter.Windows
{

    public partial class old_or_new_pat : Window
    {
        public old_or_new_pat()
        {
            InitializeComponent();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            create_new_pat create_New_Pat = new create_new_pat();
            create_New_Pat.ShowDialog();
            this.Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            add_record add_Record = new add_record();
            add_Record.ShowDialog();
            this.Close();
        }
    }
}
