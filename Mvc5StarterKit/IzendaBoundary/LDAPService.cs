using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Net;

namespace Mvc5StarterKit.IzendaBoundary
{
    public class LDAPService
    {
        /// <summary>
        /// This method doesnot need an account to connect to AD to setup a PrincipalContext
        /// Can use this method for a secure LDAP connection (LDAPS)
        /// Secure LDAP port: 636, Non-secure LDAP port: 389
        /// </summary>
        public static bool Authenticate(string username, string password)
        {
            string domain = ConfigurationManager.AppSettings["LDAPName"];
            string port = ConfigurationManager.AppSettings["LDAPPort"];

            //remove the domain in username value
            if (username.Contains("\\"))
            {
                var s = username.Split('\\');
                username = s[1];
            }
            try
            {
                using (var ldapConnection = new LdapConnection(domain + ":" + port))
                {
                    var networkCredential = new NetworkCredential(username, password, domain);
                    ldapConnection.SessionOptions.SecureSocketLayer = false;
                    ldapConnection.AuthType = AuthType.Negotiate;
                    ldapConnection.Bind(networkCredential);
                }

                // if the bind succeeds, the credentials are OK
                return true;
            }
            catch (LdapException ex)
            {
                return false;
            }
        }
    }
}