using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class SpotType
    {
        [Key]
        public Guid SpotTypeGuid { get; set; }

        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }
        
        public Guid TypeGuid { get; set; }
        public virtual Type Type { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }

    public class SpotsTypesDbContext : DbContext
    {
        public DbSet<SpotType> SpotsTypes { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public DbSet<Type> Types { get; set; }

        public SpotsTypesDbContext() : base("UrbaneMreze") { }
    }
}