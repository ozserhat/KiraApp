using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IKira_BeyanDal : IEntityRepository<Beyan>
    {
        IEnumerable<Beyan> GetList();

        IEnumerable<Beyan> GetListByCriterias(KiraBeyanRequest request);
        IEnumerable<Beyan> GetListByCriteriasActive(KiraBeyanRequest request);

        IEnumerable<Beyan> GetListByCriteriasGayrimenkul(GayrimenkulBeyanRequest request);
        IEnumerable<Beyan> GetListByCriteriasSicil(SicilBeyanRequest request);

        Beyan GetirBeyan(int BeyanId);

        Beyan GetById(int id);

        bool Delete(int id);
        Beyan Getir(int beyanId, int gayrimenkulId, int kiraciId);
    }
}