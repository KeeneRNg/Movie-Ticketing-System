//============================================================
// Student Number : S10222177, S10218985
// Student Name : Goh Tian Le Matthew, Keene Ng
// Module Group : T08
//============================================================

using System;
using System.Collections.Generic;
using System.IO;

namespace PRG2_T08_Team1
{
    class Program
    {        
        static void Main(string[] args)
        {
            // Data Lists
            List<Movie> mList = new List<Movie>();
            List<Cinema> cList = new List<Cinema>();
            List<Screening> sList = new List<Screening>();

            // Order List
            List<Order> oList = new List<Order>();

            // program flow
            while (true)
            {
                int choice = Menu();
                if (choice == 1)
                {
                    // Loading Moive and Cinema Data into lists
                    LoadMovieCinemaData(mList, cList);
                }
                else if (choice == 2)
                {
                    // Loading Screening Data into list
                    LoadScreeningData(sList, mList, cList);
                }
                else if (choice == 3)
                {
                    // Listing All Movies
                    ListMovies(mList);
                }
                else if (choice == 4)
                {
                    // Listing Movie Screenings
                    ListMovieScreenings(sList);
                }
                else if (choice == 5)
                {
                    // Adding a Movie Screening Session
                    AddMovieScreening(mList, sList, cList);
                }
                else if (choice == 6)
                {
                    // Delete a movie screening session
                    DeleteMovieScreening(sList);
                }
                else if (choice == 7)
                {
                    // Order Movie Ticket/s
                    OrderMovieTicket(mList, sList, oList);
                }
                else if (choice == 8)
                {
                    if (oList.Count != 0)
                    {
                        // Cancel Order Ticket
                        CancelOrder(oList);
                    }
                    else
                    {
                        Console.WriteLine("No existing orders.\n");
                    }
                    
                }
                else if (choice == 9)
                {
                    // Recommend movie based on sale of tickets sold
                    RecommendedMovie(oList, mList);
                }

                else if (choice ==10)
                {
                    //Display available seats of screening session in descending order
                    DisplaySeats(sList);
                }

                else if (choice == 0)
                {
                    break;
                }
                    
            }
        }
        // methods
        static int Menu()
        {
            Console.WriteLine(
                "=================Main Menu===================" +
                "\n=============================================" +
                "\n1. Load Movie and Cinema Data" +
                "\n2. Load Screening Data" +
                "\n3. List all movies" +
                "\n4. list movie screenings" +
                "\n5. Add a movie screening session" +
                "\n6. Delete a movie screening session" +
                "\n7. Order movie ticket/s" +
                "\n8. Cancel order of ticket" +
                "\n9. Movie recommendation" +
                "\n10. Display available seats of screening session in desc order" +
                "\n0. Exit" +
                "\n=============================================");

            // input validation
            int choice;
            while (true) // type check
            {
                try
                {
                    while (true) // range check
                    {
                        Console.Write("Enter an option as an integer between 1 and 10 inclusive: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        if (choice < 0 || choice > 10)
                        {
                            Console.WriteLine("Error: Option is not between 1 and 10 inclusive.");
                            Console.WriteLine("Please enter an integer within the range.\n");
                        }
                        else
                        {
                            break;
                        }
                    }                    
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Please enter an integer.\n");
                }
            }           
            
            return choice;
        }
        // General====================================================================================================================================================
        static void LoadMovieCinemaData(List<Movie> mList, List<Cinema> cList)
        {
            // loading movie data
            string[] movieList = File.ReadAllLines("csv/Movie.csv");
            
            for (int i = 1; i < movieList.Length; i++)
            {
                // initialising movie object
                string[] movie = movieList[i].Split(",");
                if (movie[0] == "")
                {
                    break;
                }
                string title = movie[0];
                int duration = Convert.ToInt32(movie[1]);
                string classification = movie[3];
                DateTime openingDate = Convert.ToDateTime(movie[movie.Length - 1]);
                List<string> genreList = new List<string>();

                string[] genre = movie[2].Split("/"); // split movie genre into array
                for (int x = 0; x < genre.Length; x++) // adding each movie genre into genreList
                {
                    genreList.Add(genre[x]);
                }

                Movie movieX = new Movie(title, duration, classification, openingDate, genreList);

                // adding movie object into mList
                mList.Add(movieX);
            }

            // loading cinema data
            string[] cinemaList = File.ReadAllLines("csv/Cinema.csv");

            for (int i = 1; i < cinemaList.Length; i++)
            {                
                // initialising cinema object
                string[] cinema = cinemaList[i].Split(",");
                string name = cinema[0];
                int hallNo = Convert.ToInt32(cinema[1]);
                int capacity = Convert.ToInt32(cinema[2]);

                Cinema cinemaX = new Cinema(name, hallNo, capacity);

                // adding cinema object into cList
                cList.Add(cinemaX);
            }
            Console.WriteLine(); // formatting to leave a line
        }
        static void LoadScreeningData(List<Screening> sList, List<Movie> mList, List<Cinema> cList)
        {
            string[] screeningList = File.ReadAllLines("csv/Screening.csv");
            int screeningNo = 1000; // parameter 1

            for (int i = 1; i < screeningList.Length; i++)
            {                
                // initialising cinema object
                string[] screening = screeningList[i].Split(",");
                
                Movie movie = new Movie(); // parameter 6

                for (int x = 0; x < mList.Count; x++) 
                {
                    if (screening[screening.Length - 1] == mList[x].Title) // if title of screening matches that in mList
                    {
                        screeningNo += 1; // add one to count
                        movie = mList[x];
                    }
                }
                
                DateTime screeningDateTime = Convert.ToDateTime(screening[0]); // parameter 2

                string screeningType = screening[1]; // parameter 3

                Cinema cinema = new Cinema(); // parameter 5

                for (int x = 0; x < cList.Count; x++)
                {
                    if (screening[2] == cList[x].Name && Convert.ToInt32(screening[3]) == cList[x].HallNo)
                    {
                        cinema = cList[x];
                    }
                }

                int seatsRemaining = cinema.Capacity; // parameter 4

                Screening screeningX = new Screening(screeningNo,screeningDateTime,screeningType,seatsRemaining, cinema, movie);

                // adding cinema object into cList
                sList.Add(screeningX);
            }
            Console.WriteLine(); // formatting to leave a line
        }
        static void ListMovies(List<Movie> mList)
        {
            // display movie headings
            Console.WriteLine("\n{0,-25} {1,-20} {2,-40} {3,-20} {4,-20}", "Movie Title", "Duration (mins)", "Genre", "Classification", "Opening Date");

            // diplay movie data
            for (int i = 0; i < mList.Count; i++)
            {
                string genre;
                if (mList[i].GenreList.Count > 0)
                {
                    genre = mList[i].GenreList[0];

                    for (int x = 0; x < mList[i].GenreList.Count; x++)
                    {
                        genre += "/" + mList[i].GenreList[x];
                }
                }
                else
                {
                    genre = mList[i].GenreList[0];
                }
                    
                
                Console.WriteLine("{0,-25} {1,-20} {2,-40} {3,-20} {4,-20}", mList[i].Title, mList[i].Duration, genre, mList[i].Classification, mList[i].OpeningDate);                
            }
            Console.WriteLine();
        }
        static void ListMovieScreenings(List<Screening> sList)
        {
            // display movie headings
            Console.WriteLine("\n{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", "Movie Title", "Cinema", "Hall No.", "Seats Remaing", "Type", "Date and Time", "Screening No.");

            for (int i = 0; i < sList.Count; i++)
            {
                Console.WriteLine("{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", sList[i].Movie.Title, sList[i].Cinema.Name, sList[i].Cinema.HallNo, sList[i].SeatsRemaining, sList[i].ScreeningType, sList[i].ScreeningDateTime, sList[i].ScreeningNo);
            }
            Console.WriteLine();
        }
        // Screening==================================================================================================================================================

        static void ListCinemas(List<Cinema> cList)
        {
            Console.WriteLine("\n{0,-25} {1,-12} {2,-10}", "Name", "Hall Number", "Capacity");

            for (int i = 0; i < cList.Count; i++)
            {
                Console.WriteLine("{0,-25} {1,-12} {2,-10}", cList[i].Name, cList[i].HallNo, cList[i].Capacity);
            }
            Console.WriteLine();
        }

        // 5) Add a movie screening session
        static void AddMovieScreening(List<Movie> mList, List<Screening> sList, List<Cinema> cList)
        {
            // 5.1) list all movies
            ListMovies(mList);
            // 5.2) prompt user to select a movie

            Movie inputMov = new Movie();

            while (true)
            {
                Console.Write("Please select a movie: ");
                string Movie = Console.ReadLine();
                int mCounter = 0;
                for (int i = 0; i < mList.Count; i++)
                {
                    if (Movie == mList[i].Title)
                    {
                        inputMov = mList[i];
                        mCounter = 1;
                        break;
                    }
                }

                if (mCounter == 1)
                {
                    break;
                }

                else
                {
                    Console.WriteLine("There is no movie with that name. Please try again.");
                }
            }

            // 5.3) prompt user to enter a screening type [2D/3D]
            string ScreeningType = "";
            while (true)
            {
                Console.Write("Please select a screening type [2D/3D]: ");
                ScreeningType = Console.ReadLine();

                if ((ScreeningType != "2D") && (ScreeningType != "3D"))
                {
                    Console.WriteLine("Invalid screening type. Please input either 2D or 3D.");
                }

                else
                {
                    break;
                }
            }

            // 5.4) prompt user to enter a screening date and time (check to see if the datetime entered is after the opening date of the movie)
            string temp = "";
            while (true)
            {
                Console.Write("Please enter a screening date (dd/mm/yyyy): ");
                string ScreeningDate = Console.ReadLine();
                Console.Write("Please enter a screening time (Hour and Minute separated by ':', followed by AM or PM): ");
                string ScreeningTime = Console.ReadLine();
                

                if ((ScreeningDate.Length != 10) || (ScreeningDate[2] != '/') || (ScreeningDate[5] != '/'))
                {
                    Console.WriteLine("Please use the correct date format");
                }

                else if ((Char.IsDigit(ScreeningDate[0]) == false) || (Char.IsDigit(ScreeningDate[1]) == false) || (Char.IsDigit(ScreeningDate[3]) == false) || (Char.IsDigit(ScreeningDate[4]) == false) || (Char.IsDigit(ScreeningDate[6]) == false) || (Char.IsDigit(ScreeningDate[7]) == false) || (Char.IsDigit(ScreeningDate[8]) == false) || (Char.IsDigit(ScreeningDate[9]) == false))//Checks for non-number values in date
                {
                    Console.WriteLine("Date entered is not valid. Please try again.");
                }

                else if ((ScreeningTime.Length != 6) && (ScreeningTime.Length != 7))//Checks that format of time is either e.g 8:30AM or 12:30AM
                {
                    Console.WriteLine("Please use the correct time format");
                }

                else if ((ScreeningTime.Length == 6) && ((ScreeningTime[1] != ':') || ((ScreeningTime.Substring(4) != "AM") && (ScreeningTime.Substring(4) != "PM")) || ((Char.IsDigit(ScreeningTime[0]) == false)) || (Char.IsDigit(ScreeningTime[2]) == false) || (Char.IsDigit(ScreeningTime[3]) == false))) //Checks that 6 character time input is correct formatting
                {
                    Console.WriteLine("Time entered is not valid. Please try again.");
                }

                else if ((ScreeningTime.Length == 7) && ((ScreeningTime[2] != ':') ||  ((ScreeningTime.Substring(5) != "AM") && (ScreeningTime.Substring(5) != "PM")) || ((Char.IsDigit(ScreeningTime[0]) == false)) || (Char.IsDigit(ScreeningTime[1]) == false) || (Char.IsDigit(ScreeningTime[3]) == false) || (Char.IsDigit(ScreeningTime[4]) == false))) //Checks that 7 character time input is correct formatting
                {
                    Console.WriteLine("Time entered is not valid. Please try again.");
                }

                else if (Convert.ToInt16(ScreeningDate.Substring(0,2)) > 31) //Checks that day input isnt more than 31
                {
                    Console.WriteLine("Day entered is " + ScreeningDate.Substring(0, 2));
                    Console.WriteLine("Day value entered is not possible. Try again.");
                }

                else if (Convert.ToInt16(ScreeningDate.Substring(3, 2)) > 12) //Checks that month input isnt more than 12
                {
                    Console.WriteLine("Month entered is " + ScreeningDate.Substring(3, 2));
                    Console.WriteLine("Month value entered is not possible. Try again.");
                }

                else if ((ScreeningTime.Length == 7) && (Convert.ToInt16(ScreeningTime.Substring(0, 2)) > 12)) //Checks that, for 7 character time, hour value isnt more than 12
                {
                    Console.WriteLine("Hour entered is " + ScreeningTime.Substring(0, 2));
                    Console.WriteLine("Hour value entered is not possible. Try again.");
                }

                else if ((ScreeningTime.Length == 6) && (Convert.ToInt16(ScreeningTime.Substring(2, 2)) > 59)) //Checks that, for 6 character time, minute value isnt more than 59
                {
                    Console.WriteLine("Minutes entered is " + ScreeningTime.Substring(2, 2));
                    Console.WriteLine("Minute value entered is not possible. Try again.");
                }

                else if ((ScreeningTime.Length == 7) && (Convert.ToInt16(ScreeningTime.Substring(3, 2)) > 59)) //Checks that, for 7 character time, minute value isnt more than 59
                {
                    Console.WriteLine("Minutes entered is " + ScreeningTime.Substring(3, 2));
                    Console.WriteLine("Minute value entered is not possible. Try again.");
                }

                else if (Convert.ToDateTime(ScreeningDate).Subtract(inputMov.OpeningDate).Days < 0) //Ensures that date input isnt before movie release (works)
                {
                    Console.WriteLine("The screening cannot happen before the movie is released! Please try again.");
                }

                else
                {
                    temp = ScreeningDate + " " + ScreeningTime; //Used to format date time inside loop as SDate and STime are not global
                    break;
                }
            }

            DateTime finalInputDateTime = Convert.ToDateTime(temp); //temp defined above, used now to get datetime input

            // 5.5) list all cinema halls
            ListCinemas(cList);
            // 5.6) prompt user to select a cinema hall (check to see if the cinema hall is available at the datetime entered in point 4) [need to consider the movie duration and cleaning time]
            
            Cinema inputCinema = new Cinema();

            while (true)
            {
                Console.Write("Please select a cinema hall name: "); //Validation for this at the end
                string cinemaName = Console.ReadLine();
                int cineHallNumber = 0; //Used as temporary variable so that it can be assigned a value in loop
                while (true) //Validation loop for hall number
                {
                    Console.Write("Please select a cinema hall number: ");
                    string checkCineHallNumber = Console.ReadLine();

                    if(checkCineHallNumber.Length != 1) //Ensures hall input is char
                    {
                        Console.WriteLine("Hall number can only be a single digit. Please try again.");
                    }

                    else if(Char.IsDigit(Convert.ToChar(checkCineHallNumber)) == false) //Ensures input is a number
                    {
                        Console.WriteLine("Hall number must be a number. Please try again.");
                    }

                    else //assigns input to global variable
                    {
                        cineHallNumber = Convert.ToInt32(checkCineHallNumber);
                        break;
                    }
                }

                int cCounter = 0; //Used to validate that cinema name and hall are correct and in list
                for (int i = 0; i < cList.Count; i++)
                {
                    if ((cinemaName == cList[i].Name) && (cineHallNumber == cList[i].HallNo))
                    {
                        inputCinema = cList[i];
                        cCounter = 1;
                        break;
                    }
                }

                if (cCounter == 1) //True if cinema name and hall exists together
                {

                    bool SameDayScreening = false; //Used in a future check, default is false
                    bool ScreenSlotAvail = true; //Used in a future check, default is true
                    List<int> SameDayScreeningList = new List<int>(); //List of screenings that are on the same day as input
                    for (int i = 0; i < sList.Count; i++) //Looks through screening list
                    {
                        if ((sList[i].Cinema.Name == inputCinema.Name) && (sList[i].Cinema.HallNo == inputCinema.HallNo)) //Checks for a cinema and hall that matches
                        {
                            if ((sList[i].ScreeningDateTime.Day == finalInputDateTime.Day) && (sList[i].ScreeningDateTime.Month == finalInputDateTime.Month) && (sList[i].ScreeningDateTime.Year == finalInputDateTime.Year)) //Executes if either day, month or year are different
                            {
                                SameDayScreening = true; //Says there is a screening on the same day
                                SameDayScreeningList.Add(i); //Adds the screening index to the list
                            }
                        }
                    }

                    if (SameDayScreening == false) //If no other screenings on same day, slot is automatically available
                    {
                        ScreenSlotAvail = true;
                    }

                    else //There is/are screening(s) on the same day
                    {
                        for (int i = 0; i < SameDayScreeningList.Count; i++) //Looks through each screening thats on the same day
                        {

                            double numberOfHours = (sList[SameDayScreeningList[i]].Movie.Duration / 30) + 1; //Gets number of complete half hours needed for the movie. IRL movies also screen in 30-60 minute intervals
                            int EstimateMovieLength = (int)(Math.Ceiling(numberOfHours) * 30) + 30; //Gets total time taken for movie + cleaning time
                            DateTime CineHallAvailAfter = sList[SameDayScreeningList[i]].ScreeningDateTime.AddMinutes(EstimateMovieLength); //Sets time that the hall is available after cleaning
                            double inputNumberOfHours = (inputMov.Duration / 30) + 1; //Gets number of complete half hours needed for the input movie. IRL movies also screen in 30-60 minute intervals
                            int inputEstimateMovieLength = (int)(Math.Ceiling(inputNumberOfHours) * 30) + 30; //Gets total time taken for input movie + cleaning time
                            DateTime CineHallAvailBefore = sList[SameDayScreeningList[i]].ScreeningDateTime.AddMinutes(-(inputEstimateMovieLength)); //Sets time that the hall is available before cleaning

                            if ((finalInputDateTime < CineHallAvailBefore) || (finalInputDateTime >= CineHallAvailAfter)) //If input time is within the time available, executes
                            {
                                Console.WriteLine("Cinema hall is available at that timeslot!"); //Does not change state of ScreenSlotAvail as it is default true, and if it hits a false it must stay false
                            }
                            
                            else //If input time is within the time the hall is unavailable,
                            {
                                ScreenSlotAvail = false; //Sets state of ScreenSlotAvail to false. Once it is set to false, if there is more than 1 screening in a day, it will not be overridden as it will always fall in unavailable range
                            }
                        }
                    }



                    if (ScreenSlotAvail == false) //If input time is within the unavailable range for any movie
                    {
                        Console.WriteLine("Cinema Hall is unavailable during that timeslot! Choose a different cinema hall.");
                        continue;
                    }

                    else //Slot is okay, all inputs correct, thus loop can end and break ends loop.
                    {
                        Console.WriteLine("Slot available");
                        break;
                    }
                }

                else //Validation input for cinema name and hall number
                {
                    Console.WriteLine("Cinema name or hall number does not exist. Please try again.");
                }
            }



            // 5.7) create a Screening object with the information given and add to the relevant screening list
            Screening screeningTemp = new Screening((sList[sList.Count-1].ScreeningNo + 1), finalInputDateTime, ScreeningType, inputCinema.Capacity, inputCinema, inputMov);

            sList.Add(screeningTemp);

            // 5.8) display the status of the movie screening session creation (i.e. successful or unsuccessful)
            Console.WriteLine("\nScreening creation successful!\n");
        }


        // 6) Delete a movie screening session
        static void ListUnsoldScreenings(List<Screening> sList)
        {
            List<Screening> unsoldSList = new List<Screening> ();

            for(int i = 0; i < sList.Count; i++)
            {
                if(sList[i].SeatsRemaining == sList[i].Cinema.Capacity)
                {
                    unsoldSList.Add(sList[i]);
                }
            }

            Console.WriteLine("\n{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", "Movie Title", "Cinema", "Hall No.", "Seats Remaing", "Type", "Date and Time", "Screening No.");

            for (int x = 0; x < unsoldSList.Count; x++)
            {
                Console.WriteLine("{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", unsoldSList[x].Movie.Title, unsoldSList[x].Cinema.Name, unsoldSList[x].Cinema.HallNo, unsoldSList[x].SeatsRemaining, unsoldSList[x].ScreeningType, unsoldSList[x].ScreeningDateTime, unsoldSList[x].ScreeningNo);
            }
            Console.WriteLine();
        }
        static void DeleteMovieScreening(List<Screening> sList)
        {

            // 6.1) list all movie screening sessions that have not sold any tickets
            List<Screening> unsoldSList = new List<Screening>();

            for (int i = 0; i < sList.Count; i++)
            {
                if (sList[i].SeatsRemaining == sList[i].Cinema.Capacity)
                {
                    unsoldSList.Add(sList[i]);
                }
            }

            if(unsoldSList.Count == 0)
            {
                Console.WriteLine("\nNo screenings without unsold tickets\n");
            }

            else
            {
                ListUnsoldScreenings(sList);



                // 6.2) prompt user to select a session
                string ScNo = "";

                while (true)
                {
                    Console.Write("Please select a screening by entering its Screening Number: ");

                    ScNo = Console.ReadLine();

                    int ListCheck = 0;

                    for (int i = 0; i < unsoldSList.Count; i++)
                    {
                        if (ScNo == Convert.ToString(unsoldSList[i].ScreeningNo))
                        {
                            ListCheck += 1;
                        }
                    }

                    if (ListCheck == 0)
                    {
                        Console.WriteLine("The Screening Number you entered is not in the list. Please try again");
                    }

                    else
                    {
                        break;
                    }
                }

                // 6.3) remove the movie screening from all screening lists
                for (int i = 0; i < unsoldSList.Count; i++)
                {
                    if (ScNo == Convert.ToString(unsoldSList[i].ScreeningNo))
                    {
                        unsoldSList.Remove(unsoldSList[i]);
                    }

                }

                for (int i = 0; i < sList.Count; i++)
                {
                    if (ScNo == Convert.ToString(sList[i].ScreeningNo))
                    {
                        sList.Remove(sList[i]);
                    }

                }

                // 6.4) display the status of the removal (i.e. successful or unsuccessful)
                Console.WriteLine("\nRemoval success!\n");
            }
        }

        // Order======================================================================================================================================================
        static void OrderMovieTicket(List<Movie> mList, List<Screening> sList, List<Order> oList)
        {
            while (true) // exits order function if all ticket holders do not meet the movie classification requirements
            {
                // if no data is loaded
                if ( (mList.Count == 0) || (sList.Count == 0) )
                {
                    Console.WriteLine("Error: No movie or screening data loaded.\n");
                    break;
                }

                // 7.1) list all movies
                ListMovies(mList);

                Movie m = new Movie();

                while (true) // input validation for movie selection to see if movie title entered exists in the list
                {
                    // 7.2) prompt user to select a movie
                    Console.Write("Please select a movie by entering its title: ");

                    // initilizing movie object selected
                    string movieTitle = Console.ReadLine();
                    bool found = false;

                    for (int i = 0; i < mList.Count; i++)
                    {
                        if (mList[i].Title == movieTitle)
                        {
                            m = mList[i]; // initilizing Movie object m
                            found = true;
                            break;
                        }
                    }

                    
                    bool screening = false;

                    for (int i = 0; i < sList.Count; i++)
                    {
                        // validate if there is/are available screening sessions
                        if (sList[i].Movie == m) 
                        {
                            screening = true;
                            break;
                        }
                    }
                    
                    if (found)
                    {
                        if (screening)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error. Movie does not have a screening session.");
                            Console.WriteLine("Please select another movie.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error. Movie not found. Please select a movie found in the list.\n");
                    }
                }

                // 7.3) list all movie screenings of the selected movie              
                List<Screening> screeningSelections = new List<Screening>();

                // display movie headings
                Console.WriteLine("\n{0,-25} {1,-20} {2,-20} {3,-10} {4,-25} {5,-20}", "Movie Title", "Cinema", "Seats Remaing", "Type", "Date and Time", "Screening No.");

                for (int i = 0; i < sList.Count; i++)
                {
                    if (sList[i].Movie == m)
                    {
                        screeningSelections.Add(sList[i]);
                        Console.WriteLine("{0,-25} {1,-20} {2,-20} {3,-10} {4,-25} {5,-20}",
                            sList[i].Movie.Title, sList[i].Cinema.Name, sList[i].SeatsRemaining, sList[i].ScreeningType, sList[i].ScreeningDateTime, sList[i].ScreeningNo);
                    }
                }
                Console.WriteLine();
                            

                int sNo;
                Screening s = new Screening();

                while (true) // input validation for movie screeening selection
                {
                    try // type validation
                    {
                        while (true) // validation to see if screening number exists
                        {
                            // 7.4) prompt the user to select movie screening
                            Console.Write("Please select a movie screening by entering screening number: ");

                            // 7.5) retrieve the selected movie
                            sNo = Convert.ToInt32(Console.ReadLine()); // screening number                
                            bool found = false;

                            for (int i = 0; i < screeningSelections.Count; i++)
                            {
                                if (screeningSelections[i].ScreeningNo == sNo && screeningSelections[i].SeatsRemaining != 0) // check if screening number exists and that there are available seats
                                {
                                    s = screeningSelections[i];
                                    found = true;
                                    break;                                                                                                          
                                }                                                               
                            }
                            if (found)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid screening number.");
                                Console.WriteLine("Please enter a valid screening number found in the list / that has available seating.\n");
                            }
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Please enter an integer.\n");
                    }
                }

                // 7.6) prompt user to enter the total number of tickets to order
                int noTicketsToOrder;

                while (true) // input validation for noTicketsToOrder
                {
                    try // type validation
                    {
                        while (true) // range validation
                        {
                            Console.Write("Please enter the total number of tickets to order: ");
                            noTicketsToOrder = Convert.ToInt32(Console.ReadLine());

                            if (noTicketsToOrder > s.SeatsRemaining || noTicketsToOrder < 0)
                            {
                                Console.WriteLine("Error: Integer out of range.");
                                Console.WriteLine("Please enter a valid number between 1 and the number of seats remaining inclusive\n");
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Please enter an integer.\n");
                    }
                }

                // 7.7) prompt user if all ticket holders meet the movie requirement
                if (m.Classification != "G")
                {
                    string allowed;
                    while (true)
                    {
                        Console.Write("Do all ticket holders meet the movie classfication requirements? (Y/N): ");
                        allowed = Console.ReadLine();
                        if (allowed == "Y" || allowed == "N")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid option.");
                            Console.WriteLine("Please enter 'Y' or 'N'.\n");
                        }
                    }
                    if (allowed == "N")
                    {
                        Console.WriteLine(); // formatting - leave a line
                        break;
                    }
                    Console.WriteLine(); // formatting leave a line
                }

                // 7.8) create an Order object with status "Unpaid"
                Order orderX = new Order();
                orderX.OrderNo = oList.Count + 1;
                orderX.Status = "Unpaid";
                orderX.TicketList = new List<Ticket>();

                // prompt user for a response depending on the type of ticket ordered
                for (int i = 0; i < noTicketsToOrder; i++)
                {
                    string type;

                    while (true) // input validation for type of ticket ordered
                    {
                        Console.Write("Please enter type of ticket ordered ('s', 'sc', 'a'): ");
                        type = Console.ReadLine();

                        if (type == "s" || type == "sc" || type == "a")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid ticket type.");
                            Console.WriteLine("Please select a valid ticket type.\n");
                        }
                    }                    

                    if (type == "s")
                    {
                        string levelOfStudy;

                        while (true) // input validation for student level of study
                        {
                            // 7.9ai) prompt user for level or study
                            Console.Write("Enter student's level or study [Primary, Secondary, Tertiary]: ");
                            levelOfStudy = Console.ReadLine();

                            if (levelOfStudy == "Primary" || levelOfStudy == "Secondary" || levelOfStudy == "Tertiary")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid level of study.");
                                Console.WriteLine("Please enter a valid level of study.\n");
                            }
                        }                        

                        // 7.9b) creating student ticket and adding it to tList
                        Student student = new Student(s, levelOfStudy);
                        orderX.AddTicket(student);
                    }
                    else if (type == "sc")
                    {
                        int yob;

                        while (true) // input validation
                        {
                            try // type validation
                            {

                                while (true) // input validation for yob input to check if age >= 55 years
                                {
                                    // 7.9aii) prompt user for senior citizen's year of birth (must be 55 years and above)
                                    Console.Write("Enter senior citizen's year of birth: ");
                                    yob = Convert.ToInt32(Console.ReadLine());

                                    if (DateTime.Now.Year - yob >= 55)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Age of senior citizen < 55.");
                                        Console.WriteLine("Please enter a valid year such that senior citizen's age is <= 55.\n");
                                    }
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                                Console.WriteLine("Please enter an integer.\n");
                            }
                        }                                                                  

                        // 7.9b) creating senior citizen ticket and adding it to tList
                        SeniorCitizen seniorCitizen = new SeniorCitizen(s, yob);
                        orderX.AddTicket(seniorCitizen);
                    }
                    else // Adult
                    {
                        // 7.9aiii) prompt user if adult wants popcorn for $3                    
                        bool popcorn;

                        while (true) // input validation for popcorn
                        {
                            Console.Write("Popcorn for $3? [Y/N]: ");
                            string pc = Console.ReadLine();

                            if (pc == "Y")
                            {
                                popcorn = true;
                                break;

                            }
                            else if (pc == "N")
                            {
                                popcorn = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid input.");
                                Console.WriteLine("Please type 'Y' or 'N'.\n");
                            }
                        }

                        // 7.9b) creating adult ticket and adding it to tList
                        Adult adult = new Adult(s, popcorn);
                        orderX.AddTicket(adult);                       
                    }                    
                }

                // 7.9d) updating seats remaining for the movie screening
                for (int i = 0; i < sList.Count; i++)
                {
                    if (sList[i] == s)
                    {
                        sList[i].SeatsRemaining -= noTicketsToOrder;
                    }
                }

                // 7.10) payable amount
                double amt = 0;
                Console.WriteLine(); // formatting - skip a line

                for (int i = 0; i < orderX.TicketList.Count; i++)
                {
                    if (orderX.TicketList[i] is Adult)
                    {
                        Adult a = (Adult)orderX.TicketList[i];
                        if (a.PopcornOffer == true)
                        {
                            amt += 3.00;
                        }
                    }
                    amt += orderX.TicketList[i].CalculatePrice();
                }

                Console.WriteLine("===========================" +
                    "\nPayable amount: ${0:0.00}\n", amt);

                // 7.11) prompt user to press any key to make payment
                Console.Write("Press any key to make payment...");
                Console.ReadKey();

                // 7.12) filling in necessary details to new order
                orderX.Amount = amt;
                orderX.OrderDateTime = DateTime.Now;
                // 7.13) change order status to "Paid"
                orderX.Status = "Paid";
                // 7.9c) adding order to oList
                oList.Add(orderX);

                // final message
                Console.WriteLine("\nOrder completed!\n");
                break;
            }
        }
        static void CancelOrder(List<Order> oList)
        {
            int oNo;

            while (true) // input validation
            {
                try
                {
                    while (true)
                    {
                        // 8.1) prompt user for order number
                        Console.Write("Please enter order number to cancel: ");
                        oNo = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(oList.Count);
                        if (oNo <= oList.Count && oNo > 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Number not in range.");
                            Console.WriteLine("Please enter a number between 1 and {0}", oList.Count);
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Please enter an integer.\n");
                }
            }

            // 8.2) retrieve the selected order
            Order orderToCancel = new Order();
            
            for (int i = 0; i < oList.Count; i++)
            {
                if (oList[i].OrderNo == oNo)
                {
                    orderToCancel = oList[i];
                }
            }

            // 8.3) check if screening in the selected order is screened
            DateTime screening = orderToCancel.TicketList[0].Screening.ScreeningDateTime;
            DateTime present = DateTime.Now;
            int compare = DateTime.Compare(screening, present);
            
            if (compare < 0)
            {
                // screening is screened (reject cancellation)                
                // 8.7) display status of cancelation
                Console.WriteLine("Cancellation unsuccessful.\n");
            }
            else
            {
                // screening has not screened yet (accept cancellation)
                for (int i = 0; i < orderToCancel.TicketList.Count; i++) // 8.4) update seat remaining
                {
                    orderToCancel.TicketList[0].Screening.SeatsRemaining += 1;
                }

                orderToCancel.Status = "Cancelled"; // 8.5) change order status to "Cancelled"

                // 8.6) display a message indicating that the amount is refunded
                Console.WriteLine("Amount has been refunded.");

                // 8.7) display status of cancelation
                Console.WriteLine("Cancellation successful!\n");
            }
        }

        // Advanced===================================================================================================================================================
        // 3.1) Recommend movie based on sale of tickets sold
        static void RecommendedMovie(List<Order> oList, List<Movie> mList)
        {
            // creating a list for total number of tickets sold for each movie
            List<int> totalTicketsSoldForEachMovie = new List<int>();

            // initialising list
            for (int i = 0; i < mList.Count; i++)
            {
                totalTicketsSoldForEachMovie.Add(0);
            }

            // calculating total tickets sold for each movie and updating the list
            for (int i = 0; i < oList.Count; i++) // for each order
            {
                if (oList[i].Status == "Paid")
                {
                    Ticket t = oList[i].TicketList[0];
                    string title;

                    // downcasting and finding movie title
                    if (t is Student)
                    {
                        Student ticket = (Student)t;
                        title = ticket.Screening.Movie.Title;
                    }
                    else if (t is SeniorCitizen)
                    {
                        SeniorCitizen ticket = (SeniorCitizen)t;
                        title = ticket.Screening.Movie.Title;
                    }
                    else
                    {
                        Adult ticket = (Adult)t;
                        title = ticket.Screening.Movie.Title;
                    }

                    // finding index of movie ticket count and updating it
                    for (int x = 0; x < mList.Count; x++)
                    {
                        if (mList[x].Title == title)
                        {
                            totalTicketsSoldForEachMovie[x] += oList[i].TicketList.Count;
                        }
                    }
                }                
            }
            // displaying movie tickets sold
            Console.WriteLine("\n{0,-30} {1,-10}" +
                "\n===========================================", "Movie Title", "Tickets Sold"); // display heading

            string recommendation = "";
            int largest = 0;

            for (int i = 0; i < mList.Count; i++)
            {
                // display movie title and number of tickets sold
                Console.WriteLine("{0,-30} {1,-10}", mList[i].Title, totalTicketsSoldForEachMovie[i]);

                // find movie with the most movie tickets sold
                if (totalTicketsSoldForEachMovie[i] > largest)
                {
                    largest = totalTicketsSoldForEachMovie[i];
                    recommendation = mList[i].Title;
                }
                else if (totalTicketsSoldForEachMovie[i] == largest)
                {
                    recommendation = recommendation + " or " + mList[i].Title;
                }
            }

            // display recommendation
            if (largest == 0)
            {
                Console.WriteLine("No recommendation.");
            }
            else
            {
                Console.WriteLine("\nMovie Recommendation(s): {0}\n", recommendation);
            }
            
        }
        
        // 3.2) Display available seats of screening session in descending order
        static void DisplaySeats(List<Screening> sList)
        {
            if (sList.Count == 0)
            {
                Console.WriteLine("\nNo movie screenings! Please add some first!\n");
            }

            else
            {
                sList.Sort();
                Console.WriteLine("\n{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", "Movie Title", "Cinema", "Hall No.", "Seats Remaing", "Type", "Date and Time", "Screening No.");

                for (int i = 0; i < sList.Count; i++)
                {
                    Console.WriteLine("{0,-25} {1,-20} {2,-10} {3,-20} {4,-10} {5,-25} {6,-20}", sList[i].Movie.Title, sList[i].Cinema.Name, sList[i].Cinema.HallNo, sList[i].SeatsRemaining, sList[i].ScreeningType, sList[i].ScreeningDateTime, sList[i].ScreeningNo);
                }
                Console.WriteLine();
            }
        }
    }
}