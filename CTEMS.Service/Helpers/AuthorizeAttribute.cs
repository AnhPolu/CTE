using Common.Enums;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTEMS.Service.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<CTERole> _roles;
        private readonly object _lock = new object();

        public AuthorizeAttribute(params CTERole[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            lock (_lock)
            {
                // skip authorization if action is decorated with [AllowAnonymous] attribute
                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonymous)
                    return;

                // authorization
                AuthenticateResponse user = context.HttpContext.Items["User"] as AuthenticateResponse;
                List<CTERole> userRoles = user?.Roles?.ToList() ?? new List<CTERole>();

                if (user == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }

                List<CTERole> metRoleCondition = new List<CTERole>();

                userRoles.ForEach(x =>
                {
                    if (_roles.Contains(x)) metRoleCondition.Add(x);
                });

                if (_roles.Any() && !metRoleCondition.Any())
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }
            }
        }
    }
}
