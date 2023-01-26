using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Net.Http.Headers; 
using System.Text;
using Durian.Models;
using Durian.Data;
using System.Security.Claims;

namespace Durian.Handler 
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DurianContext durianContext;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder,
        ISystemClock clock, DurianContext duriancontext) : base(options,loggerFactory,encoder,clock)
        {
            this.durianContext = duriancontext;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync(){
            if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("No Header Found");

            var _headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(_headervalue.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(':');
                string username = array[0];
                string password = array[1];
                var user = this.durianContext.Users.FirstOrDefault(item=>item.Username==username && item.Password==password);
                if(user==null)
                    return AuthenticateResult.Fail("UnAuthorized");

                //GenerateTicket
                var claim = new[]{new Claim(ClaimTypes.Name, username)};
                var identity = new ClaimsIdentity(claim,Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("UnAuthorized");
            }
        }
    }
}