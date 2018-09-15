using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class CarColor
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(50),Required]
        public string Color { get; set; }
        [StringLength(15), Required]
        public string Code { get; set; }

        [JsonIgnore]
        public ICollection<Ad> Ads { get; set; }
    }
}
