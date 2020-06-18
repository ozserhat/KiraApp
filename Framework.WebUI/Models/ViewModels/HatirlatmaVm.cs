using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class HatirlatmaVm
    {
        public int GecikenTahakkukSayisi { get; set; }
        public int ArtisiGelenSayisi { get; set; }
        public int SozlemesiBitenSayisi { get; set; }

        public int OdemesiGelenTahakkukSayisi { get; set; }
    }

    public class HatirlatmaListesiVM : PagingVMBase
    {
        public IPagedList<Tahakkuk> ArtisiGelenler { get; set; }
        public IPagedList<Tahakkuk> OdemesiGecikenler { get; set; }
        public IPagedList<Tahakkuk> OdemesiGelenler { get; set; }

        public IPagedList<Tahakkuk> SozlesmesiBitenler { get; set; }
    }

}