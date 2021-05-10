using Microsoft.EntityFrameworkCore;
using MvcActor.Models;
using MvcMovie.Models;
using MvcSeries.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MvcActor.Data
{
    public class MvcActorContext : DbContext
    {
        public MvcActorContext (DbContextOptions<MvcActorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public void seedMovies(ModelBuilder modelBuilder){
            const string GURL = "https://api.themoviedb.org/3/genre/movie/list";
            string GurlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
            var genreResponse = HTTP.Response.returnResponse(GURL, GurlParameters);
            JArray genrejObject = (JArray)genreResponse["genres"];

            JArray movieObject = new JArray();
            for(int i=1; i<10; i++){
                const string URL = "https://api.themoviedb.org/3/movie/popular";
                string urlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US&page={i}";
                var movieReponse = HTTP.Response.returnResponse(URL, urlParameters);
                movieObject.Merge((JArray)movieReponse["results"]);
            }
            int counter = 1;
            foreach(var item in movieObject.Children()){
                var genre_id = (string)item["genre_ids"].First;
                // Loop through the genres to find the matching id
                foreach(var genreObj in genrejObject){
                    if((string)genreObj["id"] == genre_id){
                        var genre = (string)genreObj["name"];
                        modelBuilder.Entity<Movies>().HasData(
                            new Movies{
                                Id = counter,
                                Title = (string)item["original_title"],
                                ReleaseDate = DateTime.Parse((string)item["release_date"]??"10/10/2010"),
                                Genre = genre,
                                Rating = (decimal)item["vote_average"],
                                Poster_path= (string)item["poster_path"],
                                Overview=(string)item["overview"]
                        }
                );
                    }
                    counter += 1;
                }
            }
        }
        public void seedSeries(ModelBuilder modelBuilder){
            const string GURL = "https://api.themoviedb.org/3/genre/tv/list";
            string GurlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
            var genreResponse = HTTP.Response.returnResponse(GURL, GurlParameters);
            JArray genrejObject = (JArray)genreResponse["genres"];

            JArray seriesObject = new JArray();
            for(int i=1; i<20; i++){
                const string URL = "https://api.themoviedb.org/3/tv/popular";
                string urlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US&page={i}";
                var seriesReponse = HTTP.Response.returnResponse(URL, urlParameters);
                seriesObject.Merge((JArray)seriesReponse["results"]);
            }
            int counter = 1;
            foreach(var item in seriesObject.Children()){
                var genre_id = (string)item["genre_ids"].First;
                // Loop through the genres to find the matching id
                foreach(var genreObj in genrejObject){
                    if((string)genreObj["id"] == genre_id){
                        var genre = (string)genreObj["name"];
                        var release = (string)item["first_air_date"];
                        if(String.IsNullOrEmpty(release)){
                            release = "10/10/2010";
                        }
                        modelBuilder.Entity<Series>().HasData(
                            new Series{
                                Id = counter,
                                Title = (string)item["original_name"],
                                ReleaseDate = DateTime.Parse(release),
                                Genre = genre,
                                Rating = (decimal)item["vote_average"],
                                Poster_path = (string)item["poster_path"],
                                Overview = (string)item["overview"]
                            }
                        );
                    }
                    counter += 1;
                }
            }
        }
        public void seedActors(ModelBuilder modelBuilder){
            JArray actorsObject = new JArray();

            string detailsUrlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
            for (int i = 1; i < 20; i++) {
                const string URL = "https://api.themoviedb.org/3/person/popular";
                string urlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US&page={i}";
                var seriesReponse = HTTP.Response.returnResponse(URL, urlParameters);
                actorsObject.Merge((JArray)seriesReponse["results"]);
            }
            int counter = 1;
            foreach (var item in actorsObject.Children()) {
                string[] names = ((string)item["name"]).Split(" ");
                Actor actor = new Actor
                {
                    ActorId = (int)item["id"],
                    Id = counter,
                    LastName = names[names.Length - 1],
                    FirstName = String.Join(" ", names.Take(names.Length - 1)),
                };
                string detailsURL = $"https://api.themoviedb.org/3/person/{actor.ActorId}";
                var detailsResponse = HTTP.Response.returnResponse(detailsURL, detailsUrlParameters);
                actor.Bio = (string)detailsResponse["biography"];
                actor.Birthday = DateTime.Parse((string)detailsResponse["birthday"] ?? "10/10/2010");
                actor.Profile_pic_path = (string)detailsResponse["profile_path"];
                modelBuilder.Entity<Actor>().HasData(actor);
                counter += 1;
            };
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            seedActors(modelBuilder);
            seedMovies(modelBuilder);
            seedSeries(modelBuilder);

        }
        public DbSet<Actor> Actor { get; set; }
    };
};