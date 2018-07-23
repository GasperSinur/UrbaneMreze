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

        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        public String Description { get; set; }

        [Required(ErrorMessage = "Izberite Longitude!")]
        public Double Longitude { get; set; }

        [Required(ErrorMessage = "Izberite Latitude!")]
        public Double Latitude { get; set; }

        [Required(ErrorMessage = "Izberite lokacijo slike!")]
        public byte[] File { get; set; }

        public byte[] Thumbnail { get; set; }
        
        public String ContentType { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid UserCreatedID { get; set; }

        public Guid UserModifiedID { get; set; }
    }

    public class PhotoViewModel
    {
        public Guid PhotoGuid { get; set; }

        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }

        public String Description { get; set; }

        [Required(ErrorMessage = "Izberite Longitude!")]
        public Double Longitude { get; set; }

        [Required(ErrorMessage = "Izberite Latitude!")]
        public Double Latitude { get; set; }

        [Required(ErrorMessage = "Izberite lokacijo slike!")]
        public HttpPostedFileBase File { get; set; }

        public String ContentType { get; set; }
    }

    public class PhotosDbContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public PhotosDbContext() : base("UrbaneMreze") { }
    }
}