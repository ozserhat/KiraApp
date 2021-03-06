﻿using Framework.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Models.ComplexType
{
    public class BeyanSelectListsVm
    {
        #region Constructor
        private IKira_DurumService _kiraDurumService;
        private IOdemePeriyotTurService _odemePeriyotService;
        private IBeyan_TurService _beyanTurService;
        private IGayrimenkulTurService _gayrimenkulTurService;
        private IIlService _ilService;
        private IIlceService _ilceService;
        private IBeyan_UfeOranService _ufeOranService;
        private IGayrimenkulService _gayrimenkulservice;
        private ISistemParametre_DetayService _parametreService;
        public  IIcraDurumService _icraDurumService;

        public BeyanSelectListsVm(IGayrimenkulService gayrimenkulservice,
            ISistemParametre_DetayService parametreService,
            IKira_DurumService kiraDurumService,
            IOdemePeriyotTurService odemePeriyotService,
        IBeyan_TurService beyanTurService,
        IIlService ilService,
        IIlceService ilceService,
        IGayrimenkulTurService gayrimenkulTurService, 
        IIcraDurumService icraDurumService,
        IBeyan_UfeOranService ufeOranService)
        {
            _gayrimenkulservice = gayrimenkulservice;
            _beyanTurService = beyanTurService;
            _parametreService = parametreService;
            _odemePeriyotService = odemePeriyotService;
            _kiraDurumService = kiraDurumService;
            _ilService = ilService;
            _ilceService = ilceService;
            _gayrimenkulTurService = gayrimenkulTurService;
            _ufeOranService = ufeOranService;
            _icraDurumService = icraDurumService;
        }
        #endregion

        #region SelectLists

        public SelectList IlSelectList()
        {
            var iller = _ilService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }

        public SelectList GayrimenkulTuruSelectList()
        {
            var gayrimenkulTuru = _gayrimenkulTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(gayrimenkulTuru, "Id", "Ad");
        }

        public SelectList GayrimenkulSelectList()
        {
            var iller = _gayrimenkulservice.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList BeyanTurSelectList()
        {
            var turler = _beyanTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList BeyanYilSelectList()
        {
            var yillar = _parametreService.GetirListe(1).Where(a => a.AktifMi.Value).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(yillar, "Id", "Ad");
        }

        public SelectList KdvOraniSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList DamgaVergisiDurumSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Evet",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Hayır",
                                    Value="0"
                                  }
            };

            return new SelectList(newList, "Value", "Text");
        }

        public SelectList KiraDurumFullSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList KiraDurumSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Where(a => a.Id == 2).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad",2);
        }

        public SelectList EkTahakkukOranlariSelectList()
        {
            var iller = _parametreService.GetirListe(11).Select(x => new { Id = x.Id, Ad = x.Ad }).OrderByDescending(a => a.Id).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OdemePeriyotSelectList()
        {
            var iller = _odemePeriyotService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OtoparkTatilGunuSelectList()
        {
            var tatilgun = _parametreService.GetirListe(10).Select(x => new { Id = x.Deger, Ad = x.Ad }).ToList();

            return new SelectList(tatilgun, "Id", "Ad");
        }

        public SelectList UfeOranSelectList()
        {
            return new SelectList("Id", "Ad");
        }

        public SelectList UfeOranSelectList(int turId)
        {
            var oran = _ufeOranService.GetirListeAktif().Where(a => a.ArtisTuru_Id == turId).Select(x => new { x.Id, Ad = x.Ad + " (%" + x.Oran + ")" }).ToList();

            return new SelectList(oran, "Id", "Ad");
        }

        public SelectList ArtisAlTurSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Tam Al",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Fark Al",
                                    Value="2"
                                  }
            };

            return new SelectList(newList, "Value", "Text");
        }

        public SelectList ArtisTuruSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Üfe Oranı",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Tüfe Oranı",
                                    Value="2"
                                  }
            };
            return new SelectList(newList, "Value", "Text");

        }

        public SelectList KiraYenilemePeriyotSelectList()
        {
            var periyot = _parametreService.GetirListe(13).Select(x => new { Id = x.Deger, Ad = x.Ad }).ToList();

            return new SelectList(periyot, "Id", "Ad");
        }

        public SelectList IcraDurumlariSelectList()
        {
            var periyot = _icraDurumService.GetirListeAktif().Select(x => new {x.Id,x.Ad }).ToList();

            return new SelectList(periyot, "Id", "Ad");
        }

        public SelectList OdemeDurumuSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Tahsil Edildi",
                                    Value="1"
                                  },
                                  new SelectListItem(){
                                  Text="Vadesi Gelmeyen",
                                  Value="0"
                                 }
            };

            return new SelectList(newList, "Value", "Text");
        }

        #endregion
    }
}