using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AcerPro.Presentation.Server.Infrastructures;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute :
Attribute, IAuthorizationFilter
{

    public AuthorizeAttribute() : base()
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        IServiceProvider services = context.HttpContext.RequestServices;
        var configuration = services.GetRequiredService<IConfiguration>();

        var requestHeaders =
            context.HttpContext.Request.Headers["Authorization"];

        var token =
            (requestHeaders
            .FirstOrDefault()
            ?.Split(" ")
            .Last()) ?? throw new UnauthorizedAccessException("User is unauthorized");

        JwtUtility.AttachUserToContext(context: context.HttpContext, token: token, secretKey: configuration["SecretKey"]);
    }
}
