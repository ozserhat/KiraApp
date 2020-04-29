using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
   public interface IBeyan_DosyaService
    {
        IEnumerable<Beyan_Dosya> GetirListe();
        Beyan_Dosya Getir(int id);

        Beyan_Dosya GetirGuid(Guid guid);

        Beyan_Dosya Ekle(Beyan_Dosya tur);

        Beyan_Dosya Guncelle(Beyan_Dosya tur);

        bool Sil(int id);
    }
}
