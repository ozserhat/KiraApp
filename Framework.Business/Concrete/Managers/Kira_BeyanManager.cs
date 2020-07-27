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

    public class Kira_BeyanManager : IKira_BeyanService
    {
        private IKira_BeyanDal _beyanDal;

        public Kira_BeyanManager(IKira_BeyanDal beyanDal)
        {
            _beyanDal = beyanDal;
        }

        public Beyan Ekle(Beyan beyan)
        {
            return _beyanDal.Add(beyan);
        }

        public Beyan Getir(int id)
        {
            return _beyanDal.GetById(id);
        }

        public Beyan GetirBeyan(int BeyanId)
        {
            return _beyanDal.GetirBeyan(BeyanId);

        }


       public IEnumerable<Beyan> GetirSorguListeGayrimenkul(GayrimenkulBeyanRequest request)
        {
            return _beyanDal.GetListByCriteriasGayrimenkul(request);
        }
    

        public IEnumerable<Beyan> GetirSorguListeSicil(SicilBeyanRequest request)
        {
            return _beyanDal.GetListByCriteriasSicil(request);
        }

        public IEnumerable<Beyan> GetirListe()
        {
            return _beyanDal.GetList();
        }

        public IEnumerable<Beyan> GetirSorguListe(KiraBeyanRequest request)
        {
            return _beyanDal.GetListByCriterias(request);
        }

        public IEnumerable<Beyan> GetirSorguListeAktif(KiraBeyanRequest request)
        {
            return _beyanDal.GetListByCriteriasActive(request);
        }

        public Beyan Guncelle(Beyan beyan)
        {
            return _beyanDal.Update(beyan);
        }

        public Beyan Getir(int beyanId, int gayrimenkulId, int kiraciId)
        {
            return _beyanDal.Getir(beyanId, gayrimenkulId,kiraciId);
        }
        public bool Sil(int id)
        {
            return _beyanDal.Delete(id);
        }

      
        

        void GetAll()
        {
            _beyanDal.GetList();
        }
    }
}
