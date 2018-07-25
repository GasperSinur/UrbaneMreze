using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentGuid { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Naslov")]
        [Required(ErrorMessage = "Vpišite naslov!")]
        public String Title { get; set; }

        [Display(Name = "Besedilo")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Vpišite besedilo!")]
        public String Text { get; set; }

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

    public class CommentEditViewModel
    {
        public Guid CommentGuid { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Display(Name = "Naslov")]
        [Required(ErrorMessage = "Vpišite naslov!")]
        public String Title { get; set; }

        [Display(Name = "Besedilo")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Vpišite besedilo!")]
        public String Text { get; set; }
    }

    public class CommentsDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public CommentsDbContext() : base("UrbaneMreze") { }
    }
}