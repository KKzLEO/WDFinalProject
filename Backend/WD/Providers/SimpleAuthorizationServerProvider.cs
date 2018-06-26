using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WD.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                this.ReserveLoginMode(context.UserName, context.Password);

                switch (this.LoginMode)
                {
                    //case LoginModeEnum.SSO:
                    //    this.RenderIdentityTicket(context.Password, ref context);
                    //    break;
                    //case LoginModeEnum.Login:
                    //    Core.Service.Interface.ILoginService loginService;
                    //    loginService = (Core.Service.Interface.ILoginService)(new Ir.Utility.SpringUtility.RepositoryFactory()).Service("LoginService");


                    //    if (loginService.CasLogin(context.UserName, context.Password))
                    //    {
                    //        this.RenderIdentityTicket(context.UserName, ref context);
                    //    }

                    //    break;
                    //case LoginModeEnum.Admin:

                    //    if (context.UserName == "admin" && context.Password.Split(':')[1] == "p@ssw0rd" + System.DateTime.Now.ToString("yyyyMMdd"))
                    //    {
                    //        this.RenderIdentityTicket(context.Password.Split(':')[0], ref context);
                    //    }
                    //    break;
                    //default:
                    //    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        private void RenderIdentityTicket(string usrId, ref OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Core.Service.Interface.IAccessControlService accessControlService;
            //accessControlService = (Core.Service.Interface.IAccessControlService)(new Ir.Utility.SpringUtility.RepositoryFactory()).Service("AccessControlService");

            //var userAccessControl = accessControlService.LoadUserAccessControl(usrId);

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Name, userAccessControl.UserId));
            //identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userAccessControl.PerSerilNo));
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAccessControl.EmployeeNo));
            //identity.AddClaim(new Claim(ClaimTypes.PrimaryGroupSid, userAccessControl.CompanySerilNo));
            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //            {
            //                {"perSerilNo", userAccessControl.PerSerilNo},
            //                {"userName", userAccessControl.UserName},
            //                {"userId", userAccessControl.UserId},
            //                {"userMail", userAccessControl.UserMail},
            //                {"companySerilNo",userAccessControl.CompanySerilNo},
            //                {"companyName",userAccessControl.CompanyName},
            //                {"companyId",userAccessControl.CompanyId},
            //                {"ouId",userAccessControl.OuId},
            //                {"ouSerilNo", userAccessControl.OuSerilNo},
            //                {"ouName", userAccessControl.OuName},
            //                {"employeeNo", userAccessControl.EmployeeNo }
            //            });
            //var ticket = new AuthenticationTicket(identity, props);

            //if (Models.ApVariable.LogApStatistic)
            //{
            //    ApStatistic.ToolKit.Helper apStatisticHelper = new ApStatistic.ToolKit.Helper();
            //    apStatisticHelper.LogTrailAsync(string.Empty, userAccessControl.UserId, Core.Variable.Variable.ApId, "TakeToken", "TakeToken");
            //}

            //context.Validated(ticket);
        }

        private void ReserveLoginMode(string userName, string password)
        {
            this.LoginMode = LoginModeEnum.Login;
            if (userName == "sso")
            {
                this.LoginMode = LoginModeEnum.SSO;
            }

            if (userName == "admin" && password.Split(':')[1] == "p@ssw0rd" + System.DateTime.Now.ToString("yyyyMMdd"))
            {
                this.LoginMode = LoginModeEnum.Admin;
            }
        }

        private LoginModeEnum LoginMode;
        private enum LoginModeEnum
        {
            SSO,
            Login,
            Admin
        }
    }

}