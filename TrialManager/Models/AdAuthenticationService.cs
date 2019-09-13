using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.Owin.Security;



namespace TrialManager.Models
{
    public class AdAuthenticationService
    {
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }


        /// <summary>
        /// Check if username and password matches existing account in AD. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticationResult SignIn(String username, String password)
        {
            ContextType authenticationType = ContextType.Domain;

            PrincipalContext principalContext = new PrincipalContext(authenticationType);
//            PrincipalContext principalContext = new PrincipalContext(authenticationType, null, "dc=campus,dc=ncl,dc=ac,dc=uk", "nmtmd", "NineSingingPanthers19");
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                if (userPrincipal != null)
                {
                    isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                }
            }
            catch (Exception exception)
            {
                //TODO log exception in your ELMAH like this:
                //Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (!isAuthenticated)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                // here can be a security related discussion whether it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                // here can be a security related discussion whether it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is disabled");
            }

            var identity = CreateIdentity(userPrincipal);

            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);


            return new AuthenticationResult();
        }


        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, userPrincipal.GivenName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, userPrincipal.Surname));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            //try
            //{
                PrincipalSearchResult<Principal> groups = userPrincipal.GetGroups();
                foreach (var @group in groups)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, @group.Name));
                }

                // add your own claims if you need to add more information stored on the cookie

                return identity;
            //}
            //catch (Exception exception)
            //{
                //return identity;

            //}
        }
    }
}
