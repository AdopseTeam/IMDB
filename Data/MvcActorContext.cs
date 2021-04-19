using Microsoft.EntityFrameworkCore;
using MvcActor.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Timers;

namespace MvcActor.Data
{
    public class MvcActorContext : DbContext
    {
        public MvcActorContext (DbContextOptions<MvcActorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            JArray actorsObject = new JArray();

            string detailsUrlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
            for (int i = 1; i < 20; i++) {
                const string URL = "https://api.themoviedb.org/3/person/popular";
                string urlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}language=en-US&page={i}";
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
        public DbSet<Actor> Actor { get; set; }
    };
};