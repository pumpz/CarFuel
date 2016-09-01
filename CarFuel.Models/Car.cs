using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarFuel.Models {
  [Table("tblCar")]
  public class Car {

    public Car() : this("Car"){
      // new parameter.
    }

    public Car(string name) {
      FillUps = new HashSet<FillUp>();
      Name = name;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    //[Key]
    //public int id { get; set; }

    [Required]
    [Display(Name="Car's name")]
    public string Name { get; set; }

    [Required]
    public virtual User Owner { get; set; }

    public virtual ICollection<FillUp> FillUps { get; set; } //= new HashSet<FillUp>();

    public double? AverageKmL {
      get {
        if (FillUps?.Count <= 1)
          return null;

        if (FillUps?.Count <= 2)
          return FillUps.First().KmL;

        var first = FillUps.FirstOrDefault();
        var last = FillUps.LastOrDefault();
        var sumLiters = FillUps.Sum(f => f.Liters) - first.Liters;
        var kml = (last.Odometer - first.Odometer) / sumLiters;

        Console.WriteLine("ToEven: " + Math.Round(1.355, 2, MidpointRounding.ToEven));
        Console.WriteLine("ToEven: " + Math.Round(1.365, 2, MidpointRounding.ToEven));
        Console.WriteLine("AwayFromZero: " + Math.Round(1.365, 2, MidpointRounding.AwayFromZero));
        return Math.Round(kml, 2, MidpointRounding.AwayFromZero);
      }
    }

    public FillUp AddFillUpNoNextFillUp(int odometer, double liters) {
      FillUp f = new FillUp(odometer, liters);
      //this.FillUps.Add(f);
      FillUps.Add(f);
      return f;
    }

    public FillUp AddFillUp(int odometer, double liters) {
      FillUp f = new FillUp(odometer, liters);
      if (FillUps?.Count > 0) {
        FillUps.LastOrDefault().NextFillUp = f;
      }
      FillUps.Add(f);
      return f;
    }
  }
}