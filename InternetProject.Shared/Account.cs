using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class Account
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(20), Required]
        public string UserName { get; set; }

        [StringLength(100), Required]
        public string Password { get; set; }

        [StringLength(100),Required,EmailAddress]
        public string Email { get; set; }

        [StringLength(20), Required]
        public string PhoneNumber { get; set; }

        public Guid CityID { get; set; }
        public CityName City { get; set; }

        [JsonIgnore]
        public ICollection<Ad> Ads { get; set; }


        public bool IsAdmin { get; set; }
    }
}
