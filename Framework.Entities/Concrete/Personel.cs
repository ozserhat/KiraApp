using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Concrete
{  
    [Table("Personel")]
   public class Personel:IEntity
    {
       [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Ad{ get; set; }

        [StringLength(30)]
        public string Soyad { get; set; }

        [StringLength(30)]
        public string Memleket { get; set; }

    }
}
