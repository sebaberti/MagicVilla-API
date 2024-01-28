using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaCreateDto
    {
        

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }


        [Required]
        public double Price { get; set; }

        public int Capacity { get; set; }

        public int SquareMetres { get; set; }

        public string ImgageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
