using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Type
    {
        [Key]
        public Guid TypeGuid { get; set; }

        [Required(ErrorMessage = "Vpišite ime tipa!")]
        public String TypeName { get; set; }

        public String Description { get; set; }

        [Required(ErrorMessage = "Izberite Buciko!")]
        public Guid PinGuid { get; set; }
        public virtual Pin Pin { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string UserCreatedID { get; set; }

        public string UserModifiedID { get; set; }
    }

    public class TypeViewModel
    {
        [Key]
        public Guid TypeGuid { get; set; }

        [Required(ErrorMessage = "Vpišite ime tipa!")]
        public String TypeName { get; set; }

        public String Description { get; set; }

        [Required(ErrorMessage = "Izberite Buciko!")]
        public Guid PinGuid { get; set; }
    }

    public class TypesDbContext : DbContext
    {
        public DbSet<Type> Types { get; set; }

        public DbSet<Pin> Pins { get; set; }

        public TypesDbContext() : base("UrbaneMreze") { }
    }
}