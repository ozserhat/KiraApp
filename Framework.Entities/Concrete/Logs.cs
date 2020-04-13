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
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
    }
}
