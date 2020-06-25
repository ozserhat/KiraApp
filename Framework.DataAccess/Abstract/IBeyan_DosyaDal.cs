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
        Beyan_Dosya DosyaEkle(Beyan_Dosya dosya);

        IEnumerable<Beyan_Dosya> GetirListe(bool? kapatmaMi);

        Beyan_Dosya GetById(int id, bool? kapatmaMi);

        IEnumerable<Beyan_Dosya> GetirBeyanId(int BeyanId, bool? kapatmaMi);

        IEnumerable<Beyan_Dosya> GetirBeyanIdFull(int BeyanId);

        Beyan_Dosya GetByGuid(Guid guid, bool? kapatmaMi);

        bool Delete(int id);
    }
}
