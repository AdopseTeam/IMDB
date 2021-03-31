using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcSeries.Data;
using MvcActor.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace MvcMovie.Models
{
    public static class SeedMovies
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            const string URL = "https://api.themoviedb.org/3/movie/popular";
            string urlParameters = "?api_key=<api_key>&language=en-US&page=1";
            HttpClient client =  new HttpClient();
            client.BaseAddress = new Uri(URL);
            // Add headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                JObject joResponse = JObject.Parse(dataObjects);
                JArray ojObject = (JArray)joResponse["results"];

                 using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
                {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                } else {
                    foreach(var item in ojObject.Children()){
                        context.Movie.Add(
                            new Movie{
                                    Id = (int)item["id"],
                                    Title = (string)item["original_title"],
                                    ReleaseDate = DateTime.Parse((string)item["release_date"]),
                                    Genre = "Unknown",
                                    Rating = (int)item["vote_average"],
                                }
                        );
                    }
                }

                context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}

namespace MvcSeries.Models
{
    public static class SeedSeries
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcSeriesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcSeriesContext>>()))
            {
                // Look for any series.
                if (context.Series.Any())
                {
                    return;   // DB has been seeded
                }

                context.Series.AddRange(
                    new Series
                    {
                        Title = "Two and a half men",
                        ReleaseDate = DateTime.Parse("2003-2-12"),
                        Genre = "Comedy",
                        Seasons = 5,
                        Rating = 7.5M
                    },

                    new Series
                    {
                        Title = "See ",
                        ReleaseDate = DateTime.Parse("2019-3-13"),
                        Genre = "Action",
                        Seasons = 2,
                        Rating = 8.9M
                    },

                    new Series
                    {
                        Title = "After life",
                        ReleaseDate = DateTime.Parse("2018-2-23"),
                        Genre = "Drama",
                        Rating = 6
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

namespace MvcActor.Models
{
    public static class SeedActors
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcActorContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcActorContext>>()))
            {
                // Look for any actors.
                if (context.Actor.Any())
                {
                    return;   // DB has been seeded
                }

                context.Actor.AddRange(
                    new Actor
                    {
                        FirstName = "Tom",
                        Birthday = DateTime.Parse("1996-2-12"),
                        LastName = "Holldand"
                    },

                    new Actor
                    {
                        FirstName = "Dwayne",
                        Birthday = DateTime.Parse("1972-3-13"),
                        LastName = "Johnson"
                    },

                    new Actor
                    {
                        FirstName = "Emma",
                        Birthday = DateTime.Parse("1990-2-23"),
                        LastName = "Watchon"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
