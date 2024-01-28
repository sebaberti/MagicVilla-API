using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string  Detail { get; set; }

        [Required]
        public double Price { get; set; }

        public int Capacity { get; set; }

        public int SquareMetres { get; set; }

        public string ImgageUrl { get; set; }

        public  string Amenity { get; set; }

        public DateTime CreationDate  { get; set; }

        public  DateTime UpdateDate { get; set; }


    }
}
