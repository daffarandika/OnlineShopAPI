using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShopAPI.Utils;
using System.Security.Claims;
using System.Text;

namespace OnlineShopAPI.Attributes
{
    public class JwtAuth : Attribute, IAuthorizationFilter
    {
        string Role { get; set; }
        public JwtAuth(string Role = "")
        {
            this.Role = Role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("authorization"))
            {
                context.ModelState.AddModelError("Unauthorized", "You need to provide a token");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
                return;
            }
            string token = context.HttpContext.Request.Headers.First(x => x.Key.ToLower() == "authorization").Value.ToString();
            if (Guard.FailsAgainstInvalidToken(token))
            {
                context.ModelState.AddModelError("Unauthorized", "Your token is invalid");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
                return;
            }
            if (Guard.FailsAgainstExpiredToken(token))
            {
                context.ModelState.AddModelError("Unauthorized", "Your token has expired");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
                return;
            }
            if (Role.Length != 0)
            {
                string userRole = Helper.GetValueFromToken(token, ClaimTypes.Role);
                if (userRole.Trim('\\').Trim('\"').ToLower() != Role.ToLower())
                {
                    context.ModelState.AddModelError("Unauthorized", "You do not have the right privilege to access this page");
                    context.Result = new UnauthorizedObjectResult(context.ModelState);
                    return;
                }
            }
        }
    }
}
