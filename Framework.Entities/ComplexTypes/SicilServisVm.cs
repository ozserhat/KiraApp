using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.ComplexTypes
{
    public class SicilServisVm
    {
        public string SicilNo { get; set; }

        public string VergiNo { get; set; }

        public string TcKimlikNo { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        [StringLength(500)]
        public string Soyad { get; set; }

        [StringLength(500)]
        public string Tanim { get; set; }

        public string Il{ get; set; }

        public string Ilce { get; set; }

        public string Mahalle { get; set; }

        public string Sokak { get; set; }

        [StringLength(500)]
        public string VergiDairesi { get; set; }

        [StringLength(1500)]
        public string AcikAdres { get; set; }
    }
}
