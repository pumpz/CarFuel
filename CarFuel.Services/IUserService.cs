using CarFuel.Models;
using CarFuel.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services {

  public interface IUserService : IService<User> {

    User CurrentUser { get; set; }
  }
}
