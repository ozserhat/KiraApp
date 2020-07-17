using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Beyan_IcraIsleme")]

    public class Beyan_IcraIsleme:IEntity
    {
        [Key]
        public int Id { get; set; }

        public int? IcraDurum_Id { get; set; }

        public int? Sayi { get; set; }

        public DateTime? VerilisTarihi { get; set; }

        public DateTime? BaskanlikOlurTarihi { get; set; }


        [StringLength(2500)]
        public string Aciklama { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [ForeignKey("IcraDurum_Id")]
        public IcraDurum IcraDurumlari { get; set; }

        public int? Beyan_Id { get; set; }
        [ForeignKey("Beyan_Id")]
        public Beyan Beyanlar { get; set; }

    }
}
