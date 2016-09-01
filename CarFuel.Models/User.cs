using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Models {
  [Table("tblUser")]
  public class User {

    public User() {
      Cars = new HashSet<Car>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid UserId { get; set; }

    [Required]
    public string DisplayName { get; set; }

    public ConsumptionRateUnit ConsumptionRate { get; set; }

    public virtual ICollection<Car> Cars { get; set; }
  }
}
