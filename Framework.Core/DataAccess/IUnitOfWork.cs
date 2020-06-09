using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DataAccess
{
    public interface IUnitOfWork :  IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();

    }
}
