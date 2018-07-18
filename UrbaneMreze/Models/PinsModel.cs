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

        [Required(ErrorMessage = "Vpišite ime!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dodajte ikono!")]
        public byte[] Icon { get; set; }

        [Required(ErrorMessage = "Izberite barvo!")]
        public string Color { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string UserCreatedID { get; set; }

        public string UserModifiedID { get; set; }
    }

    public class PinViewModel
    {
        [Key]
        public Guid PinGuid { get; set; }

        [Required(ErrorMessage = "Vpišite ime!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dodajte ikono!")]
        public HttpPostedFileBase Icon { get; set; }

        [Required(ErrorMessage = "Izberite barvo!")]
        public string Color { get; set; }

        public string Description { get; set; }
    }

    public class PinsDbContext : DbContext
    {
        public DbSet<Pin> Pins { get; set; }

        public PinsDbContext() : base("UrbaneMreze") { }
    }
}