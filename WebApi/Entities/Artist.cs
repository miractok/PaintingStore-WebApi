using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Artist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Country { get; set; }
        public virtual ICollection<ArtistPainting>? ArtistPainting { get; set; }
    }
}