using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }


        [Required]
        public double Price { get; set; }

        public int Capacity { get; set; }

        public int SquareMetres { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
