using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IKira_BeyanDal : IEntityRepository<Kira_Beyan>
    {
        IEnumerable<Kira_Beyan> GetList();

        IEnumerable<Kira_Beyan> GetListByCriterias(KiraBeyanRequest request);

        IEnumerable<Kira_Beyan> GetListByCriteriasGayrimenkul(GayrimenkulBeyanRequest request);
        IEnumerable<Kira_Beyan> GetListByCriteriasSicil(SicilBeyanRequest request);

        Kira_Beyan GetirBeyan(int BeyanId);

        Kira_Beyan GetById(int id);

        bool Delete(int id);
    }
}