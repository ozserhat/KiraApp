using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class DtContext : DbContext
    {
        public DtContext() : base("DbContext")
        {
            this.Configuration.LazyLoadingEnabled = false; 
            Database.SetInitializer<DtContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }
        //

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
