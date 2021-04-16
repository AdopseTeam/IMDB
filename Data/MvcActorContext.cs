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

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            JArray actorsObject = new JArray();
            for(int i=1; i<20; i++){
                const string URL = "https://api.themoviedb.org/3/person/popular";
                string urlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US&page={i}";
                var seriesReponse = HTTP.Response.returnResponse(URL, urlParameters);
                actorsObject.Merge((JArray)seriesReponse["results"]);
            }
            int counter = 1;
            foreach(var item in actorsObject.Children()){
                string[] names = ((string)item["name"]).Split(" ");
                modelBuilder.Entity<Actor>().HasData(
                    new Actor{
                        ActorId= (int)item["id"],
                        Id = counter,
                        LastName = names[names.Length -1],
                        FirstName = String.Join(" ", names.Take(names.Length-1)),
                    }
                );
                counter += 1;
            }

            JArray actorDetails = new JArray();
            Timer ApiCallTimer;
           
            foreach (var item in Actor)   
            {
                string detailsURL = $"https://api.themoviedb.org/3/person/{item.ActorId}";
                string detailsUrlParameters = $"?api_key={Environment.GetEnvironmentVariable("API")}&language=en-US";
                var detailsResponse = HTTP.Response.returnResponse(detailsURL, detailsUrlParameters);
                actorDetails =(JArray)detailsResponse["details"];
                foreach (var detail in actorDetails) {
                    modelBuilder.Entity<Actor>().HasData(
                        item.Bio = (string)detail["biography"],
                        item.Birthday=(DateTime)detail["birthday"],
                        item.Profile_pic_path=(string)detail["profile_path"]
                        ) ;
                }
                SetApiTimer();   //Use timer to call API every 100ms to avoid getting blocked from it.
            }
            void SetApiTimer()
            {
                // Create a timer with a 100ms interval.
                ApiCallTimer = new Timer(100);
                ApiCallTimer.AutoReset = true;
                //ApiCallTimer.Enabled = true;
                return;
            }
        }
        public DbSet<Actor> Actor { get; set; }


    }
}