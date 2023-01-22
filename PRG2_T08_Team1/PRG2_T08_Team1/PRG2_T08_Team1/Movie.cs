//============================================================
// Student Number : S10222177, S10218985
// Student Name : Goh Tian Le Matthew, Keene Ng
// Module Group : T08
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T08_Team1
{
    class Movie
    {
        // properties
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; }
        public List<Screening> ScreeningList { get; set; }

        // constructors
        public Movie() { }
        public Movie(string t, int d, string cs, DateTime od, List<string> gl)
        {
            Title = t;
            Duration = d;
            Classification = cs;
            OpeningDate = od;
            GenreList = gl;
        }

        // methods
        public void AddGenre(string genre)
        {
            GenreList.Add(genre);
        }
        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }
        public override string ToString()
        {
            return "Title: " + Title +
                "\tDuration: " + Duration +
                "\tClassification: " + Classification +
                "\tOpeningDate: " + OpeningDate +
                "\tGenreList: " + GenreList;
        }
    }
}
