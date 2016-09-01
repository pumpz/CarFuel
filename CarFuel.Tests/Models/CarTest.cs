using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Should;

namespace CarFuel.Tests.Models {
  public class CarTest {

    public class General {

      [Fact]
      public void InitialValues() {
        var c = new Car(name: "Jazz");

        c.Name.ShouldEqual("Jazz");
        c.FillUps.ShouldBeEmpty();
        //Assert.Equal("Jazz", c.Name);
        //Assert.Empty(c.FillUps);
      }
    }

    public class AddFillUpMethod {

      private readonly ITestOutputHelper _output;

      public AddFillUpMethod(ITestOutputHelper output) {
        _output = output;
      }

      [Fact]
      public void AddFirstFillUp() {
        var c = new Car(name: "Ford");
        FillUp f = c.AddFillUp(odometer: 1000, liters: 20.0);
        Assert.Equal(1, c.FillUps.Count());
        Assert.Equal(1000, f.Odometer);
        Assert.Equal(20.0, f.Liters);
      }

      [Fact]
      public void AddTwoFillUpsAndThemShouldBeChainedCorrectly() {
        //arrange
        var c = new Car(name: "Ford");

        //act
        var f1 = c.AddFillUp(1000, 50);
        var f2 = c.AddFillUp(2000, 60);
        var f3 = c.AddFillUp(2500, 20);

        Dump(c);

        f1.NextFillUp.ShouldBeSameAs(f2);
        f2.NextFillUp.ShouldBeSameAs(f3);
        //Assert.Same(f2, f1.NextFillUp);
        //Assert.Same(f3, f2.NextFillUp);
      }

      //[Fact]
      [Theory]
      //[InlineData(1000, 40.0)]
      //[MemberData("RandomFillUpData", 50)] // => call function RandomFillUpData and return IEnumerable<object[]>
      public void AddSeveralFillUps(int odometer, double liters) {
        var c = new Car("Vios");

        c.AddFillUp(odometer, liters);

        c.FillUps.Count().ShouldEqual(1);

        Assert.IsType<Car>(c);
        Assert.IsAssignableFrom<Car>(c);

      }

      public static IEnumerable<object[]> RandomFillUpData(int count) {
        var r = new Random();
        for (int i = 0; i < count; i++) {
          var odo = r.Next(0, 999999 + 1);
          var liter = r.Next(0, 9999 + 1) / 100;
          yield return new object[] { odo, liter };
        }
      }

      private void Dump(Car c) {
        _output.WriteLine("Car: {0} ", c.Name);
        foreach(var f in c.FillUps) {
          // string format
          //_output.WriteLine("{0:000000} {1:n2} L. {2:n2} Km/L.", f.Odometer, f.Liters, f.KmL);
          // string interpolation
          _output.WriteLine($"{f.Odometer:000000} {f.Liters:n2} L. {f.KmL:n2} Km/L.");
        }
      }
    }

    public class AverageKmLProperty {
      [Fact]
      public void NoFillUp_NoValue() {
        var c = new Car("Jazz");
        double? kml = c.AverageKmL;
        kml.ShouldBeNull();
      }

      [Fact]
      public void FirstFillUp_NoValue() {
        var c = new Car("Jazz");
        c.AddFillUp(odometer: 1000, liters: 20.0);

        double? kml = c.AverageKmL;
        kml.ShouldBeNull();
      }

      [Fact]
      public void SecordFillUp_SameAsKmLOfFirstFillUp() {
        var c = new Car("Jazz");
        var f1 = c.AddFillUp(odometer: 1000, liters: 40.0);
        var f2 = c.AddFillUp(odometer: 2000, liters: 50.0);

        double? kml = c.AverageKmL;
        kml.ShouldEqual(f1.KmL);
      }

      [Fact]
      public void ThirdFillUps() {
        var c = new Car("Jazz");
        var f1 = c.AddFillUp(odometer: 1000, liters: 40.0);
        var f2 = c.AddFillUp(odometer: 2000, liters: 50.0);
        var f3 = c.AddFillUp(odometer: 2500, liters: 20.0);

        double? kml = c.AverageKmL;
        kml.ShouldEqual(21.43);
      }
    }
  }
}
