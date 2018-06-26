using Izenda.BI.Logic.CustomConfiguration;
using Izenda.BI.Framework.Models.UserManagement;
using Rhino.Licensing;

namespace Mvc5StarterKit
{
    public static class IzendaConfig
    {
        ///<izendaIntegration>Required, Unique to Deployment Mode 3</izendaIntegration>
        public static void RegisterLoginLogic()
        {
            //<summary>Logic to generate tokens for server-side interactions with Izenda</summary><remarks>This is used for exporting only</remarks>
            UserIntegrationConfig.GetAccessToken = (args) =>
            {
                return IzendaBoundary.IzendaTokenAuthorization.GetToken(new Models.UserInfo()
                {
                    UserName = args.UserName,
                    TenantUniqueName = args.TenantId
                });
            };
            //<summary>Logic to validate tokens sent with requests to Izenda API</summary><remarks>Tokens are generated either from a RESTful API route (see <see cref="UserController.GenerateToken()"/>) in Host App and delivered to client or from server-side logic above.</remarks>
            UserIntegrationConfig.ValidateToken = (ValidateTokenArgs args) =>
            {
                var token = args.AccessToken;
                var user = IzendaBoundary.IzendaTokenAuthorization.GetUserInfo(token);

                // TenantUniqueName corresponds to the 'TenantID' field in the IzendaTenant table
                return new ValidateTokenResult { UserName = user.UserName, TenantUniqueName = user.TenantUniqueName };
            };
        }
    }
}
