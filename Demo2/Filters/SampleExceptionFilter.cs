using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Demo2.Exceptions;

namespace Demo2.Filters
{
    public class SampleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DuplicateEntryException)
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
