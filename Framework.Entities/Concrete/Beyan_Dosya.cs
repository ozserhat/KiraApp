using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Framework.Entities.Concrete
{
    [Table("Beyan_Dosyalar")]
    public class Beyan_Dosya : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int Beyan_Id { get; set; }

        [ForeignKey("Beyan_Id")]
        public virtual Beyan Beyanlar { get; set; }

        public int BeyanDosya_Tur_Id { get; set; }

        [ForeignKey("BeyanDosya_Tur_Id")]
        public virtual BeyanDosya_Tur BeyanDosyaTurleri { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [StringLength(1500)]
        public string Aciklama { get; set; }
    }
}
