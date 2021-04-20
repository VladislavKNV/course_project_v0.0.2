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
using System.Text.RegularExpressions;

namespace course_project_v0._0._2
{
	public partial class Sing_In : Window
	{
		public Sing_In()
		{
			InitializeComponent();
		}

		private void Button_Click_Reg(object sender, RoutedEventArgs e)
		{
			Sing_Up sing_Up = new Sing_Up();
			sing_Up.Show();
		}

		private void Button_Click_Sing_In(object sender, RoutedEventArgs e)
		{
			//Hide();

			if (loginbool == false)
			{
				LoginLabel.Content = "Неверный логин.";
			}
			if (passbool == false)
			{
				PassLabel.Content = "Неверный пароль.";
			}
			if (loginbool == true && passbool == true)
			{
				MainWindow mainWindow = new MainWindow();
				mainWindow.Show();

			}

		}

		public bool loginbool = false;
		public bool passbool = false;
		private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"\b\w{4,20}\b";

			if (Regex.IsMatch(TextBoxLogin.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxLogin.BorderBrush = Brushes.LimeGreen;
				loginbool = true;
				LoginLabel.Visibility = Visibility.Hidden;
			}
			else
			{
				TextBoxLogin.BorderBrush = Brushes.DarkRed;
			}
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)//PasswordBox
		{
			string pattern = @"\b\w{4,20}\b";
			if (Regex.IsMatch(PasswordBox.Password, pattern, RegexOptions.IgnoreCase))
			{
				PasswordBox.BorderBrush = Brushes.LimeGreen;
				PassLabel.Visibility = Visibility.Hidden;
				passbool = true;
			}
			else
				PasswordBox.BorderBrush = Brushes.Red;

		}
	}
}
