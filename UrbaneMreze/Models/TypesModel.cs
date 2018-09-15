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

        [Display(Name = "Entiteta")]
        [Required(ErrorMessage = "Izberite Entiteto!")]
        public Guid EntityGuid { get; set; }
        public virtual Entity Entity { get; set; }

        [Display(Name = "Bucika")]
        [Required(ErrorMessage = "Izberite Buciko!")]
        public Guid PinGuid { get; set; }
        public virtual Pin Pin { get; set; }

        [Display(Name = "Ime Tipa")]
        [Required(ErrorMessage = "Vpišite ime tipa!")]
        public String TypeName { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Datum nastanka")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Datum spremembe")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateModified { get; set; }

        [Display(Name = "Avtor")]
        public Guid UserCreatedID { get; set; }

        [Display(Name = "Avtor spremembe")]
        public Guid UserModifiedID { get; set; }
    }

    public class TypeEditViewModel
    {
        public Guid TypeGuid { get; set; }

        [Display(Name = "Entiteta")]
        [Required(ErrorMessage = "Izberite Entiteto!")]
        public Guid EntityGuid { get; set; }
        public virtual Entity Entity { get; set; }

        [Display(Name = "Bucika")]
        [Required(ErrorMessage = "Izberite Buciko!")]
        public Guid PinGuid { get; set; }
        public virtual Pin Pin { get; set; }

        [Display(Name = "Ime Tipa")]
        [Required(ErrorMessage = "Vpišite ime tipa!")]
        public String TypeName { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

    }

    public class TypesDbContext : DbContext
    {
        public DbSet<Type> Types { get; set; }

        public DbSet<Entity> Entities { get; set; }

        public DbSet<Pin> Pins { get; set; }

        public TypesDbContext() : base("UrbaneMreze") { }
    }
}