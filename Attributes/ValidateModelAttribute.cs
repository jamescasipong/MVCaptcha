using Microsoft.AspNetCore.Mvc.Filters;
using MVCaptcha.Exceptions;

namespace MVCaptcha.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!context.ModelState.IsValid)
            //{
            //    var errors = context.ModelState
            //        .Where(x => x.Value.Errors.Count > 0)
            //        .ToDictionary(
            //            kvp => kvp.Key,
            //            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            //        );

            //    throw new ValidationException(errors);
            //}
        }
    }
}
