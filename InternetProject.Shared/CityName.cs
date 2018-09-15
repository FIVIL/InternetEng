using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class CityName
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(70),Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Account> Advertisers { get; set; }
    }
}
