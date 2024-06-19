using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApi.Filters.Helpers;

namespace MovieApi.Filters;

public class ApiKeyAuthorizationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyConstants.ApiKeyHeaderName, out var incomingApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Api key is missing");
            return;
        }

        var apiKeyValidator = context.HttpContext.RequestServices.GetRequiredService<IApiKeyValidator>();
        if (!apiKeyValidator.IsValid(incomingApiKey!))
            context.Result = new UnauthorizedObjectResult("Api key is invalid");
    }
}