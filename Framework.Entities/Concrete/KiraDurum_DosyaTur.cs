using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("KiraDurum_DosyaTurleri")]
    public class KiraDurum_DosyaTur : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BeyanDosyaTur_Id { get; set; }

        [ForeignKey("BeyanDosyaTur_Id")]
        public virtual BeyanDosya_Tur BeyanDosya_Turleri { get; set; }

        public int KiraDurum_Id { get; set; }

        [ForeignKey("KiraDurum_Id")]
        public virtual Kira_Durum Kira_Durumlari { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool AktifMi { get; set; }
    }
}
