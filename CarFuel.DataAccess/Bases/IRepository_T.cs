using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.DataAccess.Bases {
  public interface IRepository<T> where T : class {
    IQueryable<T> Query(Func<T, bool> criteria);

    T Add(T item);
    T Remove(T item);
    int SaveChanges();
  }
}
