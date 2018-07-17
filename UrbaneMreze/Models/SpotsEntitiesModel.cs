using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class SpotsEntities
    {
        [Key]
        public Guid SpotGuid { get; set; }

        [Key]
        public Guid EntityGuid { get; set; }
    }

    public class SpotsEntitiesDbContext : DbContext
    {
        public DbSet<SpotsEntities> SpotsEntities { get; set; }

        public SpotsEntitiesDbContext() : base("UrbaneMreze") { }
    }
}