using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PaintingStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<PaintingStoreDbContext>>()))
            {
                if(context.Paintings.Any())
                {
                    return;
                }

                context.Paintings.AddRange(
                    new Painting
                    {
                        Name = "Travel of Poseidon by Sea",
                        ArtistId = 1,
                        GenreId = 1,
                        StyleId = 1,
                        MediumId = 1,
                        Subject = "A greek god travelling by sea.",
                        Price = 169
                    },
                    new Painting
                    {
                        Name = "TestPainting",
                        ArtistId = 2,
                        GenreId = 2,
                        StyleId = 2,
                        MediumId = 2,
                        Subject = "TestSubject",
                        Price = 120
                    },
                    new Painting
                    {
                        Name = "TestPainting1",
                        ArtistId = 3,
                        GenreId = 3,
                        StyleId = 3,
                        MediumId = 3,
                        Subject = "TestSubject1",
                        Price = 121
                    }
                );

                context.Artists.AddRange(
                    new Artist
                    {
                        NameSurname = "Ivan Aivazovski",
                        Country = "Ukraine"
                    },
                    new Artist
                    {
                        NameSurname = "TestArtist",
                        Country = "TestCountry"
                    },
                    new Artist
                    {
                        NameSurname = "TestArtist1",
                        Country = "TestCountry1"
                    }
                );

                context.Styles.AddRange(
                    new Style
                    {
                        Name = "Romanticism"
                    },
                    new Style
                    {
                        Name = "TestName"
                    },
                    new Style
                    {
                        Name = "TestName1"
                    }
                );

                context.Mediums.AddRange(
                    new Medium
                    {
                        Name = "Oil"
                    },
                    new Medium
                    {
                        Name = "TestName"
                    },
                    new Medium
                    {
                        Name = "TestName1"
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Mythological Painting"
                    },
                    new Genre
                    {
                        Name = "TestName"
                    },
                    new Genre
                    {
                        Name = "TestName1"
                    }
                );

                context.ArtistPaintings.AddRange(
                    new ArtistPainting
                    {
                        ArtistId = 1,
                        PaintingId = 1
                    },
                    new ArtistPainting
                    {
                        ArtistId = 2,
                        PaintingId = 2
                    },
                    new ArtistPainting
                    {
                        ArtistId = 3,
                        PaintingId = 3
                    }
                );

                context.SaveChanges();
            }
        }
    }
}