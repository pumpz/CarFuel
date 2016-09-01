using CarFuel.Models;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFuel.Tests.Models {
  public class FillUpTest {

    public class KmLProperty {

      [Fact]
      public void NewFillUpDontHasKML() { // inner or nester class
        // arrange
        var f1 = new FillUp();

        // act
        double? kml = f1.KmL; //KilometersPerLiteral (Nullable(double) => double?)

        // a
        Assert.Null(kml);
      }

      [Fact]
      public void TwoFillUpsCanCalculateKmL() {
        var f1 = new FillUp();
        f1.Odometer = 1000;
        f1.Liters = 40.0;
        f1.IsFull = true;

        var f2 = new FillUp();
        f2.Odometer = 2000;
        f2.Liters = 50.0;
        f2.IsFull = true;

        f1.NextFillUp = f2;

        var kml1 = f1.KmL;
        var kml2 = f2.KmL;

        Assert.Equal(20.0, kml1);
        Assert.Null(kml2);
      }

      [Fact]
      public void TwoFillUpsCanCalculateKmL_Case2() {
        var f1 = new FillUp(odometer: 2000, liters: 50.0);
        var f2 = new FillUp(odometer: 2500, liters: 20.0);

        f1.NextFillUp = f2;

        var kml1 = f1.KmL;

        Assert.Equal(25.0, kml1);
      }

      [Theory]
      [InlineData(1000, 40.0, 2000, 50.0, 20.0)]
      [InlineData(2000, 50.0, 2500, 20.0, 25.0)]
      public void TwoFillUpsCanCalculateKmL_Theory(int odo1, double liters1,
                                                    int odo2, double liters2, 
                                                    double expectedKmL) {
        var f1 = new FillUp(odo1, liters1);
        var f2 = new FillUp(odo2, liters2);
        f1.NextFillUp = f2;

        var kml1 = f1.KmL;
        var kml2 = f2.KmL;

        Assert.Equal(expectedKmL, kml1);
        Assert.Null(kml2);
      }

      [Fact]
      public void OdoMeterMustGreaterThanThePreviousFillUp() {
        var f1 = new FillUp(odometer: 50000, liters: 50.0);
        var f2 = new FillUp(odometer: 49000, liters: 60.0); // ** Invalid odo **
        f1.NextFillUp = f2;

        var ex = Assert.Throws<Exception>(() => {

          var kml = f1.KmL;

        });

        ex.Message.ShouldEqual("Odometer should be greater than the previous one.");
      }
    }
  }
}
