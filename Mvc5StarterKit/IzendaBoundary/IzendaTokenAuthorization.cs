using Mvc5StarterKit.Models;
using Rhino.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary
{
    public class IzendaTokenAuthorization
    {
        #warning Change this key to ensure security for your app !!!
        const string KEY = "THISISKEY1234567"; //must be at least 16 characters long (128 bits)

        /// <summary>
        /// Generate token from UserInfo. Userinfo will be encrypted before sending to Izenda.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetToken(UserInfo user)
        {
            // remove tenant property when sending token to Izenda, if Tenant is System.
            if (user.TenantUniqueName == "System")
                user.TenantUniqueName = null;

            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var token = StringCipher.Encrypt(serializedObject, KEY);
            return token;
        }


        /// <summary>
        /// Get User info from token. Token, which recieved from Izenda, will be decrypted to get user info.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string token)
        {
            var serializedObject = StringCipher.Decrypt(token, KEY);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(serializedObject);
            return user;
        }
    }
}