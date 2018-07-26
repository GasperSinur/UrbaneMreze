using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Photo
    {
        [Key]
        public Guid PhotoGuid { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        [Required(ErrorMessage = "Vpišite zemljepisno dolžino!")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        [Required(ErrorMessage = "Vpišite zemljepisno širino!")]
        public Double Latitude { get; set; }

        [Display(Name = "Fotografija")]
        [Required(ErrorMessage = "Dodajte fotografijo!")]
        public byte[] File { get; set; }

        [Display(Name = "Sličica")]
        public byte[] Thumbnail { get; set; }
        
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

    public class PhotoViewModel
    {
        public Guid PhotoGuid { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        [Required(ErrorMessage = "Vpišite zemljepisno dolžino!")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        [Required(ErrorMessage = "Vpišite zemljepisno širino!")]
        public Double Latitude { get; set; }

        [Display(Name = "Fotografija")]
        [Required(ErrorMessage = "Dodajte fotografijo!")]
        public HttpPostedFileBase File { get; set; }
    }

    public class PhotoEditViewModel
    {
        public Guid PhotoGuid { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        [Required(ErrorMessage = "Vpišite zemljepisno dolžino!")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        [Required(ErrorMessage = "Vpišite zemljepisno širino!")]
        public Double Latitude { get; set; }

        [Display(Name = "Fotografija")]
        public HttpPostedFileBase File { get; set; }
    }

    public class PhotoAllViewModel
    {
        public Guid PhotoGuid { get; set; }

        [Display(Name = "Lokacija")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Zemljepisna dolžina")]
        public Double Longitude { get; set; }

        [Display(Name = "Zemljepisna širina")]
        public Double Latitude { get; set; }

        [Display(Name = "Fotografija")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Sličica")]
        public HttpPostedFileBase Thumbnail { get; set; }

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

        public class PhotosDbContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public PhotosDbContext() : base("UrbaneMreze") { }
    }
}