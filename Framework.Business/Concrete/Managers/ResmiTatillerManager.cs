﻿using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class ResmiTatillerManager : IResmiTatillerService
    {
        private IResmiTatillerDal _resmiTatilDal;

        public ResmiTatillerManager(IResmiTatillerDal resmiTatilDal)
        {
            _resmiTatilDal = resmiTatilDal;
        }

        public ResmiTatiller Ekle(ResmiTatiller tur)
        {
            return _resmiTatilDal.Add(tur);
        }

        public ResmiTatiller Getir(int id)
        {
            return _resmiTatilDal.GetById(id);
        } 

        public IEnumerable<ResmiTatiller> GetirListe()
        {
            return _resmiTatilDal.GetList();
        }

        public ResmiTatiller Guncelle(ResmiTatiller tur)
        {
            return _resmiTatilDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _resmiTatilDal.Delete(id);
        }

        void GetAll()
        {
            _resmiTatilDal.GetList();
        }
    }
}