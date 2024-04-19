﻿using Contracts;
using Entities;
using Shared.Exceptions;

namespace FitScriptions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadRequestException ex)
            {
                _logger.LogError($"Request failed on {nameof(BadRequestException)} with error {ex.Message} causing error code:{StatusCodes.Status400BadRequest}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (GymNotFoundException ex)
            {
                _logger.LogError($"Request failed on {nameof(GymNotFoundException)} with error {ex.Message} causing error code:{StatusCodes.Status404NotFound}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failed on {nameof(Exception)} with error {ex.Message} causing error code:{StatusCodes.Status500InternalServerError}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, int statusCode, string errorMessage)
        {
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                message = errorMessage
            }.ToString());
        }
    }
}