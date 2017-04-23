using MySql.Data.MySqlClient;
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

namespace TyoajanSeuranta {
	/// <summary>
	/// Interaction logic for SignIn.xaml
	/// </summary>
	public partial class SignIn : Window {
		public SignIn()
		{
			InitializeComponent();
		}

		private void button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void button_Ok_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.UserPin = txt_PinCode.Text;
			if (string.IsNullOrEmpty(txt_PinCode.Text) == true) {
				txt_signinMessages.Text = "Pin koodi on pakollinen!";
			} else if (VerifyUserLogin() == true) {
				this.Close();
			}
		}
		private bool VerifyUserLogin()
		{
			string[] userName = new string[2];
			try {
				string connStr = Properties.Settings.Default.MySqlLoginString;
				string sql = string.Format("SELECT FirstName, LastName FROM Employee WHERE Passwd LIKE {0}", Properties.Settings.Default.UserPin);
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand(sql, conn))
					using (MySqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							userName[0] = reader.GetString(0);
							userName[1] = reader.GetString(1);
						}
					}
				}
				Properties.Settings.Default.LoggedInUser = string.Format("{0} {1}", userName[0], userName[1]);
				return true;
			}
			catch (Exception) {
				txt_signinMessages.Text = "Väärä pin-koodi!";
				Properties.Settings.Default.UserPin = "";
				return false;
			}
		}
	}
}
