﻿using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IKira_BeyanDal : IEntityRepository<Kira_Beyan>
    {
        IEnumerable<Kira_Beyan> GetList();

        Kira_Beyan GetById(int id);

        bool Delete(int id);
    }
}