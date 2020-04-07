using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    public class User_Permission : IEntity
    {
        [Key]
        public int Id { get; set; }
      
        public int User_Id { get; set; }
   
        public int ControllerAction_Id { get; set; }
        public bool IsAuthorize { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("User_Id")]
        public User Users { get; set; }

        [ForeignKey("ControllerAction_Id")]
        public ControllerAction ControllerActions { get; set; }
    }
}
