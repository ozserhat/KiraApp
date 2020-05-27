using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;


namespace Framework.Business.Concrete.Managers
{
    public class Gayrimenkul_DosyaManager : IGayrimenkul_DosyaService
    {
        private IGayrimenkul_DosyaDal _gayrimenkuldosyaDal;

        public Gayrimenkul_DosyaManager(IGayrimenkul_DosyaDal gayrimenkuldosyaDal)
        {
            _gayrimenkuldosyaDal = gayrimenkuldosyaDal;
        }

        public Gayrimenkul_Dosya Ekle(Gayrimenkul_Dosya tur)
        {
            return _gayrimenkuldosyaDal.Add(tur);
        }

        public Gayrimenkul_Dosya Getir(int id)
        {
            return _gayrimenkuldosyaDal.GetById(id);
        }

        public IEnumerable<Gayrimenkul_Dosya> GetirGayrimenkulId(int GayrimenkulId)
        {
            return _gayrimenkuldosyaDal.GetirGayrimenkulId(GayrimenkulId);
        }

        public Gayrimenkul_Dosya GetirGuid(Guid guid)
        {
            return _gayrimenkuldosyaDal.GetByGuid(guid);
        }

        public IEnumerable<Gayrimenkul_Dosya> GetirListe()
        {
            return _gayrimenkuldosyaDal.GetirListe();
        }

        public IEnumerable<Gayrimenkul_Dosya> GetirListeAktif()
        {
            return _gayrimenkuldosyaDal.GetirListe().Where(a => a.AktifMi == true);
        }

        public Gayrimenkul_Dosya Guncelle(Gayrimenkul_Dosya tur)
        {
            return _gayrimenkuldosyaDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _gayrimenkuldosyaDal.Delete(id);
        }
    }
}
