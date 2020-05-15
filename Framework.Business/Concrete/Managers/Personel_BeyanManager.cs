using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class Personel_BeyanManager : IPersonel_BeyanService
    {

        private IPersonelBeyanDal _personelBeyanDal;

        public Personel_BeyanManager(IPersonelBeyanDal personelBeyanDal)
        {
            _personelBeyanDal = personelBeyanDal;
        }

        public PersonelBeyan Ekle(PersonelBeyan tur)
        {
            return _personelBeyanDal.Add(tur);

        }

        public PersonelBeyan Getir(int id)
        {
            return _personelBeyanDal.GetById(id);
        }

        public IEnumerable<PersonelBeyan> GetirListe()
        {
            return _personelBeyanDal.GetirListe();
        }
        public IEnumerable<PersonelBeyan> GetirListeAktif()
        {
            return _personelBeyanDal.GetList().Where(a => a.AktifMi == true);
        }
        public PersonelBeyan Guncelle(PersonelBeyan tur)
        {
            return _personelBeyanDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _personelBeyanDal.Delete(id);
        }
        public bool BeyanSil(int beyanId)
        {
            return _personelBeyanDal.Delete(beyanId);
        }
        
    }
}
