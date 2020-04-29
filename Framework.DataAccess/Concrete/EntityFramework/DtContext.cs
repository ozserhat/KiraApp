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
        public DbSet<Gayrimenkul> Gayrimenkuller { get; set; }
        public DbSet<GayrimenkulTur> GayrimenkulTurleri { get; set; }
        public DbSet<GayrimenkulAlt_Tur> GayrimenkulAlt_Turleri { get; set; }
        public DbSet<GayrimenkulDosya_Tur> GayrimenkulDosya_Turleri { get; set; }
        public DbSet<Gayrimenkul_Dosya> Gayrimenkul_Dosyalar { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<Duyuru_Tur> Duyuru_Turleri { get; set; }
        public DbSet<Duyuru_Bildirim> Duyuru_Bildirimleri { get; set; }
        public DbSet<SistemParametreleri> SistemParametreleri { get; set; }
        public DbSet<SistemParametre_Detay> SistemParametre_Detaylari { get; set; }
        public DbSet<OdemePeriyotTur> OdemePeriyotTurleri { get; set; }
        public DbSet<KiraciTur> KiraciTurleri { get; set; }
        public DbSet<Kira_Durum> Kira_Durumlari { get; set; }
        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Kiraci> Kiracilar { get; set; }
        public DbSet<BeyanDosya_Tur> BeyanDosya_Turleri { get; set; }
        public DbSet<Beyan_Dosya> Beyan_Dosyalari { get; set; }
        public DbSet<Beyan> Beyanlar { get; set; }

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

            //modelBuilder.Entity<Duyuru_Bildirim>()
            //.HasKey(x => new { x.Kullanici_Id, x.Duyuru_Id });
        }
    }
}
