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
    [Table("Iller")]
    public class Il:IEntity
    {
        [Key]
        public int Id { get; set; }

        public int IlKodu { get; set; }

        public string Ad { get; set; }

    }
}
