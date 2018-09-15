using System;
using System.Collections.Generic;
using System.Text;
using InternetProject.Models;
namespace InternetProject.ViewModels
{
    public delegate void PropertyChange();
    public class SearchViewModel
    {
        private Guid? _brand;
        private string _carName;
        private FuelType? _fuel;
        private GearboxType? _gearbox;
        private CarClassType? _carClass;
        private int? _mYearStart;
        private int? _mYearEnd;
        private int? _kMS;
        private int? _kME;
        private bool? _firstHanded;
        private bool? _havePic;
        private double? _priceS;
        private double? _priceE;
        private bool? _planned;

        public event PropertyChange OnPropertyChange;
        public Guid? Brand { get => _brand; set { if (value != _brand) OnPropertyChange?.Invoke(); _brand = value; } }
        public string CarName { get => _carName; set { if (value != _carName) OnPropertyChange?.Invoke(); _carName = value; } }
        public FuelType? Fuel { get => _fuel; set { if (value != _fuel) OnPropertyChange?.Invoke(); _fuel = value; } }
        public GearboxType? Gearbox { get => _gearbox; set { if (value != _gearbox) OnPropertyChange?.Invoke(); _gearbox = value; } }
        public CarClassType? CarClass { get => _carClass; set { if (value != _carClass) OnPropertyChange?.Invoke(); _carClass = value; } }
        public int? MYearStart { get => _mYearStart; set { if (value != _mYearStart) OnPropertyChange?.Invoke(); _mYearStart = value; } }
        public int? MYearEnd { get => _mYearEnd; set { if (value != _mYearEnd) OnPropertyChange?.Invoke(); _mYearEnd = value; } }
        public int? KMS { get => _kMS; set { if (value != _kMS) OnPropertyChange?.Invoke(); _kMS = value; } }
        public int? KME { get => _kME; set { if (value != _kME) OnPropertyChange?.Invoke(); _kME = value; } }
        public bool? FirstHanded { get => _firstHanded; set { if (value != _firstHanded) OnPropertyChange?.Invoke(); _firstHanded = value; } }
        public bool? HavePic { get => _havePic; set { if (value != _havePic) OnPropertyChange?.Invoke(); _havePic = value; } }
        public double? PriceS { get => _priceS; set { if (value != _priceS) OnPropertyChange?.Invoke(); _priceS = value; } }
        public double? PriceE { get => _priceE; set { if (value != _priceE) OnPropertyChange?.Invoke(); _priceE = value; } }
        public bool? Planned { get => _planned; set { if (value != _planned) OnPropertyChange?.Invoke(); _planned = value; } }
        public int Skip { get; set; }
    }
}
