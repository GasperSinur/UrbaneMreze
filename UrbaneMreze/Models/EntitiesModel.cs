using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class EntitiesModel
    {
        [Key]
        public Guid EntityGuid { get; set; }

        public Guid TypeGuid { get; set; }

        public String EntityName { get; set; }

        public String Description { get; set; }        
    }
}