using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Mvc5StarterKit.IzendaBoundary;
using Mvc5StarterKit.Models;

namespace Mvc5StarterKit.Managers
{
    public class ADCachingManager
    {
        #region Constants

        public const string AD_GROUP = "ADGroup_Cache";

        public const string AD_USER = "ADUser_Cache";

        #endregion

        #region Singleton
        static private readonly ADCachingManager Instance = new ADCachingManager();

        private ADCachingManager()
        {

        }
        public static ADCachingManager GetInstance()
        {
            return Instance;
        }
        #endregion

        public void Insert(string key, object data)
        {
            HttpRuntime.Cache.Insert(
              /* key */                key,
              /* value */              data,
              /* dependencies */       null,
              /* absoluteExpiration */ Cache.NoAbsoluteExpiration,
              /* slidingExpiration */  Cache.NoSlidingExpiration,
              /* priority */           CacheItemPriority.NotRemovable,
              /* onRemoveCallback */   null);
        }

        public T GetCache<T>(string key)
        {
            var cacheData = HttpRuntime.Cache[key];
            return (T)cacheData;
        }

        public IList<ADGroup> GetADGroups()
        {
            if (HttpRuntime.Cache[AD_GROUP] != null)
            {
                return HttpRuntime.Cache[AD_GROUP] as IList<ADGroup>;
            }

            //If has not cached already
            var adGroups = LDAPService.GetInstance().GetADGroups();
            Insert(AD_GROUP, adGroups);
            return adGroups;
        }

        public IList<ADUser> GetADUsers()
        {
            if (HttpRuntime.Cache[AD_USER] != null)
            {
                return HttpRuntime.Cache[AD_USER] as IList<ADUser>;
            }

            //If has not cached already
            var adUsers = LDAPService.GetInstance().GetADUsers();
            Insert(AD_USER, adUsers);
            return adUsers;
        }
    }
}