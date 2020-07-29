using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Helpers
{
    public class LDAPHelper
    {
        public string LDAPPath = "LDAP://csb.local";

        public IEnumerable GetADUsers(string term)
        {
            try
            {
                var persons = new List<string>();

                var ldapUser = ConfigurationManager.AppSettings["LdapUser"];
                var ldapPass = ConfigurationManager.AppSettings["LdapPassword"];


                using (var searchRoot = new DirectoryEntry(LDAPPath, ldapUser, ldapPass))
                {
                    using (var search = new DirectorySearcher(searchRoot))
                    {
                        //var search = new DirectorySearcher(searchRoot);
                        search.Filter = $"(SAMAccountName={term}*)";

                        search.PropertiesToLoad.Add("SAMAccountName");
                        search.PropertiesToLoad.Add("displayName"); //first name
                        search.PageSize = 1001;
                        search.SizeLimit = System.Int32.MaxValue;
                        var resultCol = search.FindAll();

                        for (var counter = 0; counter < resultCol.Count; counter++)
                        {
                            var result = resultCol[counter];
                            if (result.Properties.Contains("SAMAccountName") &&
                                result.Properties.Contains("displayName"))
                                persons.Add((string)result.Properties["SAMAccountName"][0]);
                        }
                    }

                    IEnumerable ipersons = persons.AsEnumerable().OrderBy(p => p);
                    return ipersons;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }


        public string isAuthanticated(DomainUser user, bool withoutPassWord = false)
        {
            var userRealName = "";
            try
            {
                var fullName = string.Format("{0}\\{1}", user.Domain, user.Name);
                DirectoryEntry entry;
                if (withoutPassWord)
                    entry = new DirectoryEntry(LDAPPath);
                else
                    entry = new DirectoryEntry(LDAPPath, fullName, user.Password);

                var searcher = new DirectorySearcher(entry);
                searcher.Filter = string.Format("(SAMAccountName={0})", user.Name);
                searcher.PropertiesToLoad.Add("CN");

                var sr = searcher.FindOne();

                if (!sr.Equals(DBNull.Value))
                {
                    var Properties = sr.Properties;
                    foreach (string Key in Properties.PropertyNames)
                        if (Key == "cn")
                            foreach (var Values in Properties[Key])
                                userRealName = Values.ToString();
                }
            }
            catch (Exception exception)
            {
            }
            return userRealName;
        }

        public DomainUser GetByUsername(string username)
        {
            var kullanici = new DomainUser();
            var searchRoot = new DirectoryEntry(LDAPPath);
            using (searchRoot)
            {
                using (var ds = new DirectorySearcher(searchRoot))
                {
                    ds.PropertiesToLoad.Add("displayName");
                    ds.PropertiesToLoad.Add("SAMAccountName");
                    ds.PropertiesToLoad.Add("mail");

                    ds.Filter = "(SAMAccountName=" + username + ")";

                    try
                    {
                        var result = ds.FindOne();

                        if (result.Properties["SAMAccountName"].Count > 0 && result.Properties["displayName"].Count > 0 &&
                            result.Properties["mail"].Count > 0)
                            kullanici = new DomainUser
                            {
                                Domain = result.Properties["SAMAccountName"][0].ToString(),
                                Name = result.Properties["displayName"][0].ToString(),
                                Email = result.Properties["mail"][0].ToString()
                            };
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }

            return kullanici;
        }




        public class DomainUser
        {
            public object Domain { get; set; }
            public object Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}