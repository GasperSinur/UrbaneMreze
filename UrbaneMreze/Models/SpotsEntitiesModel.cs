using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class SpotEntity
    {
        [Key]
        public Guid SpotGuid { get; set; }

        [Key]
        public Guid EntityGuid { get; set; }
    }

    public class SpotsEntitiesDbContext : DbContext
    {
        public DbSet<SpotEntity> SpotsEntities { get; set; }

        public SpotsEntitiesDbContext() : base("UrbaneMreze") { }
    }
}