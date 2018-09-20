using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Spot
    {
        [Key]
        public Guid SpotGuid { get; set; }

        [Display(Name = "Ime Lokacije")]
        [Required(ErrorMessage = "Vpišite ime Lokacije!")]
        public String SpotName { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        [Required(ErrorMessage = "Vpišite zemljepisno dolžino!")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        [Required(ErrorMessage = "Vpišite zemljepisno širino!")]
        public Double Latitude { get; set; }

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

        [NotMapped]
        public String SpotAuthorUsername { get; set; }

        [NotMapped]
        public Guid TypeGuid { get; set; }
    }

    public class SpotEditViewModel
    {
        public Guid SpotGuid { get; set; }

        [Display(Name = "Ime Lokacije")]
        [Required(ErrorMessage = "Vpišite ime Lokacije!")]
        public String SpotName { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        [Required(ErrorMessage = "Vpišite zemljepisno dolžino!")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        [Required(ErrorMessage = "Vpišite zemljepisno širino!")]
        public Double Latitude { get; set; }

        [NotMapped]
        public Guid TypeGuid { get; set; }
    }

    public class SpotLight
    {
        public Guid SpotGuid { get; set; }

        [NotMapped]
        public string Style { get; set; }
    }

    public class SpotsDbContext : DbContext
    {
        public DbSet<Spot> Spots { get; set; }

        public SpotsDbContext() : base("UrbaneMreze") { }
    }
}