using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Concrete
{
    public class ControllerAction : IEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string AreaName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Attributes { get; set; }
        public string ReturnType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
