using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class LdapAuthentication
    {

        private string _path;
        private string _filterAttribute;

        public string x { get; set; }


        public LdapAuthentication(string path)
        {
            x = "";
            _path = path;
        
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path,
                domainAndUsername, pwd);
            try
            {

                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if(null == result)
                {
                    return false;
                }
                //Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];

            }
            catch(Exception ex)
            {
                x = ex.Message;
                return false;

            }
            return true;
        }
    }
}