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
	public partial class Sing_Up : Window
	{
		public Sing_Up()
		{
			InitializeComponent();
		}

		private void Button_Back_Click(object sender, RoutedEventArgs e)
		{
			Close();
			Sing_In sing_In = new Sing_In();
			sing_In.Show();
			
		}

		private void Button_Reg_Click(object sender, RoutedEventArgs e)
		{
			if (emailbool == false)
			{
				EmailLabel.Content = "Неверно введеён E-mail.";
			}
			if (loginbool == false)
			{
				LoginLabel.Content = "Логин должен содержать от 4 до 20 символов.";
			}
			if (pass1bool == false)
			{
				Pass1Label.Content = "Пароль должен содержать от 4 до 20 символов и совпадать с другим паролем.";
			}
			if (pass2bool == false)
			{
				Pass2Label.Content = "Пароль должен содержать от 4 до 20 символов и совпадать с другим паролем.";
			}

		}

		public bool emailbool = false;
		public bool loginbool = false;
		public bool pass1bool = false;
		public bool pass2bool = false;

		private void pass1_PasswordChanged(object sender, RoutedEventArgs e)
		{
			string pattern = @"\b\w{4,20}\b";
			if (Regex.IsMatch(Pass1.Password, pattern, RegexOptions.IgnoreCase))
			{
				if (Pass1.Password.Equals(Pass2.Password))
				{
					Pass1.BorderBrush = Brushes.LimeGreen;
					Pass2.BorderBrush = Brushes.LimeGreen;
					Pass1Label.Visibility = Visibility.Hidden;
					Pass2Label.Visibility = Visibility.Hidden;
					pass1bool = true;
				}
				else
				{
					Pass1.BorderBrush = Brushes.DarkRed;
					Pass2.BorderBrush = Brushes.DarkRed;
				}
			}
			else
			{
				Pass1.BorderBrush = Brushes.DarkRed;
			}
		}

		private void pass2_PasswordChanged(object sender, RoutedEventArgs e)
		{
			string pattern = @"\b\w{4,20}\b";
			if (Regex.IsMatch(Pass2.Password, pattern, RegexOptions.IgnoreCase))
			{
				if (Pass2.Password.Equals(Pass1.Password))
				{
					Pass1.BorderBrush = Brushes.LimeGreen;
					Pass2.BorderBrush = Brushes.LimeGreen;
					Pass2Label.Visibility = Visibility.Hidden;
					Pass1Label.Visibility = Visibility.Hidden;
					pass2bool = true;
				}
				else
				{
					Pass1.BorderBrush = Brushes.DarkRed;
					Pass2.BorderBrush = Brushes.DarkRed;
				}
			}
			else
			{
				Pass2.BorderBrush = Brushes.DarkRed;
			}

		}
		private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

			if (Regex.IsMatch(EmailTextBox.Text, pattern, RegexOptions.IgnoreCase))
			{

				EmailTextBox.BorderBrush = Brushes.LimeGreen;
				EmailLabel.Visibility = Visibility.Hidden;
				emailbool = true;
			}
			else
			{
				EmailTextBox.BorderBrush = Brushes.DarkRed;
			}
			
		}

		private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"\b\w{4,20}\b";

			if (Regex.IsMatch(LoginTextBox.Text, pattern, RegexOptions.IgnoreCase))
			{
				LoginTextBox.BorderBrush = Brushes.LimeGreen;
				LoginLabel.Visibility = Visibility.Hidden;
				loginbool = true;
			}
			else
			{
				LoginTextBox.BorderBrush = Brushes.DarkRed;
			}
		}
	}
}
