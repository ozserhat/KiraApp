using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Framework.Entities.Concrete
{
    [Table("GayrimenkulAlt_Turleri")]
    public class GayrimenkulAlt_Tur : IEntity
    {
        [Key]
        public int Id { get; set; }
   
        public int Gayrimenkul_Id { get; set; }

        [ForeignKey("Gayrimenkul_Id")]
        public virtual GayrimenkulTur GayrimenkulTur{ get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

    }
}
