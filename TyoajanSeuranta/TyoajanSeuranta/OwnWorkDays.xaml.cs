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
using System.Windows.Shapes;
using TyoajanSeuranta.Classes.ViewModels;

namespace TyoajanSeuranta {
	/// <summary>
	/// Interaction logic for OwnWorkDays.xaml
	/// </summary>
	public partial class OwnWorkDays : Window {
		public OwnWorkDays()
		{
			InitializeComponent();
			LoadDataGrid();
		}

		private void LoadDataGrid()
		{
			try {
				lb_Welcome.Content = Properties.Settings.Default.LoggedInUser;
				Mysql_UserLoadWorkdays svmo = new Mysql_UserLoadWorkdays();
				svmo.LoadFromMysql();
				dg_WorkDays.DataContext = svmo.UserWorkDays;
				int total = svmo.UserWorkDays.Sum(item => item.HoursTogether);
				txtb_AllHours.Text = string.Format("Yhteensä: {0} h", total);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
	}
}
