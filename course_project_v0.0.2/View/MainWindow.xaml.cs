using course_project_v0._0._2.View;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using course_project_v0._0._2.DataBase;
using System.IO;

namespace course_project_v0._0._2
{
	public partial class MainWindow : Window
	{
		public MainWindow(bool admi, string login)
		{
            InitializeComponent();
            ADMIN = admi;
            LOGIN = login;
			if (ADMIN == false)
			{
                ButtonForAdmin.Visibility = Visibility.Hidden;
            }
            DateForPicker();
            InfoForListBox();
        }
        public bool ADMIN;
        public string LOGIN;

        public void DateForPicker()
		{
            Datepic.SelectedDate = DateTime.Now;
        }

        private ObservableCollection<AppView> infoforfilm;
   
        public void InfoForListBox()
        {
            using (course_work cw = new course_work())
            {
                int[] idFilms = new int[30];
                bool coincidence = true;
                var info = cw.Film.ToList();
                infoforfilm = new ObservableCollection<AppView>();
                var forBD = cw.Database.SqlQuery<Session>($"select * from Session");
                foreach (var check in forBD)
                {
					if (check.date == Datepic.SelectedDate.Value)
					{
                        foreach (var i in info)
                        {
                            for (int f = 0; f < idFilms.Length; f++)
                            {
								if (idFilms[f] == Convert.ToInt32(i.filmID))
								{
                                    coincidence = false;
								}
                               
                            }
                            if (i.filmID == check.filmID && coincidence == true)
                            {
                               

                                AppView allFilms = new AppView();
                                allFilms.Add(i.filmName, (int)i.year, i.genres, (float)i.rating, i.countries, i.director, (int)i.duration, i.poster, i.filmID.Trim());
                                infoforfilm.Add(allFilms);
                                for (int n = 0; n < idFilms.Length; n++)
                                {
									if (idFilms[n] != Convert.ToInt32(i.filmID))
									{
                                        idFilms[n] = Convert.ToInt32(i.filmID);
                                    }
                                }
                            }
                            coincidence = true;
                        }
                    }
                }
                ListBoxFilms.ItemsSource = infoforfilm;
            }
        }

		private void Button_Click_Admin(object sender, RoutedEventArgs e)
		{
            this.Close();
            AdminAddFilm adminAddFilm = new AdminAddFilm(ADMIN, LOGIN);
            adminAddFilm.Show();
        }
          
        private void Button_Click_Feedback(object sender, RoutedEventArgs e)
        {
            this.Close();
            FeedbackWPF feedbackWPF = new FeedbackWPF(ADMIN, LOGIN);
            feedbackWPF.Show();
        }

        private void ListBoxFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            var aaa = ListBoxFilms.SelectedItem as AppView;
            if (aaa != null)
            {
                SessionWPF sessionWPF = new SessionWPF(aaa.filmID.Trim(), ADMIN, LOGIN, Datepic.SelectedDate.Value);
                sessionWPF.Show();
               this.Close();
            }
        }

		private void Datepic_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
            InfoForListBox();

        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            Datepic.BlackoutDates.AddDatesInPast();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
           BasketWPF basketWPF = new BasketWPF(LOGIN, ADMIN);
           basketWPF.Show();
            this.Close();
		}
	}
}
