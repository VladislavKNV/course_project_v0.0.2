using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using course_project_v0._0._2.DataBase;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace course_project_v0._0._2.View
{
	public partial class AdminAddFilm : Window
	{
		public AdminAddFilm(bool admi)
		{
			InitializeComponent();
			randFilmID();
			InfoForUsers();
			InfoForFilms();
			InfoForComboBoxFilms();
			ADMIN = admi;
		}

		public bool ADMIN;
		public bool namebool = false;
		public bool yearbool = false;
		public bool plotbool = false;
		public bool genresbool = false;
		public bool ratingbool = false;
		public bool countriesbool = false;
		public bool directorbool = false;
		public bool actorsbool = false;
		public bool durationbool = false;
		public bool premieredatebool = false;
		public bool namebool2 = false;
		public bool yearbool2 = false;
		public bool plotbool2 = false;
		public bool genresbool2 = false;
		public bool ratingbool2 = false;
		public bool countriesbool2 = false;
		public bool directorbool2 = false;
		public bool actorsbool2 = false;
		public bool durationbool2 = false;
		public bool premieredatebool2 = false;
		public bool emailbool = false;
		public bool loginbool = false;
		public bool passbool = false;
		public string FilmID;
		public string Picture;
		public byte[] Pic;
		public byte[] PictureForByte;

		public void InfoForComboBoxFilms()
		{
			using (course_work cw = new course_work())
			{

				var info = cw.Film.ToList();

				foreach (var i in info)
				{
					ComboBoxFilms.Items.Add(i.filmName);
				}
			}
		}

		private ObservableCollection<AppViewUsers> infoforusers;

		public void InfoForUsers()
		{
			using (course_work cw = new course_work())
			{

				var info = cw.UsersBD.ToList();
				infoforusers = new ObservableCollection<AppViewUsers>();
				foreach (var i in info)
				{
					AppViewUsers allUsers = new AppViewUsers();

					allUsers.AddUser(i.userID, i.login, i.password, i.EmailBD, (bool)i.admin, i.basket);
					infoforusers.Add(allUsers);
				}
				ListBoxUsers.ItemsSource = infoforusers;
			}
		}


		private ObservableCollection<AppView> infoforfilms;

		public void InfoForFilms()
		{
			using (course_work cw = new course_work())
			{

				var info = cw.Film.ToList();
				infoforfilms = new ObservableCollection<AppView>();
				foreach (var i in info)
				{
					AppView allFilms = new AppView();

					allFilms.AddFilmsForAdmin(i.filmID, i.filmName, (int)i.year, i.plotDescription, i.genres, (float)i.rating, i.countries, i.director, i.actors, (int)i.duration, i.poster, i.premiereDate);
					infoforfilms.Add(allFilms);
				}
				ListBoxFilms.ItemsSource = infoforfilms;
			}
		}

		private void Button_Save_Click(object sender, RoutedEventArgs e)
		{
			if (namebool == true && yearbool == true && plotbool == true && genresbool == true && ratingbool == true && countriesbool == true && directorbool == true && durationbool == true && actorsbool == true && premieredatebool == true)
			{
				using (course_work cw = new course_work())
				{
					byte[] imageBytes = File.ReadAllBytes(Picture);
					Film film = new Film()
					{
						filmID = FilmID,
						filmName = TextBoxFilmName.Text.Trim(),
						year = Convert.ToInt32(TextBoxFilmYear.Text.Trim()),
						plotDescription = TextBoxFilmPlot.Text.Trim(),
						genres = TextBoxFilmGenres.Text.Trim(),
						rating = float.Parse(TextBoxFilmRating.Text.Trim()),
						countries = TextBoxFilmCountries.Text.Trim(),
						director = TextBoxFilmDirector.Text.Trim(),
						actors = TextBoxFilmActors.Text.Trim(),
						duration = Convert.ToInt32(TextBoxFilmDuration.Text.Trim()),
						premiereDate = TextBoxFilmPremiereDate.Text.Trim(),
						poster = PictureForByte
					};
					cw.Film.Add(film);
					cw.SaveChanges();
				}
				MessageBox.Show("Запись прошла успешно.");
			}
			else
			{
				if (namebool == false)
				{
					FilmNameLabel.Content = "Неверно введено название.";
				}
				if (yearbool == false)
				{
					FilmYearLabel.Content = "Неверно введён год.";
				}
				if (plotbool == false)
				{
					FilmPlotLabel.Content = "Неверно введено описание.";
				}
				if (genresbool == false)
				{
					FilmGenresLabel.Content = "Неверно введены жанры.";
				}
				if (ratingbool == false)
				{
					FilmRatingLabel.Content = "Неверно введён рейтинг.";
				}
				if (countriesbool == false)
				{
					FilmCountriesLabel.Content = "Неверно введены страны.";
				}
				if (directorbool == false)
				{
					FilmDirectorLabel.Content = "Неверно введён режисёр.";
				}
				if (durationbool == false)
				{
					FilmDurationLabel.Content = "Неверно введена продолжительность.";
				}
				if (actorsbool == false)
				{
					FilmActorsLabel.Content = "Неверно введены актёры.";
				}
				if (premieredatebool == false)
				{
					FilmPremiereDateLabel.Content = "Неверно введена дата выхода.";
				}
			}
		}
		private void Button_Click_Back(object sender, RoutedEventArgs e)
		{
			this.Close();
			MainWindow mainWindow = new MainWindow(ADMIN);
			mainWindow.Show();

		}
		private void Button_Pict(object sender, RoutedEventArgs e)
		{
			LoadImg();

		}
		private void Button_Pict2(object sender, RoutedEventArgs e)
		{
			try
			{
				OpenFileDialog dialog = new OpenFileDialog()
				{
					CheckFileExists = false,
					CheckPathExists = true,
					Multiselect = false,
					Title = "Выберите файл"
				};

				if (dialog.ShowDialog() == true)
				{
					Picture = dialog.FileName;
				}

				string path = Picture;
				byte[] imageBytes = File.ReadAllBytes(path);
				Pic = imageBytes;
			}
			catch (Exception)
			{

			}

		}
		private void Button2_Save_Click(object sender, RoutedEventArgs e)//+
		{
			var contentListBox = ListBoxFilms.SelectedItem as AppView;
			if (contentListBox != null)
			{
				if (namebool2 == true && yearbool2 == true && plotbool2 == true && genresbool2 == true && ratingbool2 == true && countriesbool2 == true && directorbool2 == true && durationbool2 == true && actorsbool2 == true && premieredatebool2 == true)
				{


					course_work context = new course_work();
					var customer = context.Film
						.Where(c => c.filmName == contentListBox.filmname)
						.FirstOrDefault();
					// Внести изменения
					customer.filmName = TextBoxFilmName2.Text.Trim();
					customer.year = Convert.ToInt32(TextBoxFilmYear2.Text.Trim());
					customer.plotDescription = TextBoxFilmPlot2.Text.Trim();
					customer.genres = TextBoxFilmGenres2.Text.Trim();
					customer.rating = float.Parse(TextBoxFilmRating2.Text.Trim());
					customer.countries = TextBoxFilmCountries2.Text.Trim();
					customer.director = TextBoxFilmDirector2.Text.Trim();
					customer.actors = TextBoxFilmActors2.Text.Trim();
					customer.duration = Convert.ToInt32(TextBoxFilmDuration2.Text.Trim());
					customer.premiereDate = TextBoxFilmPremiereDate2.Text.Trim();
					customer.poster = Pic;

					// Сохранить изменения
					context.SaveChanges();
				}
				else
				{
					if (namebool2 == false)
					{
						FilmNameLabel2.Content = "Неверно введено название.";
					}
					if (yearbool2 == false)
					{
						FilmYearLabel2.Content = "Неверно введён год.";
					}
					if (plotbool2 == false)
					{
						FilmPlotLabel2.Content = "Неверно введено описание.";
					}
					if (genresbool2 == false)
					{
						FilmGenresLabel2.Content = "Неверно введены жанры.";
					}
					if (ratingbool2 == false)
					{
						FilmRatingLabel2.Content = "Неверно введён рейтинг.";
					}
					if (countriesbool2 == false)
					{
						FilmCountriesLabel2.Content = "Неверно введены страны.";
					}
					if (directorbool2 == false)
					{
						FilmDirectorLabel2.Content = "Неверно введён режисёр.";
					}
					if (durationbool2 == false)
					{
						FilmDurationLabel2.Content = "Неверно введена продолжительность.";
					}
					if (actorsbool2 == false)
					{
						FilmActorsLabel2.Content = "Неверно введены актёры.";
					}
					if (premieredatebool2 == false)
					{
						FilmPremiereDateLabel2.Content = "Неверно введена дата выхода.";
					}
				}
			}
			InfoForFilms();

		}
		private void Button_SaveUsers_Click(object sender, RoutedEventArgs e)//+
		{
			var contentListBox = ListBoxUsers.SelectedItem as AppViewUsers;
			if (contentListBox != null)
			{
				if (emailbool == true && loginbool == true && passbool == true)
				{


					course_work context = new course_work();
					var customer = context.UsersBD
						.Where(c => c.login == contentListBox.login)
						.FirstOrDefault();
					// Внести изменения
					customer.login = TextBoxLogin.Text.Trim();
					customer.password = TextBoxPassword.Text.Trim();
					customer.EmailBD = TextBoxEmail.Text.Trim();
					if (ComboBoxAdmin.SelectedItem == AdminComboBox)
					{
						customer.admin = true;
					}
					else
						customer.admin = false;

					// Сохранить изменения
					context.SaveChanges();
				}
				else
				{
					if (emailbool == false)
					{
						EmailLabel.Content = "Неверно введеён E-mail.";
					}
					if (loginbool == false)
					{
						LoginLabel.Content = "Логин должен содержать от 4 до 30 символов.";
					}
					if (passbool == false)
					{
						PasswordLabel.Content = "Пароль должен содержать от 4 до 30 символов.";
					}
					
				}
			}
			InfoForUsers();

		}
		private void Button_Del_Click(object sender, RoutedEventArgs e)
		{
			var contentListBox = ListBoxFilms.SelectedItem as AppView;
			if (contentListBox != null)
			{
				
				course_work context = new course_work();
				Film customer = context.Film
				 .Where(c => c.filmName == contentListBox.filmname)
				 .FirstOrDefault();

				context.Film.Remove(customer);
				context.SaveChanges();
			}
			InfoForFilms();
		}

		private void Button_DelUser_Click(object sender, RoutedEventArgs e)
		{
			var contentListBox = ListBoxUsers.SelectedItem as AppViewUsers;
			if (contentListBox != null)
			{

				course_work context = new course_work();
				UsersBD customer = context.UsersBD
				 .Where(c => c.login == contentListBox.login)
				 .FirstOrDefault();

				context.UsersBD.Remove(customer);
				context.SaveChanges();
			}
			InfoForUsers();
		}
		private void Button_SaveSession_Click(object sender, RoutedEventArgs e)
			{
			string tttime = TimePickerSession.Text;

			using (course_work cw = new course_work())
			{
				Session session = new Session()
				{
					//date = DataPickerSession.DisplayDate,
					//time = ptttime,
					//hall = ComboBoxHalls.Text
				};
				//cw.Session.Add(session);
				//cw.SaveChanges();
			}
			MessageBox.Show("Запись прошла успешно.");
		}
		private void ListBoxFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var contentListBox = ListBoxFilms.SelectedItem as AppView;
			if (contentListBox != null)
			{
				TextBoxFilmName2.Text = contentListBox.filmname;
				TextBoxFilmYear2.Text = contentListBox.year;
				TextBoxFilmPlot2.Text = contentListBox.plotDescription;
				TextBoxFilmGenres2.Text = contentListBox.genres;
				TextBoxFilmRating2.Text = contentListBox.rating;
				TextBoxFilmDirector2.Text = contentListBox.director;
				TextBoxFilmActors2.Text = contentListBox.actors;
				TextBoxFilmDuration2.Text = contentListBox.duration;
				TextBoxFilmPremiereDate2.Text = contentListBox.premiereDate;
				TextBoxFilmCountries2.Text = contentListBox.countries;
				Pic = PictureForByte;
			}
		}

		private void ListBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var contentListBox = ListBoxUsers.SelectedItem as AppViewUsers;
			if (contentListBox != null)
			{
				TextBoxLogin.Text = contentListBox.login;
				TextBoxPassword.Text = contentListBox.password;
				TextBoxEmail.Text = contentListBox.Email;
				TextBoxBasket.Text = contentListBox.basket;
				if (contentListBox.admin == true)
				{
					ComboBoxAdmin.SelectedItem = AdminComboBox;
				}
				else
				{
					ComboBoxAdmin.SelectedItem = UserComboBox;
				}
			}
		}

		public void LoadImg()
		{
			try
			{
				OpenFileDialog dialog = new OpenFileDialog()
				{
					CheckFileExists = false,
					CheckPathExists = true,
					Multiselect = false,
					Title = "Выберите файл"
				};

				if (dialog.ShowDialog() == true)
				{
					Picture = dialog.FileName;
				}

				string path = Picture;
				byte[] imageBytes = File.ReadAllBytes(path);
				PictureForByte = imageBytes;
			}
			catch(Exception)
			{

			}
			
		}

		public void randFilmID()
		{
			Random rnd = new Random();
			int value = rnd.Next(1000000, 9999999);
			FilmID = $"{value}";
		}


		private void FilmNameTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{2,50}\b";

			if (Regex.IsMatch(TextBoxFilmName.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmName.BorderBrush = Brushes.LimeGreen;
				FilmNameLabel.Content = null;
				namebool = true;
			}
			else
			{
				TextBoxFilmName.BorderBrush = Brushes.DarkRed;
				namebool = false;
			}

			if (Regex.IsMatch(TextBoxFilmName2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmName2.BorderBrush = Brushes.LimeGreen;
				FilmNameLabel2.Content = null;
				namebool2 = true;
			}
			else
			{
				TextBoxFilmName2.BorderBrush = Brushes.DarkRed;
				namebool2 = false;
			}
		}
		private void FilmYearTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{4,4}\b";
			if (Regex.IsMatch(TextBoxFilmYear.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmYear.BorderBrush = Brushes.LimeGreen;
				FilmYearLabel.Content = null;
				yearbool = true;
				try
				{
					int year = Convert.ToInt32(TextBoxFilmYear.Text.Trim());
					DateTime date1 = new DateTime(year, 1, 1, 0, 0, 0); // год - месяц - день - час - минута - секунда
					DateTime date2 = new DateTime(1896, 1, 1, 0, 0, 0);
					if (date1 < date2 || date1 > DateTime.Now)
					{
						TextBoxFilmYear.BorderBrush = Brushes.DarkRed;
						yearbool = false;
					}
				}
				catch(Exception)
				{
					TextBoxFilmYear.BorderBrush = Brushes.DarkRed;
					yearbool = false;
				}
			}
			else
			{
				TextBoxFilmYear.BorderBrush = Brushes.DarkRed;
				yearbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmYear2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmYear2.BorderBrush = Brushes.LimeGreen;
				FilmYearLabel2.Content = null;
				yearbool2 = true;
				try
				{
					int year = Convert.ToInt32(TextBoxFilmYear2.Text.Trim());
					DateTime date1 = new DateTime(year, 1, 1, 0, 0, 0); // год - месяц - день - час - минута - секунда
					DateTime date2 = new DateTime(1896, 1, 1, 0, 0, 0);
					if (date1 < date2 || date1 > DateTime.Now)
					{
						TextBoxFilmYear2.BorderBrush = Brushes.DarkRed;
						yearbool2 = false;
					}
				}
				catch (Exception)
				{
					TextBoxFilmYear2.BorderBrush = Brushes.DarkRed;
					yearbool2 = false;
				}
			}
			else
			{
				TextBoxFilmYear2.BorderBrush = Brushes.DarkRed;
				yearbool2 = false;
			}
		}
		private void FilmPlotTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{4,3000}\b";

			if (Regex.IsMatch(TextBoxFilmPlot.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmPlot.BorderBrush = Brushes.LimeGreen;
				FilmPlotLabel.Content = null;
				plotbool = true;
			}
			else
			{
				TextBoxFilmPlot.BorderBrush = Brushes.DarkRed;
				plotbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmPlot2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmPlot2.BorderBrush = Brushes.LimeGreen;
				FilmPlotLabel2.Content = null;
				plotbool2 = true;
			}
			else
			{
				TextBoxFilmPlot2.BorderBrush = Brushes.DarkRed;
				plotbool2 = false;
			}

		}
		private void FilmGenresTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{4,50}\b";

			if (Regex.IsMatch(TextBoxFilmGenres.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmGenres.BorderBrush = Brushes.LimeGreen;
				FilmGenresLabel.Content = null;
				genresbool = true;
				for (int i = 0; i < TextBoxFilmGenres.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmGenres.Text[i]))
					{
						TextBoxFilmGenres.BorderBrush = Brushes.DarkRed;
						genresbool = false;
					}
				}
			}
			else
			{
				TextBoxFilmGenres.BorderBrush = Brushes.DarkRed;
				genresbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmGenres2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmGenres2.BorderBrush = Brushes.LimeGreen;
				FilmGenresLabel2.Content = null;
				genresbool2 = true;
				for (int i = 0; i < TextBoxFilmGenres2.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmGenres2.Text[i]))
					{
						TextBoxFilmGenres2.BorderBrush = Brushes.DarkRed;
						genresbool2 = false;
					}
				}
			}
			else
			{
				TextBoxFilmGenres2.BorderBrush = Brushes.DarkRed;
				genresbool2 = false;
			}
		}
		private void FilmRatingTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
			string pattern = @"[0-9]+\,[0-9]+";

			if (Regex.IsMatch(TextBoxFilmRating.Text, pattern, RegexOptions.IgnoreCase))
			{
				float rating = float.Parse(TextBoxFilmRating.Text.Trim());
				if (rating <= 10.0 && rating >= 0.1) 
				{
					TextBoxFilmRating.BorderBrush = Brushes.LimeGreen;
					FilmRatingLabel.Content = null;
					ratingbool = true;
				}
				else
				{
					TextBoxFilmRating.BorderBrush = Brushes.DarkRed;
					ratingbool = false;
				}
			}
			else
			{
				TextBoxFilmRating.BorderBrush = Brushes.DarkRed;
				ratingbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmRating2.Text, pattern, RegexOptions.IgnoreCase))
			{
				float rating = float.Parse(TextBoxFilmRating2.Text.Trim());
				if (rating <= 10.0 && rating >= 0.1)
				{
					TextBoxFilmRating2.BorderBrush = Brushes.LimeGreen;
					FilmRatingLabel2.Content = null;
					ratingbool2 = true;
				}
				else
				{
					TextBoxFilmRating2.BorderBrush = Brushes.DarkRed;
					ratingbool2 = false;
				}
			}
			else
			{
				TextBoxFilmRating2.BorderBrush = Brushes.DarkRed;
				ratingbool2 = false;
			}
		}
		private void FilmCountriesTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{3,60}\b";

			if (Regex.IsMatch(TextBoxFilmCountries.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmCountries.BorderBrush = Brushes.LimeGreen;
				FilmCountriesLabel.Content = null;
				countriesbool = true;
				for (int i = 0; i < TextBoxFilmCountries.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmCountries.Text[i]))
					{
						TextBoxFilmCountries.BorderBrush = Brushes.DarkRed;
						countriesbool = false;
					}
				}
			}
			else
			{
				TextBoxFilmCountries.BorderBrush = Brushes.DarkRed;
				countriesbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmCountries2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmCountries2.BorderBrush = Brushes.LimeGreen;
				FilmCountriesLabel2.Content = null;
				countriesbool2 = true;
				for (int i = 0; i < TextBoxFilmCountries2.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmCountries2.Text[i]))
					{
						TextBoxFilmCountries2.BorderBrush = Brushes.DarkRed;
						countriesbool2 = false;
					}
				}
			}
			else
			{
				TextBoxFilmCountries2.BorderBrush = Brushes.DarkRed;
				countriesbool2 = false;
			}
		}
		private void FilmDirectorTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{2,60}\b";

			if (Regex.IsMatch(TextBoxFilmDirector.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmDirector.BorderBrush = Brushes.LimeGreen;
				FilmDirectorLabel.Content = null;
				directorbool = true;
				for (int i = 0; i < TextBoxFilmDirector.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmDirector.Text[i]))
					{
						TextBoxFilmDirector.BorderBrush = Brushes.DarkRed;
						directorbool = false;
					}
				}
			}
			else
			{
				TextBoxFilmDirector.BorderBrush = Brushes.DarkRed;
				directorbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmDirector2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmDirector2.BorderBrush = Brushes.LimeGreen;
				FilmDirectorLabel2.Content = null;
				directorbool2 = true;
				for (int i = 0; i < TextBoxFilmDirector2.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmDirector2.Text[i]))
					{
						TextBoxFilmDirector2.BorderBrush = Brushes.DarkRed;
						directorbool2 = false;
					}
				}
			}
			else
			{
				TextBoxFilmDirector2.BorderBrush = Brushes.DarkRed;
				directorbool2 = false;
			}

		}
		private void FilmActorsTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{

			string pattern = @"\b\w{2,500}\b";

			if (Regex.IsMatch(TextBoxFilmActors.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmActors.BorderBrush = Brushes.LimeGreen;
				FilmActorsLabel.Content = null;
				actorsbool = true;
				for (int i = 0; i < TextBoxFilmActors.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmActors.Text[i]))
					{
						TextBoxFilmActors.BorderBrush = Brushes.DarkRed;
						actorsbool = false;
					}
				}
			}
			else
			{
				TextBoxFilmActors.BorderBrush = Brushes.DarkRed;
				actorsbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmActors2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmActors2.BorderBrush = Brushes.LimeGreen;
				FilmActorsLabel2.Content = null;
				actorsbool2 = true;
				for (int i = 0; i < TextBoxFilmActors2.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmActors2.Text[i]))
					{
						TextBoxFilmActors2.BorderBrush = Brushes.DarkRed;
						actorsbool2 = false;
					}
				}
			}
			else
			{
				TextBoxFilmActors2.BorderBrush = Brushes.DarkRed;
				actorsbool2 = false;
			}
		}
		private void FilmDurationTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{2,3}\b";

			if (Regex.IsMatch(TextBoxFilmDuration.Text, pattern, RegexOptions.IgnoreCase))
			{
				for (int i = 0; i < TextBoxFilmDuration.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmDuration.Text[i]))
					{
						TextBoxFilmDuration.BorderBrush = Brushes.LimeGreen;
						FilmDurationLabel.Content = null;
						durationbool = true;
					}
					else
					{
						TextBoxFilmDuration.BorderBrush = Brushes.DarkRed;
						durationbool = false;
					}	
				}
			}
			else
			{
				TextBoxFilmDuration.BorderBrush = Brushes.DarkRed;
				durationbool = false;
			}

			if (Regex.IsMatch(TextBoxFilmDuration2.Text, pattern, RegexOptions.IgnoreCase))
			{
				for (int i = 0; i < TextBoxFilmDuration2.Text.Length; i++)
				{
					if (Char.IsDigit(TextBoxFilmDuration2.Text[i]))
					{
						TextBoxFilmDuration2.BorderBrush = Brushes.LimeGreen;
						FilmDurationLabel2.Content = null;
						durationbool2 = true;
					}
					else
					{
						TextBoxFilmDuration2.BorderBrush = Brushes.DarkRed;
						durationbool2 = false;
					}
				}
			}
			else
			{
				TextBoxFilmDuration2.BorderBrush = Brushes.DarkRed;
				durationbool2 = false;
			}
		}
		private void PremiereDateTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{2,15}\b";

			if (Regex.IsMatch(TextBoxFilmPremiereDate.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmPremiereDate.BorderBrush = Brushes.LimeGreen;
				FilmPremiereDateLabel.Content = null;
				premieredatebool = true;
			}
			else
			{
				TextBoxFilmName.BorderBrush = Brushes.DarkRed;
				premieredatebool = false;
			}

			if (Regex.IsMatch(TextBoxFilmPremiereDate2.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmPremiereDate2.BorderBrush = Brushes.LimeGreen;
				FilmPremiereDateLabel2.Content = null;
				premieredatebool2 = true;
			}
			else
			{
				TextBoxFilmName2.BorderBrush = Brushes.DarkRed;
				premieredatebool2 = false;
			}
		}
		private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{4,30}\b";

			if (Regex.IsMatch(TextBoxLogin.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxLogin.BorderBrush = Brushes.LimeGreen;
				LoginLabel.Content = null;
				loginbool = true;
			}
			else
			{
				TextBoxLogin.BorderBrush = Brushes.DarkRed;
				loginbool = false;
			}
		}
		private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"\b\w{4,60}\b";
			if (Regex.IsMatch(TextBoxPassword.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxPassword.BorderBrush = Brushes.LimeGreen;
				PasswordLabel.Content = null;
				passbool = true;
			}
			else
			{
				TextBoxPassword.BorderBrush = Brushes.DarkRed;
				passbool = false;
			}
		}
		private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{
			string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
			string pattern2 = @"\b\w{4,60}\b";

			if (Regex.IsMatch(TextBoxEmail.Text.Trim(), pattern, RegexOptions.IgnoreCase))
			{
				if (Regex.IsMatch(TextBoxEmail.Text.Trim(), pattern2, RegexOptions.IgnoreCase))
				{
					TextBoxEmail.BorderBrush = Brushes.LimeGreen;
					EmailLabel.Content = null;
					emailbool = true;
				}
			}
			else
			{
				TextBoxEmail.BorderBrush = Brushes.DarkRed;
				emailbool = false;
			}
		}


		private void BasketTextBox_TextChanged(object sender, TextChangedEventArgs e)//+
		{

		}
		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void TextBoxNumbers_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			var regex = new Regex(@"[^0-9]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void TextBoxFilmLetters_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			var regex = new Regex(@"[0-9]|[@#$%!?=<>:;№.^&*+/-]");
			e.Handled = regex.IsMatch(e.Text);
		}

	}
}


