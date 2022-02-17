using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SuBeefrri.Core.Exceptions;
using System.Net;

namespace SuBeefrri.Services.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string exception;
            if (context.Exception.GetType() == typeof(CustomException))
                exception = ((CustomException)context.Exception).Message;
            else
                exception = "Ocurrio un error interno...!!!";
            var f = context.Exception;
            context.Result = new BadRequestObjectResult(exception);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.ExceptionHandled = true;
        }
    }
}
