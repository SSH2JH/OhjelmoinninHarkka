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
using TyoajanSeuranta.Classes.ViewModels;

namespace TyoajanSeuranta
{
    /// <summary>
    /// Interaction logic for SignToWork.xaml
    /// </summary>
    public partial class SignToWork : Window
    {
        public SignToWork()
        {
            InitializeComponent();    
        }



        private void btn_SignToWork_OK_Click(object sender, RoutedEventArgs e)
        {
            // This button allows employee to start their workday
            try
            {
                AddFirstEntry controls = new AddFirstEntry();
                // In the next method happens all the sql magic. You can find it under classes and viewmodel
                controls.AddNewEntry();
                // also disable the button for this session because why would you start workday two times per day
                ((MainWindow)Application.Current.MainWindow).txtb_IsOk1.Text = "OK!";
                ((MainWindow)Application.Current.MainWindow).btn_SignStart.IsEnabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_SignToWork_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).btn_SignStart.IsEnabled = true;
            this.Close();            
        }
    }
}

