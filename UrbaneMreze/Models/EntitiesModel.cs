using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Entity
    {
        [Key]
        public Guid EntityGuid { get; set; }

        [Required(ErrorMessage = "Izberite Tip!")]
        public Guid TypeGuid { get; set; }
        public virtual Type Type { get; set; }

        [Required(ErrorMessage = "Vpišite ime entitete!")]
        public String EntityName { get; set; }

        public String Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid UserCreatedID { get; set; }

        public Guid UserModifiedID { get; set; }
    }

    public class EntityViewModel
    {
        [Key]
        public Guid EntityGuid { get; set; }

        [Required(ErrorMessage = "Izberite Tip!")]
        public Guid TypeGuid { get; set; }

        [Required(ErrorMessage = "Vpišite ime entitete!")]
        public String EntityName { get; set; }

        public String Description { get; set; }
    }

    public class EntitiesDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public DbSet<Type> Types { get; set; }

        public EntitiesDbContext() : base("UrbaneMreze") { }
    }
}