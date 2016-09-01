using CarFuel.DataAccess.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services.Bases {
  public abstract class ServiceBase<T> : IService<T> where T : class {

    private readonly IRepository<T> _BaseRepo;

    public ServiceBase(IRepository<T> baseRepo) {
      if (baseRepo == null) {
        throw new ArgumentNullException(nameof(baseRepo));
      }
      _BaseRepo = baseRepo;
    }

    public abstract T Find(params object[] keys);
    
    /*
      Part: public virtual T Add(T item) {
              return _BaseRepo.Add(item);
            }
    */
    public virtual T Add(T item) => _BaseRepo.Add(item);

    public virtual IQueryable<T> All() => Query(_ => true);

    public virtual IQueryable<T> Query(Func<T, bool> criteria) => _BaseRepo.Query(criteria);

    public virtual T Remove(T item) => _BaseRepo.Remove(item);

    public virtual int SaveChanges() => _BaseRepo.SaveChanges();

  }
}
