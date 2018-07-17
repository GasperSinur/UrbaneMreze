using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class Pins
    {
        [Key]
        public Guid PinGuid { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

    }
}