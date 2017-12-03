using RPL.VDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace Library
{
    public static class LDAPUserDAC
    {
        // ref: http://www.codeproject.com/Tips/599697/Get-list-of-Active-Directory-users-in-Csharp
        public static List<LDAPUser> GetUsersFromActiveDirectory(string domainPath, out string message)
        {
            try
            {
                List<LDAPUser> aDUsers = new List<LDAPUser>();
                DirectoryEntry searchRoot = new DirectoryEntry(domainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("displayname");
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname")
                            && result.Properties.Contains("mail")
                            && result.Properties.Contains("displayname"))
                        {
                            LDAPUser user = new LDAPUser();
                            user.UserName = (String)result.Properties["samaccountname"][0];
                            user.DisplayName = (String)result.Properties["displayname"][0];
                            user.Email = !string.IsNullOrWhiteSpace((String)result.Properties["mail"][0]) ? (String)result.Properties["mail"][0]
                                : string.Empty;
                            aDUsers.Add(user);
                        }
                    }
                }
                message = "OK";
                return aDUsers;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }

        public static LDAPUser GetUserFromActiveDirectory(string username, string domainPath, out string message)
        {
            try
            {
                LDAPUser adUser = new LDAPUser();
                DirectoryEntry searchRoot = new DirectoryEntry(domainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("displayname");
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname")
                            && result.Properties.Contains("mail")
                            && result.Properties.Contains("displayname"))
                        {
                            string smaName = (String)result.Properties["samaccountname"][0];
                            if (!string.IsNullOrWhiteSpace(smaName)
                                && smaName.Trim().ToLower() == username.Trim().ToLower())
                            {
                                adUser = new LDAPUser();
                                adUser.UserName = (String)result.Properties["samaccountname"][0];
                                adUser.DisplayName = (String)result.Properties["displayname"][0];
                                adUser.Email = !string.IsNullOrWhiteSpace((String)result.Properties["mail"][0]) ? (String)result.Properties["mail"][0]
                                    : string.Empty;
                                break;
                            }
                        }
                    }
                }
                message = "OK";
                return adUser;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
    }
}
