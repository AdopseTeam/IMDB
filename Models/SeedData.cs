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

namespace HTTP{
    public static class Response{
        public static JObject returnResponse(string URL, string urlParameters) {
        HttpClient client =  new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = client.GetAsync(urlParameters).Result;
        var GdataObjects = response.Content.ReadAsStringAsync().Result;
        JObject joResponse = JObject.Parse(GdataObjects);
        client.Dispose();
        return joResponse;
    }
    }
}


namespace MvcMovie.Models
{
    public static class SeedMovies
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Get movie genres
            const string GURL = "https://api.themoviedb.org/3/genre/movie/list";
            string GurlParameters = "?api_key=<api_key>&language=en-US";
            var genreResponse = HTTP.Response.returnResponse(GURL, GurlParameters);
            JArray genrejObject = (JArray)genreResponse["genres"];

            JArray movieObject = new JArray();
            for(int i=0; i<20; i++){
                const string URL = "https://api.themoviedb.org/3/movie/popular";
                string urlParameters = "?api_key=<api_key>&language=en-US&page=1";
                var movieReponse = HTTP.Response.returnResponse(URL, urlParameters);
                movieObject.Merge((JArray)movieReponse["results"]);
            }

            using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any()){
                    return;   // DB has been seeded
                } else {
                    // Loop through the movies
                    foreach(var item in movieObject.Children()){
                        var genre_id = (string)item["genre_ids"].First;
                        // Loop through the genres to find the matching id
                        foreach(var genreObj in genrejObject){
                            if((string)genreObj["id"] == genre_id){
                                var genre = (string)genreObj["name"];
                                context.Movie.Add(
                                new Movie{
                                    Title = (string)item["original_title"],
                                    ReleaseDate = DateTime.Parse((string)item["release_date"]),
                                    Genre = genre,
                                    Rating = (int)item["vote_average"],
                                }
                        );
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}

namespace MvcSeries.Models
{
    public static class SeedSeries
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            const string GURL = "https://api.themoviedb.org/3/genre/tv/list";
            string GurlParameters = "?api_key=<api_key>&language=en-US";
            var genreResponse = HTTP.Response.returnResponse(GURL, GurlParameters);
            JArray genrejObject = (JArray)genreResponse["genres"];

            JArray seriesObject = new JArray();
            for(int i=0; i<20; i++){
                const string URL = "https://api.themoviedb.org/3/tv/popular";
                string urlParameters = "?api_key=<api_key>&language=en-US&page={i}";
                var seriesReponse = HTTP.Response.returnResponse(URL, urlParameters);
                seriesObject.Merge((JArray)seriesReponse["results"]);
            }

            using (var context = new MvcSeriesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcSeriesContext>>()))
                {
                // Look for any series.
                if (context.Series.Any())
                {
                    return;   // DB has been seeded
                } else {
                    // Loop through the series
                    foreach(var item in seriesObject.Children()){
                        var genre_id = (string)item["genre_ids"].First;
                        // Loop through the genres to find the matching id
                        foreach(var genreObj in genrejObject){
                            if((string)genreObj["id"] == genre_id){
                                var genre = (string)genreObj["name"];
                                context.Series.Add(
                                    new Series{
                                        Title = (string)item["original_name"],
                                        ReleaseDate = DateTime.Parse((string)item["first_air_date"]),
                                        Genre = genre,
                                        Rating = (int)item["vote_average"],
                                }
                        );
                            }
                        }
                    }
                }
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

            JArray actorsObject = new JArray();
            for(int i=0; i<20; i++){
                const string URL = "https://api.themoviedb.org/3/person/popular";
                string urlParameters = "?api_key=<api_key>&language=en-US&page=1";
                var seriesReponse = HTTP.Response.returnResponse(URL, urlParameters);
                actorsObject.Merge((JArray)seriesReponse["results"]);
            }

            using (var context = new MvcActorContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcActorContext>>()))
            {
                // Look for any actors.
                if (context.Actor.Any())
                {
                    return;   // DB has been seeded
                } else {
                    foreach(var item in actorsObject.Children()){
                        string[] names = ((string)item["name"]).Split(" ");
                        context.Actor.Add(
                        new Actor{
                            LastName = names[names.Length -1],
                            FirstName = String.Join(" ", names.Take(names.Length-1)),
                        }
                        );
                    }
                }
                context.SaveChanges();
            }
        }
    }
}