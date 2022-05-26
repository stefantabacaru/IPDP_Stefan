//using Licenta.Models;
using Microsoft.EntityFrameworkCore;
//using Responsibility = Licenta.Models.Responsibility;

namespace Licenta.Context
{
  public class ContextDb : DbContext
  {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {


        }
      //  public DbSet<User> Users { get; set; }

  }
}
