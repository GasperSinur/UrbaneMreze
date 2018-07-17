using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class AuthorsModel
    {
        [Key]
        public Guid AuthorGuid { get; set; }

        public String Surname { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public DateTime DateCreated { get; set; }

        public String PrivateInfo { get; set; }
    }
}