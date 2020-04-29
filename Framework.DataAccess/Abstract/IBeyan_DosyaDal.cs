using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IBeyan_DosyaDal : IEntityRepository<Beyan_Dosya>
    {
        IEnumerable<Beyan_Dosya> GetirListe();
        Beyan_Dosya GetById(int id);
        Beyan_Dosya GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
