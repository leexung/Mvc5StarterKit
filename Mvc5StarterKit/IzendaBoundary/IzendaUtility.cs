using Mvc5StarterKit.IzendaBoundary.Models;
using Mvc5StarterKit.IzendaBoundary.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary
{
    public class IzendaUtility
    {
        public static async Task<IList<ConnectionModel>> GetConnections(Guid? tenantId, string authToken)
        {
            string action = string.Empty;
            if (tenantId == null)
            {
                action = "connection";
            }
            else
            {
                action = "connection/" + tenantId.ToString();
            }
            var connections = await WebAPIService.Instance.GetAsync<IList<ConnectionModel>>(action, authToken);
            return connections;
        }

        public static async Task<ConnectionDetail> GetConnectionDetail(Guid connectionId, string authToken)
        {
            string action = "connection/detail/" + connectionId.ToString();
            var connection = await WebAPIService.Instance.GetAsync<ConnectionDetail>(action, authToken);
            return connection;
        }

        public static async Task UpdateConnectionDetail(ConnectionModel payload, string authToken)
        {
            string action = "connection";
            await WebAPIService.Instance.PostAsync(action, payload, authToken);
        }

        public static async Task SaveRole(RoleDetail payload, string authToken)
        {
            string action = "role/intergration/saveRole";
            //string action = "role";
            await WebAPIService.Instance.PostAsync(action, payload, authToken);
        }

        public static async Task<IList<TenantsModel>> GetTenants(string authToken)
        {
            string action = "tenant/allTenants";
            var tenants = await WebAPIService.Instance.GetAsync<IList<TenantsModel>>(action, authToken);
            return tenants;
        }

        public static async Task<IList<RoleDetail>> GetRoles(Guid? tenantId, string authToken)
        {
            string action = string.Empty;
            if (tenantId == null)
            {
                action = "role/all";
            }
            else
            {
                action = "role/all/" + tenantId.ToString();
            }
            var roles = await WebAPIService.Instance.GetAsync<IList<RoleDetail>>(action, authToken);
            return roles;
        }

        public static async Task<IList<UserDetail>> GetUsers(Guid? tenantId, string authToken)
        {
            string action = string.Empty;
            if (tenantId == null)
            {
                action = "user/all";
            }
            else
            {
                action = "user/all/" + tenantId.ToString();
            }
            var users = await WebAPIService.Instance.GetAsync<IList<UserDetail>>(action, authToken);
            return users;
        }

        public static async Task<RoleDetail> GetRole(Guid roleId, string authToken)
        {
            string action = "role/" + roleId.ToString();
            var role = await WebAPIService.Instance.GetAsync<RoleDetail>(action, authToken);
            return role;
        }

        public static async Task<Permission> GetPermission(string authToken)
        {
            string action = "authorization/permission";
            var permission = await WebAPIService.Instance.GetAsync<Permission>(action, authToken);
            return permission;
        }

        public static async Task<ConnectionModel> ReloadRemoteSchema(object payload, string authToken)
        {
            string action = "connection/reloadRemoteSchema";
            return await WebAPIService.Instance.PostReturnValueAsync<ConnectionModel, object>(action, payload, authToken);
        }
    }
}