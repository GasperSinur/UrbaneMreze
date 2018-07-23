using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Pin
    {
        [Key]
        public Guid PinGuid { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Vpišite ime!")]
        public string Name { get; set; }

        [Display(Name = "Ikona")]
        [Required(ErrorMessage = "Dodajte ikono!")]
        public byte[] Icon { get; set; }

        [Display(Name = "Barva")]
        [Required(ErrorMessage = "Izberite barvo!")]
        public string Color { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

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

    public class PinViewModel
    {
        public Guid PinGuid { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Vpišite ime!")]
        public string Name { get; set; }

        [Display(Name = "Ikona")]
        [Required(ErrorMessage = "Dodajte ikono!")]
        public HttpPostedFileBase Icon { get; set; }

        [Display(Name = "Barva")]
        [Required(ErrorMessage = "Izberite barvo!")]
        public string Color { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class PinEditViewModel
    {
        public Guid PinGuid { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Vpišite ime!")]
        public string Name { get; set; }

        [Display(Name = "Ikona")]
        public HttpPostedFileBase Icon { get; set; }

        [Display(Name = "Barva")]
        [Required(ErrorMessage = "Izberite barvo!")]
        public string Color { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class PinAllViewModel
    {
        public Guid PinGuid { get; set; }

        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Ikona")]
        public HttpPostedFileBase Icon { get; set; }

        [Display(Name = "Barva")]
        public string Color { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Datum nastanka")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Datum spremembe")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Display(Name = "Avtor")]
        public Guid UserCreatedID { get; set; }

        [Display(Name = "Avtor spremembe")]
        public Guid UserModifiedID { get; set; }
    }

    public class PinsDbContext : DbContext
    {
        public DbSet<Pin> Pins { get; set; }

        public PinsDbContext() : base("UrbaneMreze") { }
    }
}