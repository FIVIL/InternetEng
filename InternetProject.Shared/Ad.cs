using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class Ad
    {
        [Key]
        public Guid ID { get; set; }

        public Guid CarID { get; set; }
        public CarType Car { get; set; }


        public Guid ColorID { get; set; }
        public CarColor Color { get; set; }

        public bool FirstHanded { get; set; }

        [DataType(DataType.Date)]
        public DateTime ManufacturingDate { get; set; }

        public double KM { get; set; }


        public bool Insurance { get; set; }

        [DataType(DataType.Date)]
        public DateTime? InsuranceExpirationDate { get; set; }

        public bool TechnicalInspection { get; set; }

        public bool PlanedPayment { get; set; }

        public double Price { get; set; }

        public double? AdvancedPayment { get; set; }

        public double? InstallmentsPayment { get; set; }

        public int? InstallmentsCount { get; set; }

        public ICollection<Image> Images { get; set; }


        public Guid OwnerID { get; set; }
        public Account Owner { get; set; }

        public DateTime AdTime { get; set; }

        public bool Expired { get; set; }

        public bool Verified { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [NotMapped]
        public string Recaptcha { get; set; }
    }
}
