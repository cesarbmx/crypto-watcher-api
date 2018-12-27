using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CryptoWatcher.Application.System.Responses;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.Api.Middlewares
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
                    errorResponse = new ErrorResponse(nameof(Message.NotAuthenticated), errorCode, unauthorizedException.Message);
                    break;
                case ForbiddenException _:    // 403
                    var forbiddenException = (ForbiddenException)exception;
                    errorCode = 401;
                    errorResponse = new ErrorResponse(nameof(Message.PermissionRequired), errorCode, forbiddenException.Message);
                    break;
                case NotFoundException _:     // 404
                    var notFoundException = (NotFoundException)exception;
                    errorCode = 404;
                    errorResponse = new ErrorResponse(nameof(Message.NotFound), errorCode, notFoundException.Message);
                    break;
                case ConflictException _:     // 409
                    var conflictException = (ConflictException)exception;
                    errorCode = 409;
                    errorResponse = new ErrorResponse(nameof(Message.Conflict), errorCode, conflictException.Message);
                    break;
                case ValidationException _:    // 422
                    var validationException = (ValidationException)exception;
                    errorCode = 422;
                    errorResponse = new ErrorResponse(nameof(Message.ValidationFailed), errorCode, validationException.Message);
                    break;
                default:                      // 500
                    errorCode = 500;
                    errorResponse = new ErrorResponse(nameof(Message.InternalServerError), errorCode, Message.InternalServerError);
                    // Log error
                    _logger.LogSplunkError("UnhandledException", exception);
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
