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
        private IKira_BeyanDal _Kira_BeyanDal;

        public Kira_BeyanManager(IKira_BeyanDal Kira_BeyanDal)
        {
            _Kira_BeyanDal = Kira_BeyanDal;
        }

        public Kira_Beyan Ekle(Kira_Beyan beyan)
        {
            return _Kira_BeyanDal.Add(beyan);
        }

        public Kira_Beyan Getir(int id)
        {
            return _Kira_BeyanDal.GetById(id);
        }

        public Kira_Beyan GetirBeyan(int BeyanId)
        {
            return _Kira_BeyanDal.GetirBeyan(BeyanId);

        }

        public IEnumerable<Kira_Beyan> GetirListe()
        {
            return _Kira_BeyanDal.GetList();
        }

        public IEnumerable<Kira_Beyan> GetirSorguListe(KiraBeyanRequest request)
        {
            return _Kira_BeyanDal.GetListByCriterias(request);
        }

        public Kira_Beyan Guncelle(Kira_Beyan beyan)
        {
            return _Kira_BeyanDal.Update(beyan);
        }

        public bool Sil(int id)
        {
            return _Kira_BeyanDal.Delete(id);
        }

        void GetAll()
        {
            _Kira_BeyanDal.GetList();
        }
    }
}
