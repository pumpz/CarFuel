using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarFuel.DataAccess.Contexts {
  public class CarFuelDb : DbContext {

    public DbSet<FillUp> FillUps { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
  }
}