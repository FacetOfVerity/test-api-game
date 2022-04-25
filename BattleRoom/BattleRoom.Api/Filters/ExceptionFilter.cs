using BattleRoom.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BattleRoom.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException)
        {
            context.Result = new JsonResult(new ExceptionData(context.Exception.Message));
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            context.Result = new JsonResult(new ExceptionData("Server error."));
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    
    internal class ExceptionData
    {
        public ExceptionData(string message) => Message = message;

        public string Message { get; set; }
    }
}