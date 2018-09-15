using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class Image
    {
        [Key]
        public Guid ID { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; }

        public Guid? AdID { get; set; }
        [JsonIgnore]
        public Ad Ad { get; set; }

        [NotMapped]
        public string Value { get; set; }
    }
}
