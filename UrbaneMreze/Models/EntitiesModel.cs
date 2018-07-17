using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Entities
    {
        [Key]
        public Guid EntityGuid { get; set; }

        public Guid TypeGuid { get; set; }

        public String EntityName { get; set; }

        public String Description { get; set; }        
    }

    public class EntitiesDbContext : DbContext
    {
        public DbSet<Entities> Entities { get; set; }

        public EntitiesDbContext() : base("UrbaneMreze") { }
    }
}