using CarFuel.Models;
using CarFuel.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services {
  public interface ICarService : IService<Car> {
    //IQueryable<Car> AllCarsForUser(Guid userId);
  }
}
