using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrbaneMreze.Models
{
    public class TypesModel
    {
        [Key]
        public Guid TypeGuid { get; set; }

        public String TypeName { get; set; }

        public String Description { get; set; }

        public Guid PinGuid { get; set; }

    }
}