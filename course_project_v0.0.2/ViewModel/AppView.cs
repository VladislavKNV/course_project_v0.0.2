using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using course_project_v0._0._2.DataBase;
using GalaSoft.MvvmLight;

namespace course_project_v0._0._2.View
{
	class AppView : ViewModelBase
    {
        public string filmID { get; set; }
        public string filmname { get; set; }
        public string year { get; set; }
        public byte[] poster { get; set; }
        public string plotDescription { get; set; }
        public string genres { get; set; }
        public string rating { get; set; }
        public string countries { get; set; }
        public string director { get; set; }
        public string actors { get; set; }
        public string duration { get; set; }
        public DateTimeOffsetConverter premiereDate { get; set; }
  



        public void Add(string _filmname, int _year,string _genres, float _rating, string _countries, string _director, int _duration)
        {
            filmname = _filmname;
            year = "Год: " + (int)_year;
            genres = "Жанры: " + _genres;
            rating = "Рейтинг: " + (float)_rating;
            countries = "Страны: " + _countries;
            director = "Режисёр: " +  _director;
            duration = "Продолжительность: " +  (int)_duration;

        }

        public void AddFilmsForAdmin(string _filmID, string _filmname, int _year,byte[] _poster,string _plotDescription, string _genres, float _rating, string _countries, string _director,string _actors, int _duration)
		{
            filmID = _filmID;
            filmname = _filmname;
            year = "" + (int)_year;
            poster = _poster;
            plotDescription = _plotDescription;
            genres = genres;
            rating = "" + (float)_rating;
            countries = _countries;
            director = _director;
            actors = _actors;
            duration = "" + (int)_duration;


		}


    }

}
