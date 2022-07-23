namespace DoorWayApiProject.Authorization;
 using DoorWayApiProject.Helpers.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DoorWayApiProject.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

   
   
    private readonly IList<RoleEnum> _roles;

    public AuthorizeAttribute(params RoleEnum[] roles)
    {
        _roles = roles ?? new RoleEnum[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;


      
        
        // authorization
        var user = (User)context.HttpContext.Items["User"];
        var test = (RoleEnum)user.role_ID;
        if (user == null || (_roles.Any() && !_roles.Contains((RoleEnum)user.role_ID)))
            
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}