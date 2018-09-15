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

        [Display(Name = "Ime Entitete")]
        [Required(ErrorMessage = "Vpišite ime entitete!")]
        public String EntityName { get; set; }

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

    public class EntityEditViewModel
    {
        public Guid EntityGuid { get; set; }

        [Display(Name = "Ime Entitete")]
        [Required(ErrorMessage = "Vpišite ime entitete!")]
        public String EntityName { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }
    }

        public class EntitiesDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public EntitiesDbContext() : base("UrbaneMreze") { }
    }
}