﻿
using System;
using System.Collections.Generic;
using Framework.Entities.ComplexTypes;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.Business.Abstract
{
    public interface ITahakkukService
    {
        IEnumerable<Tahakkuk> GetirListe();
        Tahakkuk Getir(int id);
        List<Tahakkuk> GetirListe(int KiraBeyanId);
        IEnumerable<Tahakkuk> GetirSorguListe(TahakkukRequest request);
        IEnumerable<Tahakkuk> GetirListeAktif();
        Tahakkuk GetById(int id);
        Tahakkuk GetByGuid(Guid guid);
        Tahakkuk GetirBeyan(int BeyanId);
        Tahakkuk GetirKiraci(int KiraciId);
        Tahakkuk GetirGayrimenkul(int GayrimenkulId);
        Tahakkuk Ekle(Tahakkuk tahakkuk);
        bool Ekle(List<Tahakkuk> entities);
        Tahakkuk Guncelle(Tahakkuk tahakkuk);
        bool Sil(int id);
        List<Tahakkuk> GetirOdemesiGecikenTahakkuklar();
        List<Tahakkuk> GetirArtisiGelenTahakkuklar();
        List<Tahakkuk> GetirOdemesiGelenTahakkuklar();
        List<Tahakkuk> GetirSozlemesiBitenBeyanlar();

    }

    public interface ITahakkukDisServis
    {
        TahakkukSorguSonucVm TahakkukSorgulama(string BorcId);

        TahakkukEkleSonucVm TahakkukOlustur(TahakkukEkleVm tahakkukBilgi);
    }
}
