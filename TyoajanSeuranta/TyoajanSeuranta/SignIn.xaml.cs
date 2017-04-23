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
			if (string.IsNullOrEmpty(txt_PinCode.Text) == true) {
				txt_signinMessages.Text = "Pin koodi on pakollinen!";
			} else if (VerifyUserLogin(txt_PinCode.Text) == true) {
				this.Close();
			}
		}
		private bool VerifyUserLogin(string pincode)
		{
			string[] userName = new string[3];
			try {
				string connStr = Properties.Settings.Default.MySqlLoginString;
				string sql = string.Format("SELECT ID, FirstName, LastName FROM Employee WHERE Passwd LIKE {0}", pincode);
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand(sql, conn))
					using (MySqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							userName[0] = string.Format("{0}", reader.GetInt16(0));
							userName[1] = reader.GetString(1);
							userName[2] = reader.GetString(2);
						}
					}
				}
				if (string.IsNullOrWhiteSpace(userName[1]) == false && string.IsNullOrWhiteSpace(userName[2]) == false) {
					Properties.Settings.Default.LoggedInUser = string.Format("{0} {1}", userName[1], userName[2]);
					Properties.Settings.Default.UserID = userName[0];
					return true;
				} else {
					txt_signinMessages.Text = "Väärä pin-koodi!";
					Properties.Settings.Default.UserID = "";
					Properties.Settings.Default.LoggedInUser = "";
					return false;
				}
			}
			catch (Exception ex) {
				txt_signinMessages.Text = ex.Message;
				Properties.Settings.Default.UserID = "";
				return false;
			}
		}
	}
}
