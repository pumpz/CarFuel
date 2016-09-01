using CarFuel.DataAccess.Bases;
using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CarFuel.DataAccess {
  public class CarRepository : RepositoryBase<Car> {
    public CarRepository(DbContext context) : base(context) {
      // do something
    }
  }
}
