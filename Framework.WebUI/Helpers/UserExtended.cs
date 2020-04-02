using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Framework.WebUI.Helpers
{
    public static class UserExtended
    {
        static IEnumerable<Claim> GetClaims(IIdentity identity)
        {
            var newIdentity= (ClaimsIdentity)identity;
            return newIdentity.Claims;
        }

        static Claim GetClaimByTypeName(IPrincipal user, string type)
        {
            return GetClaims(user.Identity).Where(m => m.Type == type).FirstOrDefault();
        }
        static List<Claim> GetClaimsByTypeName(IPrincipal user, string type)
        {
            return GetClaims(user.Identity).Where(m => m.Type == type).ToList();
        }
        /// <summary>
        /// verilen property adına göre değer sorgulaması yapar
        /// </summary>
        /// <param name="user">IPrincipal</param>
        /// <param name="name">property name</param>
        /// <returns>property bilgisi var ise property değerini döndürür; yok ise null</returns>
        public static string GetUserPropertyValue(this IPrincipal user, string name)
        {
            var claim = GetClaimByTypeName(user, name);

            return claim != null ? claim.Value : null;
        }

        public static List<Claim> GetUserPropertyValues(this IPrincipal user, string name)
        {
            var claim = GetClaimsByTypeName(user, name);

            return claim != null ? claim.ToList() : null;
        }
    }
}