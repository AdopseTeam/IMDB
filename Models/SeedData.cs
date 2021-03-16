using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcSeries.Data;
using MvcActor.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedMovies
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = 7.5M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = 8.9M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = 6
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = 3.2M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

namespace MvcSeries.Models {
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

namespace MvcActor.Models {
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
