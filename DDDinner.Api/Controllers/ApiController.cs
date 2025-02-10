using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace DDDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ErrorOr<T> result, string locationUri = null)
        {
            if (result.IsError)
            {
                return HandleErrors(result.Errors);
            }

            if (result.Value is null)
            {
                return NoContent();
            }

            if (!string.IsNullOrEmpty(locationUri))
            {
                return Created(locationUri, result.Value);
            }

            return Ok(result.Value);
        }

        private IActionResult HandleErrors(List<Error> errors)
        {
            var firstError = errors[0];

            return firstError.Type switch
            {
                ErrorType.Conflict => Conflict(firstError.Description),
                ErrorType.Validation => UnprocessableEntity(firstError.Description),
                ErrorType.NotFound => NotFound(firstError.Description),
                _ => StatusCode(StatusCodes.Status500InternalServerError, firstError.Description)
            };
        }
    }
}
