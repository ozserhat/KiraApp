using Framework.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string Audit { get; set; }
    }
}
