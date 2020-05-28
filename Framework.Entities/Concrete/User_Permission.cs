using Framework.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    public class User_Permission : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
       
        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? YetkiliMi { get; set; }
        public bool? AktifMi { get; set; }

        public bool? GorulebilirMi { get; set; }

        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public User Users { get; set; }

        public int ControllerAction_Id { get; set; }
        [ForeignKey("ControllerAction_Id")]
        public ControllerAction ControllerActions { get; set; }
    }
}
