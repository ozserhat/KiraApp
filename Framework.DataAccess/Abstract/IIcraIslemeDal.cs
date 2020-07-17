using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IIcraIslemeDal : IEntityRepository<Beyan_IcraIsleme>
    {
        Beyan_IcraIsleme GetById(int id);

        IEnumerable<Beyan_IcraIsleme> GetirListe(int beyanId);
        IEnumerable<Beyan_IcraIsleme> GetirListeAktif(int beyanId);

        bool Delete(int id);
    }
}
