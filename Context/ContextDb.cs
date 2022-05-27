
using IPDP_Stefan.models;
using Microsoft.EntityFrameworkCore;


namespace IPDP_Stefan.Context
{
  public class ContextDb : DbContext
  {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {


        }
        public DbSet<Category> Category { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<User> User { get; set; }

    }
} 
