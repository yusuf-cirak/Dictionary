using Microsoft.AspNetCore.Mvc.Filters;

namespace Dictionary.WebApi.Infrastructure.ActionFilters
{
    public sealed class ValidateModelStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => !string.IsNullOrEmpty(e.ErrorMessage) ? e.ErrorMessage : e.Exception?.Message)
                    .Distinct().ToArray();
                return;
            }

            await next();
        }
    }
}
