using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class Duyuru_BildirimManager : IDuyuru_BildirimService
    {
        private IDuyuru_BildirimDal _duyuru_BildirimDal;
        public Duyuru_BildirimManager(IDuyuru_BildirimDal duyuru_BildirimDal)
        {
            _duyuru_BildirimDal = duyuru_BildirimDal;
        }

        public Duyuru_Bildirim Ekle(Duyuru_Bildirim tur)
        {
            return _duyuru_BildirimDal.Add(tur);
        }

        public bool Ekle(IEnumerable<Duyuru_Bildirim> entities)
        {
            return _duyuru_BildirimDal.Add(entities);
        }

        public Duyuru_Bildirim Getir(int id)
        {
            return _duyuru_BildirimDal.GetById(id);
        }

        public IEnumerable<Duyuru_Bildirim> GetirKullaniciMesajlari(int KullaniciId)
        {
            return _duyuru_BildirimDal.GetirKullaniciMesajlari(KullaniciId).ToList();
        }

        public IEnumerable<Duyuru_Bildirim> GetirListe()
        {
            return _duyuru_BildirimDal.GetList();
        }

        public Duyuru_Bildirim Guncelle(Duyuru_Bildirim tur)
        {
            return _duyuru_BildirimDal.Update(tur);
        }

        public int OkunmamisMesajSayisi(int KullaniciId)
        {
            return _duyuru_BildirimDal.GetList(a=>a.Kullanici_Id== KullaniciId && a.OkunduBilgisi==false).Count();
        }

        public bool Sil(int id)
        {
            return _duyuru_BildirimDal.Delete(id);
        }
    }
}
