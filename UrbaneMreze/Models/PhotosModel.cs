using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Photos
    {
        [Key]
        public Guid PhotoGuid { get; set; }

        public Guid SpotGuid { get; set; }

        public Guid AuthorGuid { get; set; }

        public String Description { get; set; }

        public Decimal Longitude { get; set; }

        public Decimal Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public String File { get; set; }

        public String Thumbnail { get; set; }

        public String ContentType { get; set; }
    }
}