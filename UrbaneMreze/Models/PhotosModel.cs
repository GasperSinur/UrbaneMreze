using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Photo
    {
        [Key]
        public Guid PhotoGuid { get; set; }

        public Guid SpotGuid { get; set; }

        public Guid AuthorGuid { get; set; }

        public String Description { get; set; }

        public Double Longitude { get; set; }

        public Double Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public String FilePath { get; set; }

        public byte[] Thumbnail { get; set; }

        public String ContentType { get; set; }
    }

    public class PhotosDbContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        public PhotosDbContext() : base("UrbaneMreze") { }
    }
}