using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Newtonsoft.Json.Linq;
using System;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
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
            for(int i=1; i<50; i++){
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
                        var date = (string)item["release_date"];
                        if(date == null || date == ""){
                            date = "10/10/2010";
                        }
                        string MId = (string)item["id"];
                        string MURL = $"https://api.themoviedb.org/3/movie/{MId}/credits";
                        string MurlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
                        var mResponse = HTTP.Response.returnResponse(MURL, MurlParameters);
                        JArray mjObject = (JArray)mResponse["cast"];
                        var cast = "";
                        try{
                            foreach(var actor in mjObject){
                                cast = cast + (string)actor["id"] + ",";
                            }
                        } catch {}
                        modelBuilder.Entity<Movies>().HasData(
                            new Movies{
                                Id = counter,
                                Title = (string)item["original_title"],
                                ReleaseDate = DateTime.Parse(date),
                                Genre = genre,
                                Rating = (decimal)item["vote_average"],
                                Poster_path= (string)item["poster_path"],
                                Overview=(string)item["overview"],
                                Cast = cast
                            }
                        );
                         modelBuilder.Entity<MovieComment>().HasData(
                            new MovieComment {
                                Id = counter,
                                MId = counter,
                                Creator = "Developers",
                                Text = "This is a sample text for " + (string)item["original_name"]
                            }
                        );
                    }
                    counter += 1;
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            seedMovies(builder);
        }
        public DbSet<Watchlist> Watchlist { get; set; }
        public DbSet<MovieComment> MComments { get; set; }
        public DbSet<Movies> Movies { get; set; }

    }
}