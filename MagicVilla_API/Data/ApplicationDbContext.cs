using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
        public DbSet<Villa> Villas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Detail = "Increible",
                    ImgageUrl = "",
                    Capacity = 8,
                    SquareMetres = 90,
                    Price=200,
                    Amenity= "",
                    CreationDate = DateTime.Now,
                    UpdateDate= DateTime.Now,

                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Villa Olimpica",
                     Detail = "Con icreible vista a la gran piscina",
                     ImgageUrl = "",
                     Capacity = 6,
                     SquareMetres = 70,
                     Price = 100,
                     Amenity = "",
                     CreationDate = DateTime.Now,
                     UpdateDate = DateTime.Now,

                 }

                ) ;
        }
    }
}
