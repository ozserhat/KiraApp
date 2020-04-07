using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Helpers
{
    public class DtContextWeb : DbContext
    {
        public DtContextWeb() : base("DbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<DtContextWeb>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ControllerAction> ControllerActions { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<User_Permission> User_Permissions { get; set; }
        public DbSet<Log> Logs { get; set; }
        //

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
              .HasKey(x => new { x.User_Id, x.Role_Id });

            modelBuilder.Entity<User_Permission>()
            .HasKey(x => new { x.User_Id, x.ControllerAction_Id });
        }
    }
}