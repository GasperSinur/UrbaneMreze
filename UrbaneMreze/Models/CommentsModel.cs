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

        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }
        public virtual Spot Spot { get; set; }

        [Required(ErrorMessage = "Vpišite naslov!")]
        public String Title { get; set; }

        [Required(ErrorMessage = "Vpišite besedilo!")]
        public String Text { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid UserCreatedID { get; set; }

        public Guid UserModifiedID { get; set; }
    }

    public class CommentViewModel
    {
        [Key]
        public Guid CommentGuid { get; set; }

        [Required(ErrorMessage = "Izberite Lokacijo!")]
        public Guid SpotGuid { get; set; }

        [Required(ErrorMessage = "Vpišite naslov!")]
        public String Title { get; set; }

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