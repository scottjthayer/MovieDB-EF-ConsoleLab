using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie_Database_EF_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            //creates db then commented out to not duplicate
            //CreateDB();


            //I really dislike how cluttered the main method is. I am assuming Justin will show us a better way. But anywho.
            bool runAgain = true;
            Console.WriteLine("Welcome to the movie database application.");
            while (runAgain == true)
            {
                Console.WriteLine("\nWould you like to search by genre or title?\nPlease enter 1 for 'genre' or 2 for 'title'");

                int input = Validator.Validator.GetInt(1, 2);
                if (input == 1)
                {
                    List<Movie> genreSearch = SearchByGenre();
                    Console.WriteLine("\nThe movies in the selected genre include:");
                    for (int i = 0; i < genreSearch.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {genreSearch[i].Title}. Runtime: {genreSearch[i].Runtime} minutes.");
                    }
                }

                else if (input == 2)
                {
                    List<Movie> titleSearch = SearchByTitle();
                    for (int i = 0; i < titleSearch.Count; i++)
                    {
                        Console.WriteLine($"\n{titleSearch[i].Title} is part of the {titleSearch[i].Genre} genre. Runtime: {titleSearch[i].Runtime} minutes");
                    }
                }

                runAgain = Validator.Validator.Repeat();
            }
        }
        //displays all movies in database.
        static void DisplayMovieDB()
        {
            using (MovieDBContext context = new MovieDBContext())
            {
                foreach (Movie m in context.Movies)
                {
                    Console.WriteLine($"{m.Id}. {m.Title}");
                }
            }
        }

        static List<Movie> SearchByGenre()
        {
            using (MovieDBContext context = new MovieDBContext())
            {
                List<string> genres = new List<string>();
                Console.WriteLine("These are the genres of films in our database:");
                //finds the genre for each film
                foreach (Movie m in context.Movies)
                {
                    genres.Add(m.Genre);
                }
                //finds unique genres. no duplicates.
                genres = genres.Distinct().ToList();

                //prints the unique genre list
                foreach (string g in genres)
                {
                    Console.WriteLine(g);
                }

                Console.WriteLine("\nWhich genre would you like to search by?");
                string input = Validator.Validator.GetGenreMovies();

                //puts all movies from selected genre onto new list using LINQ
                List<Movie> movies = context.Movies.Where(m => m.Genre == input).ToList();

                return movies;
            }
        }

        static List<Movie> SearchByTitle()
        {
            using (MovieDBContext context = new MovieDBContext())
            {
                List<Movie> movies = new List<Movie>();
                Console.WriteLine("\nThese are the titles that we have in our database currently.");

                DisplayMovieDB();

                Console.WriteLine("\nPlease select a title that you are interested in by entering the title.");
                int input = Validator.Validator.GetInt(1, context.Movies.Count());
                //adds the film selected to the list to be returned
                foreach (Movie m in context.Movies)
                {
                    if (m.Id == input)
                    {
                        movies.Add(m);
                        break;
                    }
                }
                return movies;
            }
        }


        static void CreateDB()
        {
            using (MovieDBContext context = new MovieDBContext())
            {
                Movie m1 = new Movie
                {
                    Title = "The Hunger Games",
                    Genre = "Action",
                    Runtime = 142
                };
                Movie m2 = new Movie
                {
                    Title = "The Thing",
                    Genre = "Horror",
                    Runtime = 109
                };
                Movie m3 = new Movie
                {
                    Title = "Blade Runner",
                    Genre = "Sci-Fi",
                    Runtime = 117
                };
                Movie m4 = new Movie
                {
                    Title = "The Evil Dead",
                    Genre = "Horror",
                    Runtime = 85
                };
                Movie m5 = new Movie
                {
                    Title = "Hellraiser",
                    Genre = "Horror",
                    Runtime = 94
                };
                Movie m6 = new Movie
                {
                    Title = "The Princess Bride",
                    Genre = "Adventure",
                    Runtime = 98
                };
                Movie m7 = new Movie
                {
                    Title = "The Cabin in the Woods",
                    Genre = "Mystery",
                    Runtime = 95
                };
                Movie m8 = new Movie
                {
                    Title = "Cashback",
                    Genre = "Comedy",
                    Runtime = 102
                };
                Movie m9 = new Movie
                {
                    Title = "Jumanji",
                    Genre = "Adventure",
                    Runtime = 104
                };
                Movie m10 = new Movie
                {
                    Title = "The Usual Suspects",
                    Genre = "Crime",
                    Runtime = 106
                };
                Movie m11 = new Movie
                {
                    Title = "Insidious",
                    Genre = "Horror",
                    Runtime = 103
                };
                Movie m12 = new Movie
                {
                    Title = "The Pianist",
                    Genre = "Biography",
                    Runtime = 150
                };
                Movie m13 = new Movie
                {
                    Title = "She's All That",
                    Genre = "Comedy",
                    Runtime = 95
                };

                context.Movies.Add(m1);
                context.Movies.Add(m2);
                context.Movies.Add(m3);
                context.Movies.Add(m4);
                context.Movies.Add(m5);
                context.Movies.Add(m6);
                context.Movies.Add(m7);
                context.Movies.Add(m8);
                context.Movies.Add(m9);
                context.Movies.Add(m10);
                context.Movies.Add(m11);
                context.Movies.Add(m12);
                context.Movies.Add(m13);

                context.SaveChanges();
            }
        }
    }
}
