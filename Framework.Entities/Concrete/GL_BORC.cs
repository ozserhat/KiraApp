using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Concrete
{
	[Table("GL_BORC")]
	public class GL_BORC:IEntity
	{
		[Key]
		public long ID { get; set; }
		public long BEYAN_ID { get; set; }

		public string BeyanNo { get; set; }

		public int? MODUL_GRUP { get; set; }
		public long? THK_NO { get; set; }
		public int? SICIL_NO { get; set; }
		public long? MERNIS_NO { get; set; }
		public DateTime THK_TAR { get; set; }
		public DateTime VADE { get; set; }
		public string TIP { get; set; }
		public int? SIRA { get; set; }
		public string ADI { get; set; }
		public string SOYADI { get; set; }
		public decimal TUTAR { get; set; }
	
	
		public decimal? TOPLAM { get; set; }
		public string TUR_KOD { get; set; }
		public short? THK_YIL { get; set; }
		public short? THK_AY { get; set; }
		public long? KUL_NO { get; set; }
		public string KUL_ADI { get; set; }
	
		public string CADDE_ADI { get; set; }
		public string KAPI_NO { get; set; }
		public short? MAH_KOD { get; set; }
		public short? SK_KOD { get; set; }
		public short? TAK_NO { get; set; }
		public long? TARIFE_NO { get; set; }
		public string SEQ { get; set; }
		public string TECIL_EH { get; set; }
		public string MAH_ADI { get; set; }
		public string SK_ADI { get; set; }
		public bool? OdemeDurumu { get; set; }

		public bool? KdvAlinacakMi { get; set; }

		public bool? EkTahakkukMu { get; set; }

		public int BeyanYil { get; set; }

		[StringLength(2500)]
		public string ACIKLAMA { get; set; }

		[StringLength(2500)]
		public string TahakkukNotu { get; set; }

		public int? OlusturanKullanici_Id { get; set; }

		public int? GuncelleyenKullanici_Id { get; set; }

		public DateTime? OlusturulmaTarihi { get; set; }

		public DateTime? GuncellenmeTarihi { get; set; }

		public int? AktifMi { get; set; }

		
	}
}
