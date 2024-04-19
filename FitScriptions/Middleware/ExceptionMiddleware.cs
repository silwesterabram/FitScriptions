using Contracts;
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
            catch (BlobFileFormatException ex)
            {
                _logger.LogError($"Request failed on {nameof(BlobFileFormatException)} with error {ex.Message} causing error code:{StatusCodes.Status400BadRequest}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (BlobFileNullException ex)
            {
                _logger.LogError($"Request failed on {nameof(BlobFileNullException)} with error {ex.Message} causing error code:{StatusCodes.Status400BadRequest}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (BlobFileSizeException ex)
            {
                _logger.LogError($"Request failed on {nameof(BlobFileSizeException)} with error {ex.Message} causing error code:{StatusCodes.Status400BadRequest}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (BlobNameNotFoundException ex)
            {
                _logger.LogError($"Request failed on {nameof(BlobNameNotFoundException)} with error {ex.Message} causing error code:{StatusCodes.Status404NotFound}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (CreationListParameterException ex)
            {
                _logger.LogError($"Request failed on {nameof(CreationListParameterException)} with error {ex.Message} causing error code:{StatusCodes.Status400BadRequest}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (QRCodeNotFoundException ex)
            {
                _logger.LogError($"Request failed on {nameof(QRCodeNotFoundException)} with error {ex.Message} causing error code:{StatusCodes.Status404NotFound}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (SubscriptionTypeNotFoundException ex)
            {
                _logger.LogError($"Request failed on {nameof(SubscriptionTypeNotFoundException)} with error {ex.Message} causing error code:{StatusCodes.Status404NotFound}");
                await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError($"Request failed on {nameof(UserNotFoundException)} with error {ex.Message} causing error code:{StatusCodes.Status404NotFound}");
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
