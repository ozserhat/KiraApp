using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Duyuru_Bildirimleri")]
    public class Duyuru_Bildirim : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool OkunduBilgisi { get; set; }
        
        public int Kullanici_Id { get; set; }

        [ForeignKey("Kullanici_Id")]
        public virtual User Kullanicilar { get; set; }

        public int Duyuru_Id { get; set; }

        [ForeignKey("Duyuru_Id")]
        public virtual Duyuru Duyurular { get; set; }
    }
}
