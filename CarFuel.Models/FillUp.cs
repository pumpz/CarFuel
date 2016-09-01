using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFuel.Models {
  [Table("tblFillUp")]
  public class FillUp {
    public FillUp() {
    }

    public FillUp(int odometer, double liters, bool isFull = true) {
      Odometer = odometer;
      Liters = liters;
      IsFull = isFull;
    }

    [Key]
    public int Id { get; set; }

    [Column("IS_FULL")]
    public bool IsFull { get; set; }

    [Range(0.0, 100.0)]
    public double Liters { get; set; }

    // Navigation Properties
    /* Makes it "virtual" to enable lazy-loading.
     Loading:
      Lazy load (if use load)
      Eagerly Load (.Include) (Auto Load)
      Manual Load */
    public FillUp NextFillUp { get; set; }

    [Range(0, 999999)]
    public int Odometer { get; set; }

    public double? KmL {
      get {
        if (NextFillUp == null)
          return null;

        if (Odometer > NextFillUp.Odometer)
          throw new Exception("Odometer should be greater than the previous one.");

        return (NextFillUp.Odometer - Odometer) / NextFillUp.Liters;

      }
    }
  }
}
 
 