namespace gain_api.Filters
{
    using core.Dto;
    using FluentValidation;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ValidationFilter<T>(IValidator<T> validator) : IAsyncActionFilter where T : class
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.FirstOrDefault().Value is T model)
            {
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    var errorResponse = ResponseDto<T>.Failure("Error de validación",ValidatorsHelper.ValidationErros(validationResult));
                    context.Result = new BadRequestObjectResult(errorResponse);
                    return;
                }
            }
            await next();
        }
    }
}
