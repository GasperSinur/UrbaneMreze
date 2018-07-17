using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Types
    {
        [Key]
        public Guid TypeGuid { get; set; }

        public String TypeName { get; set; }

        public String Description { get; set; }

        public Guid PinGuid { get; set; }
    }

    public class TypesDbContext : DbContext
    {
        public DbSet<Types> Types { get; set; }

        public TypesDbContext() : base("UrbaneMreze") { }
    }
}