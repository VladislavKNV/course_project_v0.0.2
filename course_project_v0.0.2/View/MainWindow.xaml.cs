using course_project_v0._0._2.View;
using System;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using course_project_v0._0._2.DataBase;
using System.IO;

namespace course_project_v0._0._2
{

	public partial class MainWindow : Window
	{
		public MainWindow(bool admi)
		{
            ADMIN = admi;
            InitializeComponent();
            InfoForListBox();
            ButtonForAdmin.Visibility = Visibility.Hidden;
            DataContext = new AppView();
			if (ADMIN == true)
			{
                ButtonForAdmin.Visibility = Visibility.Visible;
            }
           
        }
        public bool ADMIN;
     

        private ObservableCollection<AppView> infoforfilm;
   
        public void InfoForListBox()
        {
            using (course_work cw = new course_work())
            {

                var info = cw.Film.ToList();
                infoforfilm = new ObservableCollection<AppView>();
                foreach (var i in info)
                {
                    AppView allFilms = new AppView();

                    allFilms.Add(i.filmName, (int)i.year,i.genres, (float)i.rating,i.countries,i.director, (int)i.duration, i.poster,i.filmID.Trim());
                    infoforfilm.Add(allFilms);
                }
                ListBoxFilms.ItemsSource = infoforfilm;
                
            }
        }

		private void Button_Click_Admin(object sender, RoutedEventArgs e)
		{
            this.Close();
            AdminAddFilm adminAddFilm = new AdminAddFilm(ADMIN);
            adminAddFilm.Show();
        }

		private void ListBoxFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            var aaa = ListBoxFilms.SelectedItem as AppView;
            if (aaa != null)
            {
                SessionWPF sessionWPF = new SessionWPF(aaa.filmID.Trim(), ADMIN);
                sessionWPF.Show();
                this.Close();
            }
        }

	}
}
