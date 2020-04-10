using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Framework.Entities.Concrete;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<ControllerAction> ControllerActions { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<User_Permission> User_Permissions { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Deneme> Denemeler { get; set; }
        public DbSet<GayrimenkulTur> GayrimenkulTurleri { get; set; }
        public DbSet<GayrimenkulAlt_Tur> GayrimenkulAlt_Turleri { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<User_Role>()
            //            .HasKey(t => new { t.RoleId, t.UserId });

            // modelBuilder.Entity<User_Role>()
            //.HasRequired<User>(s => s.Users)
            //.WithMany(s => s.User_Roles)
            //.HasForeignKey(s => s.UserId);

            //var userfg = modelBuilder.Entity<User>();
            //userfg.ToTable("User", "dbo");
            //userfg.HasMany<Role>(s => s.Roles)
            //   .WithMany(c => c.Users)
            //   .Map(cs =>
            //   {
            //       cs.MapLeftKey("Role_Id");
            //       cs.MapRightKey("User_Id");
            //       cs.ToTable("User_Roles");
            //   });

            modelBuilder.Entity<User_Role>()
              .HasKey(x => new { x.User_Id, x.Role_Id });

            modelBuilder.Entity<User_Permission>()
            .HasKey(x => new { x.User_Id, x.ControllerAction_Id });
        }
    }
}
