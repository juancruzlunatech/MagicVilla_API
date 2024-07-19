using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_VillaAPI.Data

{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }


        public DbSet<LocalUser> localUsers { get; set; }
        public DbSet<Villa>Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                { 
                Id=1,
                Name ="Royal Villa ",
                Details = "asdfasdfasdfgfdgdfg",
                ImageUrl ="",
                    Ocuppancy = 5,
                Rate = 200,
                Sqft = 500,
                Amenity = "",
                CreateDate = DateTime.Now

                },
                new Villa()
                {
                Id = 2,
                Name = "Royal Villa",
                Details = "asdfasdfasdfgfdgdfg",
                ImageUrl = "",
                    Ocuppancy = 5,
                Rate = 200,
                Sqft = 500,
                Amenity = "",
                CreateDate = DateTime.Now
                 },
                new Villa()
                {
                Id = 3,
                Name = "Mountain Retreat Cabin",
                Details = "Escape to the serene beauty of the mountains with our Retreat Cabin. Nestled in the heart of nature, this cozy cabin is perfect for those seeking a peaceful and rustic getaway.",
                ImageUrl = "",
                Ocuppancy = 4,
                Rate = 175,
                Sqft = 800,
                Amenity = "",
                CreateDate = DateTime.Now
                },
                new Villa()
                {
                Id = 4,
                Name = "Beachfront Bungalow",
                Details = "Experience the tranquility of the ocean with our Beachfront Bungalow. Enjoy stunning sunsets and direct access to the beach in this cozy and relaxing accommodation.",
                ImageUrl = "",
                Ocuppancy = 2,
                Rate = 250,
                Sqft = 700,
                Amenity = "",
                CreateDate = DateTime.Now
                }

                );
        }

    }
}
