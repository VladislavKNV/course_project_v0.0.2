using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using course_project_v0._0._2.View;
using System.Threading.Tasks;
using System.Windows;
using course_project_v0._0._2.DataBase;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace course_project_v0._0._2.View
{
	public partial class AdminAddFilm : Window
	{
		public AdminAddFilm()
		{
			InitializeComponent();
			InfoForUsers();
			InfoForFilms();
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

					allUsers.AddUser(i.userID, i.login, i.password,i.EmailBD, (bool)i.admin, i.basket);
					infoforusers.Add(allUsers);
				}
				usersGrid.ItemsSource = infoforusers;
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

					allFilms.AddFilmsForAdmin(i.filmID, i.filmName, (int)i.year, i.plotDescription, i.genres,(float)i.rating, i.countries, i.director, i.actors,(int)i.duration, i.poster, i.premiereDate);
					infoforfilms.Add(allFilms);
				}
				ListBoxFilms.ItemsSource = infoforfilms;
			}
		}

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
		public string FilmID;


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


			//--
			Random rnd = new Random();
			int value = rnd.Next(1000000, 9999999);
			FilmID = $"{value}";
			//--
		}
		private void FilmYearTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

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

		}
		private void FilmGenresTextBox_TextChanged(object sender, TextChangedEventArgs e)//+-
		{
			string pattern = @"\b\w{4,50}\b";

			if (Regex.IsMatch(TextBoxFilmGenres.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmGenres.BorderBrush = Brushes.LimeGreen;
				FilmGenresLabel.Content = null;
				genresbool = true;
			}
			else
			{
				TextBoxFilmGenres.BorderBrush = Brushes.DarkRed;
				genresbool = false;
			}
		}
		private void FilmRatingTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
		private void FilmCountriesTextBox_TextChanged(object sender, TextChangedEventArgs e)//+-
		{
			string pattern = @"\b\w{3,60}\b";

			if (Regex.IsMatch(TextBoxFilmCountries.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmCountries.BorderBrush = Brushes.LimeGreen;
				FilmCountriesLabel.Content = null;
				countriesbool = true;
			}
			else
			{
				TextBoxFilmCountries.BorderBrush = Brushes.DarkRed;
				countriesbool = false;
			}
		}
		private void FilmDirectorTextBox_TextChanged(object sender, TextChangedEventArgs e)//+-
		{
			string pattern = @"\b\w{2,60}\b";

			if (Regex.IsMatch(TextBoxFilmDirector.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmDirector.BorderBrush = Brushes.LimeGreen;
				FilmDirectorLabel.Content = null;
				directorbool = true;
			}
			else
			{
				TextBoxFilmDirector.BorderBrush = Brushes.DarkRed;
				directorbool = false;
			}

		}
		private void FilmActorsTextBox_TextChanged(object sender, TextChangedEventArgs e)//+-
		{

			string pattern = @"\b\w{2,500}\b";

			if (Regex.IsMatch(TextBoxFilmActors.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmActors.BorderBrush = Brushes.LimeGreen;
				FilmActorsLabel.Content = null;
				actorsbool = true;
			}
			else
			{
				TextBoxFilmActors.BorderBrush = Brushes.DarkRed;
				actorsbool = false;
			}
		}
		private void FilmDurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string pattern = @"[^a-z]";

			if (Regex.IsMatch(TextBoxFilmDuration.Text, pattern, RegexOptions.IgnoreCase))
			{
				TextBoxFilmDuration.BorderBrush = Brushes.LimeGreen;
				FilmDurationLabel.Content = null;
				actorsbool = true;
			}
			else
			{
				TextBoxFilmDuration.BorderBrush = Brushes.DarkRed;
				actorsbool = false;
			}
		}
		private void PremiereDateTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
		private void Button_Save_Click(object sender, RoutedEventArgs e)
		{

			using (course_work cw = new course_work())
			{
				byte[] imageBytes = File.ReadAllBytes(Picture);
				Film user = new Film()
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
				cw.Film.Add(user);
				cw.SaveChanges();
			}
			MessageBox.Show("Запись прошла успешно.");
		}
		public string Picture;
		public byte[] PictureForByte;
		public void LoadImg()
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

		private void Button_Pict(object sender, RoutedEventArgs e)
		{
			LoadImg();

		}
		private void Button_Click_Back(object sender, RoutedEventArgs e)
		{
			this.Close();
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();

		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ListBoxFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)//+++++++++++++++++++++-----------------------------
		{
			var aaa = ListBoxFilms.SelectedItem as AppView;
			if (aaa != null)
			{
				
			}
		}
	}


}
