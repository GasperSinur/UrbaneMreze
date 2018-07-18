using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Spot
    {
        [Key]
        public Guid SpotGuid { get; set; }

        public String SpotName { get; set; }

        public Guid AuthorGuid { get; set; }

        public String Description { get; set; }

        public Double Longitude { get; set; }

        public Double Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }

    public class SpotsDbContext : DbContext
    {
        public DbSet<Spot> Spots { get; set; }

        public SpotsDbContext() : base("UrbaneMreze") { }
    }
}