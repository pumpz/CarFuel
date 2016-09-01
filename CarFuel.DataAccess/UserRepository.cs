using CarFuel.DataAccess.Bases;
using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.DataAccess {
  public class UserRepository : RepositoryBase<User> {
    public UserRepository(DbContext context) : base(context) {

    }
  }
}
