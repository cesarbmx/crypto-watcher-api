using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Hyper.Api.Responses;
using Hyper.Shared.Exceptions;
using Hyper.Domain.Messages;
using Hyper.Shared.Extensions;

namespace Hyper.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Response
            ErrorResponse errorResponse;
            int errorCode;

            switch (exception)
            {
                case UnauthorizedException _: // 401
                    var unauthorizedException = (UnauthorizedException)exception;
                    errorCode = 401;
                    errorResponse = new ErrorResponse(unauthorizedException.Message.GetCode(), errorCode, unauthorizedException.Message.GetMessage());
                    break;
                case ForbiddenException _:    // 403
                    var forbiddenException = (ForbiddenException)exception;
                    errorCode = 401;
                    errorResponse = new ErrorResponse(forbiddenException.Message.GetCode(), errorCode, forbiddenException.Message.GetMessage());
                    break;
                case NotFoundException _:     // 404
                    var notFoundException = (NotFoundException)exception;
                    errorCode = 404;
                    errorResponse = new ErrorResponse(notFoundException.Message.GetCode(), errorCode, notFoundException.Message.GetMessage());
                    break;
                case ConflictException _:     // 409
                    var conflictException = (ConflictException)exception;
                    errorCode = 409;
                    errorResponse = new ErrorResponse(conflictException.Message.GetCode(), errorCode, conflictException.Message.GetMessage());
                    break;
                case ValidationException _:    // 422
                    var validationException = (ValidationException)exception;
                    errorCode = 422;
                    errorResponse = new ErrorResponse(validationException.Message.GetCode(), errorCode, validationException.Message.GetMessage());
                    break;
                default:                      // 500
                    errorCode = 500;
                    errorResponse = new ErrorResponse(ServiceMessages.InternalServerError.GetCode(), errorCode, ServiceMessages.InternalServerError.GetMessage());
                    // Log error
                    _logger.LogError(exception, "Event:UnhandledException");
                    break;
            }

            var response = JsonConvert.SerializeObject(errorResponse, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCode;
            return context.Response.WriteAsync(response);
        }
    }
}
