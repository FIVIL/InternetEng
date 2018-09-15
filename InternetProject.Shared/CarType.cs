using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public enum FuelType
    {
        Petrol,
        Gasoline,
        Gas,
        Hybride,
        Electricity
    }
    public enum GearboxType
    {
        Automatic,
        Manual
    }
    public enum CarClassType
    {
        Sedans,
        Coupe,
        Hback,
        Crook,
        StationWagon,
        SUV,
        SUV4WD,
        Sport,
        Luxary,
        MiniVan,
        CargoVan,
        PickupTruck,
        CrossOver
    }
    public class CarType
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(70),Required]
        public string CarName { get; set; }

        public Guid BrandID { get; set; }
        public CarBrand Brand { get; set; }

        public FuelType Fuel { get; set; }

        public GearboxType Gearbox { get; set; }

        public CarClassType CarClass { get; set; }


        [JsonIgnore]
        public ICollection<Ad> Ads { get; set; }

        public override string ToString()
        {
            return CarName + ";" + Fuel.ToString() + ";" + Gearbox.ToString();
        }
    }
}
