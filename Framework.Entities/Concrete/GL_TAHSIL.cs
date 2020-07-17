using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Concrete
{
    public class GL_TAHSIL : IEntity
	{
		public short MODUL_GRUP { get; set; }
		public decimal SICIL_NO { get; set; }
		public DateTime THK_TAR { get; set; }
		public long THK_NO { get; set; }
		public DateTime VADE { get; set; }
		public string TIP { get; set; }
		public int SIRA { get; set; }
		public short TAK_NO { get; set; }
		public string ADI { get; set; }
		public string SOYADI { get; set; }
		public decimal TUTAR { get; set; }
		public decimal KDV { get; set; }
		public short ANA { get; set; }
		public decimal TOPLAM { get; set; }
		public string TUR_KOD { get; set; }
		public short THK_YIL { get; set; }
		public short THK_AY { get; set; }
		public long MERNIS_NO { get; set; }
		public long MAKBUZ_NO { get; set; }
		public int MAKBUZ_SIRA { get; set; }
		public DateTime THS_TARIH { get; set; }
		public DateTime IPT_TARIH { get; set; }
		public short VEZNE_NO { get; set; }
		public string VEZNE_ADI { get; set; }
		public string IPT_DURUM { get; set; }
		public string POSTA_CEKNO { get; set; }
		public string NAKIT_KREDI { get; set; }
		public string NOR_GTT { get; set; }
		public decimal GECIKME { get; set; }
		public short IPT_KULNO { get; set; }
		public string IPT_KULADI { get; set; }
		public string MAK_SERI { get; set; }
		public string KAPI_NO { get; set; }
		public string CADDE_ADI { get; set; }
		public short MAH_KOD { get; set; }
		public string MAH_ADI { get; set; }
		public short SK_KOD { get; set; }
		public string SK_ADI { get; set; }
		public string EV_NO { get; set; }
		public string BABA_ADI { get; set; }
		public string ANA_ADI { get; set; }
		public string ACIKLAMA { get; set; }
		public long TARIFE_NO { get; set; }
		public string GONDEREN { get; set; }
		public string SEQ { get; set; }
		public long CEK_SIRA { get; set; }
		public string CEK_SEQ { get; set; }
		public short VB_TAKSIT { get; set; }
		public string TAHAK_EH { get; set; }
		public string ODEME_TIPI { get; set; }
		public DateTime GECIKME_TAR { get; set; }
		public decimal FAZLA_ODEME { get; set; }
		public string EMANET_EH { get; set; }
		public DateTime EMANET_TAR { get; set; }
		public string SOKAK_ACIKLAMA { get; set; }
		public long ISGAL_NO { get; set; }
		public string KISAKOD { get; set; }
		public decimal INDIRIM { get; set; }
		public decimal KUSURAT { get; set; }
		public decimal ESKI_SICIL { get; set; }
		public long NO { get; set; }
		public DateTime ISLEM_TAR { get; set; }
		public long BEYAN_ID { get; set; }
		public string BANKAREF { get; set; }
		public long BORC_ID { get; set; }
		public short KATIATIK_KODU { get; set; }
		public string DEKONT_NO { get; set; }
		public decimal TAKSIT { get; set; }
		public decimal PROVIZYON_NO { get; set; }
		public long THK_GRUP_ID { get; set; }
		public long ID { get; set; }
		public long RECID_ODEMASTER { get; set; }

	}
}
