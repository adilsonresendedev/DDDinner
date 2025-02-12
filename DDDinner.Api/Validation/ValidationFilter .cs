using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace DDDinner.Api.Validation
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.Any())
            {
                await next();
                return;
            }

            var errors = new List<Error>();

            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator == null) continue;

                var validationContext = new ValidationContext<object>(argument);
                var validationResult = await validator.ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors.Select(e =>
                        Error.Validation(e.PropertyName, e.ErrorMessage)));
                }
            }

            if (errors.Any())
            {
                context.Result = new ObjectResult(errors) { StatusCode = StatusCodes.Status422UnprocessableEntity };
                return;
            }

            await next();
        }
    }
}
