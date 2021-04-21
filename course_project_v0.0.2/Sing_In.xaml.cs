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
using course_project_v0._0._2.Database;
using System.Data.SqlClient;

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
		public string login_Sing_in;
		public string password_Sing_in;
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
			//	using (course_workBD cw = new course_workBD())
				//{
					SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4I1BJOF;Initial Catalog=course_work;Integrated Security=True");
					conn.Open();
					SqlCommand command = new SqlCommand("SELECT * FROM User WHERE login = @TextBoxLogin.Text", conn);
					command.Parameters.AddWithValue("@TextBoxLogin", TextBoxLogin.Text);
					SqlDataReader reader = command.ExecuteReader();
					if (reader.HasRows == true)
					{
						MessageBox.Show("Corrected");
					}
					else
					{
						MessageBox.Show("Check user or password again!");
					}
					/*var forBD = cw.Database.SqlQuery<User>($"select * from User where User.login = '{TextBoxLogin.Text}'");
					foreach (var check in forBD)//
					{
						if (check.login == TextBoxLogin.Text )
						{
							MainWindow mainWindow = new MainWindow();
							mainWindow.Show();
						}
					}*/
					MessageBox.Show("EEEEEEE");

				//}

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
				LoginLabel.Content = null;
				login_Sing_in = TextBoxLogin.Text.Trim();
			}
			else
			{
				TextBoxLogin.BorderBrush = Brushes.DarkRed;
				loginbool = false;
			}
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			string pattern = @"\b\w{4,20}\b";
			if (Regex.IsMatch(PasswordBox.Password, pattern, RegexOptions.IgnoreCase))
			{
				PasswordBox.BorderBrush = Brushes.LimeGreen;
				PassLabel.Content = null;
				passbool = true;
			}
			else
			{
				PasswordBox.BorderBrush = Brushes.DarkRed;
				passbool = false;
			}

		}
	}
}
