using Framework.Core.DataAccess;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        IEnumerable<User> GetAll();
        List<UserRoleItem> GetUserRoles(User user);
        UserDetail GetByDetailByUserId(int Id);
        User GetById(int Id);
        User GetByGuid(Guid guid);
        UserDetail GetUsers(User user);
        bool UserExists(string username);
        User Register(User user, string password);
        User Ekle(User user);
        User Guncelle(User user);
        bool Sil(int id);
    }
}
