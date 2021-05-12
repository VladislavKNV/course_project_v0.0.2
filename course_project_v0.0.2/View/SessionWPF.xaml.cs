﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using course_project_v0._0._2.DataBase;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;

namespace course_project_v0._0._2.View
{
	public partial class SessionWPF : Window
	{
		public string FilmID;
		public SessionWPF(string ID, bool admin, string login, DateTime _date)
		{
			InitializeComponent();
			FilmID = ID;
			ADMIN = admin;
			LOGIN = login;
			DateSession = _date;
			InfoForFilms();
			AddPost();
			InfoForListBox();
		}

		public DateTime DateSession;
		public string NameF;
		public int Year;
		public string Plot;
		public string Genres;
		public float Rating;
		public string Countries;
		public string Director;
		public string Actors;
		public int Duration;
		public string PremiereDate;
		public byte[] Poster;
		public bool ADMIN;
		public string LOGIN;
		private void Button_Click_Back(object sender, RoutedEventArgs e)
		{
			this.Close();
			MainWindow mainWindow = new MainWindow(ADMIN, LOGIN);
			mainWindow.Show();
		}

		public void InfoForFilms()
		{
			using (course_work cw = new course_work())
			{
				var forBD = cw.Database.SqlQuery<Film>($"select * from film where Film.filmID = '{FilmID}'");
				foreach (var check in forBD)
				{
					NameF = check.filmName;
					Year = (int)check.year;
					Plot = check.plotDescription;
					Genres = check.genres;
					Rating = (float)check.rating;
					Countries = check.countries;
					Director = check.director;
					Actors = check.actors;
					Duration = (int)check.duration;
					PremiereDate = check.premiereDate;
					Poster = check.poster;
				}
			}
			FilmName.Text = NameF;
			FilmYear.Text = $"{Year}";
			FilmPlot.Text = Plot;
			FilmGenres.Text = Genres;
			FilmRating.Text = $"{Rating}";
			FilmCountries.Text = Countries;
			FilmDirector.Text = Director;
			FilmActros.Text = Actors;
			FilmDuration.Text = $"{Duration} минут";
			FilmPremiereDate.Text = PremiereDate;
		}
		private ObservableCollection<AppView> post;
		public void AddPost()
		{
			post = new ObservableCollection<AppView>();
			AppView posterfilm = new AppView();
			posterfilm.AddPoster(Poster);
			post.Add(posterfilm);
			ListBoxPoster.ItemsSource = post;
		}
		private ObservableCollection<AppViewSession> infoforsession;
		public void InfoForListBox()
		{
			using (course_work cw = new course_work())
			{
				var info = cw.Session.ToList();
				infoforsession = new ObservableCollection<AppViewSession>();
				foreach (var i in info)
				{
					if (i.date == DateSession)
					{
						AppViewSession allSession = new AppViewSession();

						allSession.AddSession(i.sessionID, i.filmID, i.date, i.time, i.hallID, i.number_of_free_seats, i.price_for_place);
						infoforsession.Add(allSession);
					}
				}
				ListBoxSession.ItemsSource = infoforsession;
			}
		}
		private void ListBoxSession_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var aaa = ListBoxSession.SelectedItem as AppViewSession;
			if (aaa != null)
			{
				TicketWPF ticketWPF = new TicketWPF(LOGIN);
				ticketWPF.Show();
			}
		}

	}
}