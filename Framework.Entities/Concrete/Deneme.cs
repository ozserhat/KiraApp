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
    [Table("Deneme")]
    public class Deneme:IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(59)]
        public string Ad { get; set; }

        public string Soyad { get; set; }
    }
}
