using Framework.Entities.ComplexTypes;

namespace Framework.Business.Abstract
{
    public interface ITahakkukService
    {
        TahakkukSorguSonucVm TahakkukSorgulama(string BorcId);

        TahakkukEkleSonucVm TahakkukOlustur(TahakkukEkleVm tahakkukBilgi);
    }
}
