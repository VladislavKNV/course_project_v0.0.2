using System;
using course_project_v0._0._2.View;
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
using System.Security.Cryptography;
using course_project_v0._0._2.DataBase;

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
				LoginLabel.Content = "Логин должен содержать от 4 до 30 символов.";
			}
			if (pass1bool == false)
			{
				Pass1Label.Content = "Пароль должен содержать от 4 до 30 символов и совпадать с другим паролем.";
			}
			if (pass2bool == false)
			{
				Pass2Label.Content = "Пароль должен содержать от 4 до 30 символов и совпадать с другим паролем.";
			}

			using (course_work cw = new course_work())
			{

				var forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD where UsersBD.login = '{LoginTextBox.Text.Trim()}'");
				foreach (var check in forBD)
				{
					if (check.login != null)
					{
						LoginLabel.Content = "Такой логин уже зарегестрирован";
						loginbool = false;

					}
				}

				forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD where UsersBD.EmailBD = '{EmailTextBox.Text.Trim()}'");
				foreach (var usercheck in forBD)
				{
					if (usercheck.EmailBD != null)
					{
						EmailLabel.Content = "Такая почта уже зарегестрирована";
						emailbool = false;

					}
				}
			}

			if (emailbool == true && loginbool == true && pass1bool == true && pass2bool == true)
			{
				
				using (course_work cw = new course_work())
				{
					UsersBD user = new UsersBD()
					{
						userID = UserID,
						login = LoginTextBox.Text.Trim(),
						password = GetHashPassword(Pass1.Password.Trim()),
						EmailBD = EmailTextBox.Text.Trim(),
						admin = false,
					};
					cw.UsersBD.Add(user);
					cw.SaveChanges();
				}
				MessageBox.Show("Регистрация прошла успешно.");

				Close();
				Sing_In sing_In = new Sing_In();
				sing_In.Show();
			}

		}

		public bool emailbool = false;
		public bool loginbool = false;
		public bool pass1bool = false;
		public bool pass2bool = false;
		public string UserID;

		private void pass1_PasswordChanged(object sender, RoutedEventArgs e)
		{
			string pattern = @"\b\w{4,30}\b";
			if (Regex.IsMatch(Pass1.Password, pattern, RegexOptions.IgnoreCase))
			{
				if (Pass1.Password.Equals(Pass2.Password))
				{
					Pass1.BorderBrush = Brushes.LimeGreen;
					Pass2.BorderBrush = Brushes.LimeGreen;
					Pass1Label.Content = null;
					Pass2Label.Content = null;
					pass1bool = true;
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
				Pass1.BorderBrush = Brushes.DarkRed;
				pass1bool = false;
			}
		}

		private void pass2_PasswordChanged(object sender, RoutedEventArgs e)
		{
			string pattern = @"\b\w{4,30}\b";
			if (Regex.IsMatch(Pass2.Password, pattern, RegexOptions.IgnoreCase))
			{
				if (Pass2.Password.Equals(Pass1.Password))
				{
					Pass1.BorderBrush = Brushes.LimeGreen;
					Pass2.BorderBrush = Brushes.LimeGreen;
					Pass2Label.Content = null;
					Pass1Label.Content = null;
					pass2bool = true;
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
				Pass2.BorderBrush = Brushes.DarkRed;
				pass2bool = false;
			}

		}
		private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
			string pattern2 = @"\b\w{4,30}\b";

			if (Regex.IsMatch(EmailTextBox.Text, pattern, RegexOptions.IgnoreCase))
			{
				if (Regex.IsMatch(EmailTextBox.Text, pattern2, RegexOptions.IgnoreCase))
				{
					EmailTextBox.BorderBrush = Brushes.LimeGreen;
					EmailLabel.Content = null;
					emailbool = true;
				}				
			}
			else
			{
				EmailTextBox.BorderBrush = Brushes.DarkRed;
				emailbool = false;
			}
			
		}

		private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"\b\w{4,30}\b";

			if (Regex.IsMatch(LoginTextBox.Text, pattern, RegexOptions.IgnoreCase))
			{
				LoginTextBox.BorderBrush = Brushes.LimeGreen;
				LoginLabel.Content = null;
				loginbool = true;

				//--
				Random rnd = new Random();
				int value = rnd.Next(1000000, 9999999);
				UserID = $"{value}";
				//--
			}
			else
			{
				LoginTextBox.BorderBrush = Brushes.DarkRed;
				loginbool = false;
			}
		}
		
		private string GetHashPassword(string s)
		{
			//переводим строку в байт-массив  
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			//создаем объект для получения средст шифрования  
			MD5CryptoServiceProvider CSP =
				new MD5CryptoServiceProvider();
			//вычисляем хеш-представление в байтах  
			byte[] byteHash = CSP.ComputeHash(bytes);
			string hash = string.Empty;
			//формируем одну цельную строку из массива  
			foreach (byte b in byteHash)
			{
				hash += string.Format("{0:x2}", b);
			}
			return hash;
		}
	}
}
