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
        public virtual Spot Spot { get; set; }

        [Key]
        public Guid EntityGuid { get; set; }
        public virtual Entity Entity { get; set; }
    }

    public class SpotsEntitiesDbContext : DbContext
    {
        public DbSet<SpotEntity> SpotsEntities { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public DbSet<Entity> Entities { get; set; }

        public SpotsEntitiesDbContext() : base("UrbaneMreze") { }
    }
}