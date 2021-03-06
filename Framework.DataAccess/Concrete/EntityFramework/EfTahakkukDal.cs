﻿using System;
using System.Data.Entity;
using System.Linq;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using Framework.Core.DataAccess.EntityFramework;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfTahakkukDal : EfEntityRepositoryBase<Tahakkuk, DtContext>, ITahakkukDal
    {
        public bool Add(IEnumerable<Tahakkuk> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.Tahakkuklar.AddRange(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tahakkuk = context.Tahakkuklar.FirstOrDefault(i => i.Id == id);

                if (tahakkuk != null)
                {
                    context.Tahakkuklar.Remove(tahakkuk);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public Tahakkuk GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public Tahakkuk GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Tahakkuk GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani)
                       .Where(gta => gta.Kira_Beyani.Beyan_Id == BeyanId).FirstOrDefault();
            }
        }
        public IEnumerable<Tahakkuk> GetListByCriterias(TahakkukRequest request)
        {
            List<Tahakkuk> result = new List<Tahakkuk>();

            using (DtContext context = new DtContext())
            {
                var query = context.Tahakkuklar
                    .Include(kb => kb.Kira_Beyani)
                    .Include(g => g.Kira_Beyani.Gayrimenkuller)
                    .Include(g => g.Kira_Beyani.Gayrimenkuller.GayrimenkulTur)
                    .Include(b => b.Kira_Beyani.Beyanlar)
                    .Include(b => b.Kira_Beyani.Kiracilar)
                    .AsQueryable();

                query = request.BeyanNo != null ? query.Where(x => x.Kira_Beyani.Beyanlar.BeyanNo == request.BeyanNo) : query;
                query = request.BeyanYil.HasValue ? query.Where(x => x.BeyanYil == request.BeyanYil) : query;
                query = request.TaksitNo.HasValue ? query.Where(x => x.TaksitSayisi == request.TaksitNo) : query;
                query = request.TahakkukTarihi.HasValue ? query.Where(x => x.TahakkukTarihi == request.TahakkukTarihi) : query;
                query = request.VadeTarihi.HasValue ? query.Where(x => x.VadeTarihi == request.VadeTarihi) : query;
                query = request.Tutar.HasValue ? query.Where(x => x.Tutar == request.Tutar) : query;
                query = request.Aciklama != null ? query.Where(x => x.Aciklama == request.Aciklama) : query;
                query = request.OdemeDurum_Id.HasValue ? query.Where(x => x.OdemeDurumu == request.OdemeDurumu) : query;
                query = request.Gayrimenkul_Id.HasValue ? query.Where(x => x.Kira_Beyani.Gayrimenkul_Id == request.Gayrimenkul_Id) : query;

                request.OdemeDurumu = (request.OdemeDurum_Id <= 0 ? false : true);

                result = query.ToList();
            }

            return result;
        }

        public Tahakkuk GetirGayrimenkul(int GayrimenkulId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                    .Include(kb => kb.Kira_Beyani)
                    .Include(kb => kb.Kira_Beyani.Beyanlar)
                    .Include(kb => kb.Kira_Beyani.Gayrimenkuller)
                    .Where(gta => gta.Kira_Beyani.Gayrimenkul_Id == GayrimenkulId).FirstOrDefault();
            }
        }

        public Tahakkuk GetirKiraci(int KiraciId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                     .Include(kb => kb.Kira_Beyani)
                     .Include(kb => kb.Kira_Beyani.Beyanlar)
                     .Include(kb => kb.Kira_Beyani.Gayrimenkuller)
                     .Where(gta => gta.Kira_Beyani.Kiraci_Id == KiraciId).FirstOrDefault();
            }
        }

        public IEnumerable<Tahakkuk> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                var result= context.Tahakkuklar.Include(kb => kb.Kira_Beyani)
                                               .Include(b => b.Kira_Beyani.Beyanlar)
                                               .Include(k=>k.Kira_Beyani.Gayrimenkuller)
                                               .Include(ilc => ilc.Kira_Beyani.Gayrimenkuller.Ilceler)
                                               .Include(il => il.Kira_Beyani.Gayrimenkuller.Ilceler.Iller)
                                               .Include(s=>s.Kira_Beyani.Kiracilar).ToList();
                return result;
            }
        }

        public List<Tahakkuk> GetirListeBeyanId(int KiraBeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                     .Include(kb => kb.Kira_Beyani)
                     .Include(by => by.Kira_Beyani.Beyanlar)
                     .Where(a => a.KiraBeyan_Id == KiraBeyanId)
                     .OrderBy(a => a.TaksitSayisi)
                     .ToList();
            }
        }

        public IEnumerable<Tahakkuk> GetirOdenmeyenTahakkuklar()
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                     .Include(kb => kb.Kira_Beyani)
                     .Include(by => by.Kira_Beyani.Beyanlar)
                     .Where(a => a.AktifMi.Value == 1 && a.OdemeDurumu.Value == false)
                     .ToList();
            }
        }
    }
}
