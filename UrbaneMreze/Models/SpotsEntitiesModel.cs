using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class SpotsEntitiesModel
    {
        [Key]
        public Guid SpotGuid { get; set; }

        public Guid EntityGuid { get; set; }
    }
}