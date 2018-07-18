﻿using System;
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

        public Guid SpotGuid { get; set; }

        public Guid AuthorGuid { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }

    public class CommentsDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentsDbContext() : base("UrbaneMreze") { }
    }
}