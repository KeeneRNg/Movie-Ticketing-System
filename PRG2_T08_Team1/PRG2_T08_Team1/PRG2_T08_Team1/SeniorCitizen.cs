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
    class SeniorCitizen:Ticket
    {
        // property
        public int YearOfBirth { get; set; }

        // constructors
        public SeniorCitizen() : base() 
        {

        }

        public SeniorCitizen(Screening s, int YOB) : base(s)
        {
            YearOfBirth = YOB;
        }


        // methods
        public override double CalculatePrice()
        {
            if(Screening.ScreeningDateTime.Subtract(Screening.Movie.OpeningDate).Days <= 7) //Runs code for adult prices if within 7 days of movie release
            {
                if(Screening.ScreeningType == "3D")
                {
                    if((Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Friday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Saturday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Sunday))) //Checks For Weekend
                    {
                        Console.WriteLine("The senior citizen ticket price is: $14");
                        return (14);
                    }

                    else //Runs code for weekdays
                    {
                        Console.WriteLine("The senior citizen ticket price is: $11");
                        return (11);
                    }
                }

                else //Runs code for ScreeningType != "3D", a.k.a ScreeningType = "2D" if i coded it right lol
                {
                    if((Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Friday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Saturday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Sunday))) //Checks For Weekend
                    {
                        Console.WriteLine("The senior citizen ticket price is: $12.50");
                        return (12.50);
                    }

                    else //Runs code for weekdays
                    {
                        Console.WriteLine("The senior citizen ticket price is: $8.50");
                        return (8.50);
                    }
                }

            }

            else //Runs code for senior prices
            {
                if(Screening.ScreeningType == "3D")
                {
                    if((Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Friday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Saturday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Sunday))) //Checks For Weekend
                    {
                        Console.WriteLine("The senior citizen ticket price is: $14");
                        return (14);
                    }

                    else //Runs code for weekdays
                    {
                        Console.WriteLine("The senior citizen ticket price is: $6");
                        return (6);
                    }
                }

                else//Runs code for ScreeningType != "3D", a.k.a ScreeningType = "2D" if i coded it right lol
                {
                    if((Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Friday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Saturday)) || (Screening.ScreeningDateTime.DayOfWeek == (DayOfWeek.Sunday))) //Checks For Weekend
                    {
                        Console.WriteLine("The senior citizen ticket price is: $12.50");
                        return (12.50);
                    }

                    else //Runs code for weekdays
                    {
                        Console.WriteLine("The senior citizen ticket price is: $5");
                        return (5);
                    }
                }
            }
        }

        public override string ToString()
        {
            return base.ToString() + "\t Year Of Birth: " + YearOfBirth;
        }
    }
}
