using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int SquareMetres { get; set; }

        [Required]
        public string ImgageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
