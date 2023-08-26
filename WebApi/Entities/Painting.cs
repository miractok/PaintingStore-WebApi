using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Painting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? Name { get; set; }
        public int ArtistId { get; set; }
        public DateTime PublishDate { get; set; }
        public Artist? Artist { get; set; }
        public int StyleId { get; set; }
        public Style? Style { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public string? Subject { get; set; }
        public int MediumId { get; set; }
        public Medium? Medium { get; set; }
        public int Price { get; set; }

    }
}