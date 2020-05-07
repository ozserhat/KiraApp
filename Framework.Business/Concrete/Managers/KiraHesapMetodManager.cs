using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class KiraHesapMetodManager
    {
        private IResmiTatillerDal _resmiTatillerDal;
        public KiraHesapMetodManager(IResmiTatillerDal resmiTatillerDal)
        {
            _resmiTatillerDal = resmiTatillerDal;
        }

        public DateTime TatilGunuKontrol(DateTime tarih)
        {
            var resmiTatil = _resmiTatillerDal.GetList();

            DateTime kontrolluTarih = tarih;
            while (kontrolluTarih.DayOfWeek != DayOfWeek.Monday)
            {
                if (kontrolluTarih.DayOfWeek == DayOfWeek.Saturday || kontrolluTarih.DayOfWeek == DayOfWeek.Sunday)
                {
                    kontrolluTarih = kontrolluTarih.AddDays(1);
                }
            }
            foreach (var item in resmiTatil)
            {
                item.Tarih = kontrolluTarih;
                kontrolluTarih = kontrolluTarih.AddDays(1);
            }
            while (kontrolluTarih.DayOfWeek != DayOfWeek.Monday)
            {
                if (kontrolluTarih.DayOfWeek == DayOfWeek.Saturday || kontrolluTarih.DayOfWeek == DayOfWeek.Sunday)
                {
                    kontrolluTarih = kontrolluTarih.AddDays(1);
                }
            }

            return kontrolluTarih;


        }
    }
}
