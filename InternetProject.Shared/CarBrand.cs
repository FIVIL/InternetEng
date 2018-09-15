using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class CarBrand
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(50),Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [StringLength(50),Required]
        public string ImgPath { get; set; }

        [JsonIgnore]
        public ICollection<CarType> Cars { get; set; }
    }
}
