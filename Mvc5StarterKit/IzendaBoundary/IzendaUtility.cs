using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Izenda.BI.API.Results;
using Izenda.BI.Framework.Models.Permissions;
using Izenda.BI.Framework.Models;
using Izenda.BI.Framework.Models.DBStructure;

namespace Mvc5StarterKit.IzendaBoundary
{
    public class IzendaUtility
    {
        public static async Task<IList<Connection>> GetConnections(Guid? tenantId, string authToken)
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
            var connections = await WebAPIService.Instance.GetAsync<IList<Connection>>(action, authToken);
            return connections;
        }

        public static async Task<ConnectionResult> GetConnectionDetail(Guid connectionId, string authToken)
        {
            string action = "connection/detail/" + connectionId.ToString();
            var connection = await WebAPIService.Instance.GetAsync<ConnectionResult>(action, authToken);
            return connection;
        }

        public static async Task UpdateConnectionDetail(Connection payload, string authToken)
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

        public static async Task<IList<Tenants>> GetTenants(string authToken)
        {
            string action = "tenant/allTenants";
            var tenants = await WebAPIService.Instance.GetAsync<IList<Tenants>>(action, authToken);
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

        public static async Task<SchemaResult> ReloadRemoteSchema(object payload, string authToken)
        {
            string action = "connection/reloadRemoteSchema";
            return await WebAPIService.Instance.PostReturnValueAsync<SchemaResult, object>(action, payload, authToken);
        }
    }
}