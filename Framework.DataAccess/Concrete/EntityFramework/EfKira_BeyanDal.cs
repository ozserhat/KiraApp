using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Framework.Entities.Concrete;
using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfKira_BeyanDal : EfEntityRepositoryBase<Beyan, DtContext>, IKira_BeyanDal
    {
        public Beyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public Beyan Getir(int beyanId, int gayrimenkulId, int kiraciId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar.Where(gt => gt.Id == beyanId && gt.Gayrimenkul_Id == gayrimenkulId && gt.Kiraci_Id == kiraciId).FirstOrDefault();
            }
        }


        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyanlar.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyanlar.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Beyan> GetList()
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar
                              .Include(bt => bt.BeyanTur)
                              .Include(k => k.Kiracilar)
                              .Include(g => g.Gayrimenkuller)
                              .Include(ilc => ilc.Gayrimenkuller.Ilceler)
                              .Include(ilc => ilc.Gayrimenkuller.Ilceler.Iller)
                              .ToList();
            }
        }

        public IEnumerable<Beyan> GetListByCriteriasGayrimenkul(GayrimenkulBeyanRequest request)
        {
            List<Beyan> result = new List<Beyan>();

            using (DtContext context = new DtContext())
            {
                var query = context.Beyanlar
                              .Include(bt => bt.BeyanTur)
                              .Include(k => k.Kiracilar)
                              .Include(g => g.Gayrimenkuller)
                              .Include(ilc => ilc.Gayrimenkuller.Ilceler)
                              .Include(ilc => ilc.Gayrimenkuller.Ilceler.Iller)
                              .AsQueryable();

                query = request.Il_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.Sokak != null ? query.Where(x => x.Gayrimenkuller.Sokak == request.Sokak) : query;
                query = request.DisKapiNo != null ? query.Where(x => x.Gayrimenkuller.DisKapiNo == request.DisKapiNo) : query;
                query = request.IcKapiNo != null ? query.Where(x => x.Gayrimenkuller.IcKapiNo == request.IcKapiNo) : query;
                query = request.GayrimenkulAdi != null ? query.Where(x => x.Gayrimenkuller.Ad == request.GayrimenkulAdi) : query;
                query = request.AdresNo != null ? query.Where(x => x.Gayrimenkuller.AdresNo == request.AdresNo) : query;
                query = request.GayrimenkulTur.HasValue ? query.Where(x => x.Gayrimenkuller.GayrimenkulTur_Id == request.GayrimenkulTur) : query;
                query = request.AracKapasitesi.HasValue ? query.Where(x => x.Gayrimenkuller.AracKapasitesi == request.AracKapasitesi) : query;
                query = request.Metrekare.HasValue ? query.Where(x => x.Gayrimenkuller.Metrekare == request.Metrekare) : query;
                query = request.NumaratajKimlikNo.HasValue ? query.Where(x => x.Gayrimenkuller.NumaratajKimlikNo == request.NumaratajKimlikNo) : query;

            }

            return result.ToList();
        }

        public IEnumerable<Beyan> GetListByCriteriasSicil(SicilBeyanRequest request)
        {
            List<Beyan> result = new List<Beyan>();

            using (DtContext context = new DtContext())
            {
                var query = context.Beyanlar
                              .Include(bt => bt.BeyanTur)
                              .Include(k => k.Kiracilar)
                              .Include(g => g.Gayrimenkuller)
                              .Include(ilc => ilc.Gayrimenkuller.Ilceler)
                              .Include(ilc => ilc.Gayrimenkuller.Iller)
                              .AsQueryable();

                query = request.Ad != null ? query.Where(x => x.Kiracilar.Ad == request.Ad) : query;
                query = request.SoyAd != null ? query.Where(x => x.Kiracilar.Soyad == request.SoyAd) : query;
                query = request.Il_Id.HasValue ? query.Where(x => x.Kiracilar.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Kiracilar.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Kiracilar.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.SicilNo.HasValue ? query.Where(x => x.Kiracilar.SicilNo == request.SicilNo) : query;
                query = request.VergiNo.HasValue ? query.Where(x => x.Kiracilar.VergiNo == request.VergiNo) : query;
                query = request.TcKimlikNo.HasValue ? query.Where(x => x.Kiracilar.TcKimlikNo == request.TcKimlikNo) : query;
                query = request.VergiDairesi != null ? query.Where(x => x.Kiracilar.VergiDairesi == request.VergiDairesi) : query;

                result = query.ToList();
            }

            return result;
        }

        public IEnumerable<Beyan> GetListByCriterias(KiraBeyanRequest request)
        {
            List<Beyan> result = new List<Beyan>();
            List<GL_BORC> result2 = new List<GL_BORC>();

            using (DtContext context = new DtContext())
            {
                var query = context.Beyanlar
                                .Include(bt => bt.BeyanTur)
                                .Include(k => k.Kiracilar)
                                .Include(g => g.Gayrimenkuller)
                                .Include(ilc => ilc.Gayrimenkuller.Iller)
                                .AsQueryable();

                query = request.UstGayrimenkulMu == true ? query.Where(x => x.Gayrimenkuller.UstId == null) : request.UstGayrimenkulMu == false ? query.Where(x => x.Gayrimenkuller.UstId != null) : query;

                query = request.Guid.HasValue ? query.Where(x => x.Guid == request.Guid) : query;
                query = request.BeyanTur_Id.HasValue ? query.Where(x => x.BeyanTur_Id == request.BeyanTur_Id) : query;
                query = request.KiraDurum_Id.HasValue ? query.Where(x => x.KiraDurum_Id == request.KiraDurum_Id) : query;
                query = request.OdemePeriyotTur_Id.HasValue ? query.Where(x => x.OdemePeriyotTur_Id == request.OdemePeriyotTur_Id) : query;
                query = request.Gayrimenkul_Id.HasValue ? query.Where(x => x.Gayrimenkul_Id == request.Gayrimenkul_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.BaslangicTaksitNo.HasValue ? query.Where(x => x.BaslangicTaksitNo == request.BaslangicTaksitNo) : query;
                query = request.KiraTutari.HasValue ? query.Where(x => x.KiraTutari == request.KiraTutari) : query;
                query = request.IhaleTutari.HasValue ? query.Where(x => x.IhaleTutari == request.IhaleTutari) : query;
                query = request.KalanAy.HasValue ? query.Where(x => x.KalanAy == request.KalanAy) : query;
                query = request.MusadeliGunSayisi.HasValue ? query.Where(x => x.MusadeliGunSayisi == request.MusadeliGunSayisi) : query;
                query = request.SozlesmeSuresi.HasValue ? query.Where(x => x.SozlesmeSuresi == request.SozlesmeSuresi) : query;
                query = request.KullanimAlani.HasValue ? query.Where(x => x.KullanimAlani == request.KullanimAlani) : query;
                query = !string.IsNullOrEmpty(request.TeminatNo) ? query.Where(x => x.TeminatNo == request.TeminatNo) : query;
                query = request.BeyanKapatmaTarihi.HasValue ? query.Where(x => x.BeyanKapatmaTarihi == request.BeyanKapatmaTarihi) : query;
                query = request.TeminatTarihi.HasValue ? query.Where(x => x.TeminatTarihi == request.TeminatTarihi) : query;
                query = request.SozlesmeBitisTarihi.HasValue ? query.Where(x => x.SozlesmeBitisTarihi == request.SozlesmeBitisTarihi) : query;
                query = request.SozlesmeTarihi.HasValue ? query.Where(x => x.SozlesmeTarihi == request.SozlesmeTarihi) : query;
                query = request.KiraBaslangicTarihi.HasValue ? query.Where(x => x.KiraBaslangicTarihi == request.KiraBaslangicTarihi) : query;
                query = request.IhaleEncumenTarihi.HasValue ? query.Where(x => x.IhaleEncumenTarihi == request.IhaleEncumenTarihi) : query;
                query = request.BeyanTarihi.HasValue ? query.Where(x => x.BeyanTarihi == request.BeyanTarihi) : query;
                query = !string.IsNullOrEmpty(request.EncumenKararNo) ? query.Where(x => x.EncumenKararNo == request.EncumenKararNo) : query;
                query = !string.IsNullOrEmpty(request.NoterSozlesmeNo) ? query.Where(x => x.NoterSozlesmeNo == request.NoterSozlesmeNo) : query;
                query = request.BeyanNo != null ? query.Where(x => x.BeyanNo == request.BeyanNo) : query;
                query = request.Kdv.HasValue ? query.Where(x => x.Kdv == request.Kdv) : query;
                query = request.BeyanYil.HasValue ? query.Where(x => x.BeyanYil == request.BeyanYil) : query;


                //Sicil Request
                query = request.Ad != null ? query.Where(x => x.Kiracilar.Ad == request.Ad) : query;
                query = request.SoyAd != null ? query.Where(x => x.Kiracilar.Soyad == request.SoyAd) : query;
                query = request.Il_Id.HasValue ? query.Where(x => x.Kiracilar.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Kiracilar.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Kiracilar.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.SicilNo.HasValue ? query.Where(x => x.Kiracilar.SicilNo == request.SicilNo) : query;
                query = request.VergiNo.HasValue ? query.Where(x => x.Kiracilar.VergiNo == request.VergiNo) : query;
                query = request.TcKimlikNo.HasValue ? query.Where(x => x.Kiracilar.TcKimlikNo == request.TcKimlikNo) : query;
                query = request.VergiDairesi != null ? query.Where(x => x.Kiracilar.VergiDairesi == request.VergiDairesi) : query;
                //Sicil Request

                //Gayrimenkul Request
                query = request.Il_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.Sokak != null ? query.Where(x => x.Gayrimenkuller.Sokak == request.Sokak) : query;
                query = request.DisKapiNo != null ? query.Where(x => x.Gayrimenkuller.DisKapiNo == request.DisKapiNo) : query;
                query = request.IcKapiNo != null ? query.Where(x => x.Gayrimenkuller.IcKapiNo == request.IcKapiNo) : query;
                query = request.GayrimenkulAdi != null ? query.Where(x => x.Gayrimenkuller.Ad == request.GayrimenkulAdi) : query;
                query = request.GayrimenkulNo != null ? query.Where(x => x.DosyaHarfNo == request.GayrimenkulNo) : query;

                query = request.AdresNo != null ? query.Where(x => x.Gayrimenkuller.AdresNo == request.AdresNo) : query;
                query = request.GayrimenkulTur.HasValue ? query.Where(x => x.Gayrimenkuller.GayrimenkulTur_Id == request.GayrimenkulTur) : query;
                query = request.AracKapasitesi.HasValue ? query.Where(x => x.Gayrimenkuller.AracKapasitesi == request.AracKapasitesi) : query;
                query = request.Metrekare.HasValue ? query.Where(x => x.Gayrimenkuller.Metrekare == request.Metrekare) : query;
                query = request.NumaratajKimlikNo.HasValue ? query.Where(x => x.Gayrimenkuller.NumaratajKimlikNo == request.NumaratajKimlikNo) : query;

                if (request.OdemeDurumu_Id != null || request.TaksitNo != null || request.TahakkukTarihi != null || request.VadeTarihi != null || request.Tutar != null)
                {
                    using (DtContext context2 = new DtContext())
                    {
                        var query2 = context.GL_BORC.Include(by => by.Beyanlar)
                                 .Include(bt => bt.Beyanlar.BeyanTur)
                                 .Include(k => k.Beyanlar.Kiracilar)
                                 .Include(g => g.Beyanlar.Gayrimenkuller)
                                 .Include(ilc => ilc.Beyanlar.Gayrimenkuller.Iller)
                                 .AsQueryable();

                        query2 = request.TaksitNo.HasValue ? query2.Where(x => x.TAK_NO == request.TaksitNo) : query2;
                        query2 = request.TahakkukTarihi.HasValue ? query2.Where(x => x.THK_TAR == request.TahakkukTarihi) : query2;
                        query2 = request.VadeTarihi.HasValue ? query2.Where(x => x.VADE == request.VadeTarihi) : query2;
                        query2 = request.Tutar.HasValue ? query2.Where(x => x.TUTAR == request.Tutar) : query2;
                        query2 = request.Aciklama != null ? query2.Where(x => x.ACIKLAMA == request.Aciklama) : query2;
                        query2 = request.OdemeDurumu_Id.HasValue ? query2.Where(x => x.OdemeDurumu == (request.OdemeDurumu_Id.Value==1?true:false)) : query2;

                        if (query2.Count() > 0)
                        {
                            result2 = query2.ToList();
                            result = result2.Select(b => b.Beyanlar).ToList();
                        }
                    }



                }
                //Gayrimenkul Request
                result = query.ToList();
            }

            return result;
        }





        public Beyan GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                var query = context.Beyanlar
                             .Include(b => b.KiraDurum)
                             .Include(kd => kd.OdemePeriyotTur)
                             .Include(bt => bt.BeyanTur)
                             .Include(k => k.Kiracilar)
                             .Include(g => g.Gayrimenkuller)
                             .Include(ilc => ilc.Gayrimenkuller.Ilceler)
                             .Include(ilc => ilc.Gayrimenkuller.Ilceler.Iller)
                             .Where(a => a.Id == BeyanId)
                             .FirstOrDefault();
                return query;
            }
        }

        public IEnumerable<Beyan> GetListByCriteriasActive(KiraBeyanRequest request)
        {
            List<Beyan> result = new List<Beyan>();
            List<GL_BORC> result2 = new List<GL_BORC>();

            using (DtContext context = new DtContext())
            {
                var query = context.Beyanlar
                                .Include(bt => bt.BeyanTur)
                                .Include(k => k.Kiracilar)
                                .Include(g => g.Gayrimenkuller)
                                .Include(ilc => ilc.Gayrimenkuller.Ilceler)
                                .Include(ilc => ilc.Gayrimenkuller.Ilceler.Iller)
                                .Where(bt=>bt.AktifMi!=0)
                                .AsQueryable();

                query = request.UstGayrimenkulMu == true ? query.Where(x => x.Gayrimenkuller.UstId == null) : request.UstGayrimenkulMu == false ? query.Where(x => x.Gayrimenkuller.UstId != null) : query;

                query = request.Guid.HasValue ? query.Where(x => x.Guid == request.Guid) : query;
                query = request.BeyanTur_Id.HasValue ? query.Where(x => x.BeyanTur_Id == request.BeyanTur_Id) : query;
                query = request.KiraDurum_Id.HasValue ? query.Where(x => x.KiraDurum_Id == request.KiraDurum_Id) : query;
                query = request.OdemePeriyotTur_Id.HasValue ? query.Where(x => x.OdemePeriyotTur_Id == request.OdemePeriyotTur_Id) : query;
                query = request.Gayrimenkul_Id.HasValue ? query.Where(x => x.Gayrimenkul_Id == request.Gayrimenkul_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.BaslangicTaksitNo.HasValue ? query.Where(x => x.BaslangicTaksitNo == request.BaslangicTaksitNo) : query;
                query = request.KiraTutari.HasValue ? query.Where(x => x.KiraTutari == request.KiraTutari) : query;
                query = request.IhaleTutari.HasValue ? query.Where(x => x.IhaleTutari == request.IhaleTutari) : query;
                query = request.KalanAy.HasValue ? query.Where(x => x.KalanAy == request.KalanAy) : query;
                query = request.MusadeliGunSayisi.HasValue ? query.Where(x => x.MusadeliGunSayisi == request.MusadeliGunSayisi) : query;
                query = request.SozlesmeSuresi.HasValue ? query.Where(x => x.SozlesmeSuresi == request.SozlesmeSuresi) : query;
                query = request.KullanimAlani.HasValue ? query.Where(x => x.KullanimAlani == request.KullanimAlani) : query;
                query = !string.IsNullOrEmpty(request.TeminatNo) ? query.Where(x => x.TeminatNo == request.TeminatNo) : query;
                query = request.BeyanKapatmaTarihi.HasValue ? query.Where(x => x.BeyanKapatmaTarihi == request.BeyanKapatmaTarihi) : query;
                query = request.TeminatTarihi.HasValue ? query.Where(x => x.TeminatTarihi == request.TeminatTarihi) : query;
                query = request.SozlesmeBitisTarihi.HasValue ? query.Where(x => x.SozlesmeBitisTarihi == request.SozlesmeBitisTarihi) : query;
                query = request.SozlesmeTarihi.HasValue ? query.Where(x => x.SozlesmeTarihi == request.SozlesmeTarihi) : query;
                query = request.KiraBaslangicTarihi.HasValue ? query.Where(x => x.KiraBaslangicTarihi == request.KiraBaslangicTarihi) : query;
                query = request.IhaleEncumenTarihi.HasValue ? query.Where(x => x.IhaleEncumenTarihi == request.IhaleEncumenTarihi) : query;
                query = request.BeyanTarihi.HasValue ? query.Where(x => x.BeyanTarihi == request.BeyanTarihi) : query;
                query = !string.IsNullOrEmpty(request.EncumenKararNo) ? query.Where(x => x.EncumenKararNo == request.EncumenKararNo) : query;
                query = !string.IsNullOrEmpty(request.NoterSozlesmeNo) ? query.Where(x => x.NoterSozlesmeNo == request.NoterSozlesmeNo) : query;
                query = request.BeyanNo != null ? query.Where(x => x.BeyanNo == request.BeyanNo) : query;
                query = request.Kdv.HasValue ? query.Where(x => x.Kdv == request.Kdv) : query;
                query = request.BeyanYil.HasValue ? query.Where(x => x.BeyanYil == request.BeyanYil) : query;


                //Sicil Request
                query = request.Ad != null ? query.Where(x => x.Kiracilar.Ad == request.Ad) : query;
                query = request.SoyAd != null ? query.Where(x => x.Kiracilar.Soyad == request.SoyAd) : query;
                query = request.Il_Id.HasValue ? query.Where(x => x.Kiracilar.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Kiracilar.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Kiracilar.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.SicilNo.HasValue ? query.Where(x => x.Kiracilar.SicilNo == request.SicilNo) : query;
                query = request.VergiNo.HasValue ? query.Where(x => x.Kiracilar.VergiNo == request.VergiNo) : query;
                query = request.TcKimlikNo.HasValue ? query.Where(x => x.Kiracilar.TcKimlikNo == request.TcKimlikNo) : query;
                query = request.VergiDairesi != null ? query.Where(x => x.Kiracilar.VergiDairesi == request.VergiDairesi) : query;
                //Sicil Request

                //Gayrimenkul Request
                query = request.Il_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Il_Id == request.Il_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Mahalle_Id) : query;
                query = request.Sokak != null ? query.Where(x => x.Gayrimenkuller.Sokak == request.Sokak) : query;
                query = request.DisKapiNo != null ? query.Where(x => x.Gayrimenkuller.DisKapiNo == request.DisKapiNo) : query;
                query = request.IcKapiNo != null ? query.Where(x => x.Gayrimenkuller.IcKapiNo == request.IcKapiNo) : query;
                query = request.GayrimenkulAdi != null ? query.Where(x => x.Gayrimenkuller.Ad == request.GayrimenkulAdi) : query;
                query = request.GayrimenkulNo != null ? query.Where(x => x.DosyaHarfNo == request.GayrimenkulNo) : query;

                query = request.AdresNo != null ? query.Where(x => x.Gayrimenkuller.AdresNo == request.AdresNo) : query;
                query = request.GayrimenkulTur.HasValue ? query.Where(x => x.Gayrimenkuller.GayrimenkulTur_Id == request.GayrimenkulTur) : query;
                query = request.AracKapasitesi.HasValue ? query.Where(x => x.Gayrimenkuller.AracKapasitesi == request.AracKapasitesi) : query;
                query = request.Metrekare.HasValue ? query.Where(x => x.Gayrimenkuller.Metrekare == request.Metrekare) : query;
                query = request.NumaratajKimlikNo.HasValue ? query.Where(x => x.Gayrimenkuller.NumaratajKimlikNo == request.NumaratajKimlikNo) : query;

                if (request.OdemeDurumu_Id != null || request.TaksitNo != null || request.TahakkukTarihi != null || request.VadeTarihi != null || request.Tutar != null)
                {
                    using (DtContext context2 = new DtContext())
                    {
                        var query2 = context.GL_BORC.Include(by => by.Beyanlar)
                                 .Include(bt => bt.Beyanlar.BeyanTur)
                                 .Include(k => k.Beyanlar.Kiracilar)
                                 .Include(g => g.Beyanlar.Gayrimenkuller)
                                 .Include(ilc => ilc.Beyanlar.Gayrimenkuller.Iller)
                                 .AsQueryable();

                        query2 = request.TaksitNo.HasValue ? query2.Where(x => x.TAK_NO == request.TaksitNo) : query2;
                        query2 = request.TahakkukTarihi.HasValue ? query2.Where(x => x.THK_TAR == request.TahakkukTarihi) : query2;
                        query2 = request.VadeTarihi.HasValue ? query2.Where(x => x.VADE == request.VadeTarihi) : query2;
                        query2 = request.Tutar.HasValue ? query2.Where(x => x.TUTAR == request.Tutar) : query2;
                        query2 = request.Aciklama != null ? query2.Where(x => x.ACIKLAMA == request.Aciklama) : query2;
                        query2 = request.OdemeDurumu_Id.HasValue ? query2.Where(x => x.OdemeDurumu == (request.OdemeDurumu_Id.Value == 1 ? true : false)) : query2;

                        if (query2.Count() > 0)
                        {
                            result2 = query2.ToList();
                            result = result2.Select(b => b.Beyanlar).ToList();
                        }
                    }



                }
                //Gayrimenkul Request
                result = query.ToList();
            }

            return result;
        }
    }
}
