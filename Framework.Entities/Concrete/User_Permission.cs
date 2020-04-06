using System.ComponentModel.DataAnnotations;

namespace Framework.Entities.Concrete
{
    public class User_Permission
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ControllerActionId { get; set; }
        public bool IsAuthorize { get; set; }
        public bool IsDeleted { get; set; }
        public User Users { get; set; }
        public ControllerAction ControllerActions { get; set; }
    }
}
