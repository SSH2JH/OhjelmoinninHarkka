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
			} else {
				Properties.Settings.Default.UserPin = txt_PinCode.Text;
				txt_signinMessages.Text = "Tervetuloa!";
				this.Close();
			}
		}
	}
}
