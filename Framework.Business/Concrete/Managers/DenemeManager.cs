using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class DenemeManager : IDenemeService
    {
        private IDenemeDal _denemeDal;

        public DenemeManager(IDenemeDal denemeDal)
        {
            _denemeDal = denemeDal;
        }

        public List<Deneme> GetirDenemeList()
        {
            return _denemeDal.GetirDenemeList();
        }
    }
}
