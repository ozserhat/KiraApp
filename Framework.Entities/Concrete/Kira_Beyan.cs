using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Kira_Beyanlari")]
    public class Kira_Beyan : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Kiraci_Id { get; set; }

        [ForeignKey("Kiraci_Id")]
        public virtual Kiraci Kiracilar { get; set; }

        public int Gayrimenkul_Id { get; set; }

        [ForeignKey("Gayrimenkul_Id")]
        public virtual Gayrimenkul Gayrimenkuller { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }       
    }
}
