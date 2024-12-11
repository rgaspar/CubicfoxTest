using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Cubicfox.Domain.Common;
using Cubicfox.Domain.Common.Constants;
using Cubicfox.Domain.Common.Exceptions;
using Cubicfox.Domain.Common.Utils;
using Microsoft.AspNetCore.Diagnostics;

namespace Cubicfox.Extensions;

public static class ExceptionMiddlewareExtensions
{

    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {

        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorId = Guid.NewGuid();

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    string errorMessage = string.Empty;
                    string errorCode = string.Empty;

                    if (contextFeature.Error is CubicfoxException cubicfoxException)
                    {
                        switch (cubicfoxException.ErrorCode)
                        {
                            case ErrorCode.NotFound:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.NOT_FOUND}";
                                break;
                            case ErrorCode.VersionConflict:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.VERSION_CONFLICT}";
                                break;
                            case ErrorCode.TimerAlreadyRunning:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = cubicfoxException.UserFriendlyMessage;

                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.TIME_ALREADY_RUNNING}";
                                break;
                            case ErrorCode.Conflict:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = cubicfoxException.UserFriendlyMessage;

                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.CONFLICT}";
                                break;
                            case ErrorCode.BadRequest:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.BAD_REQUEST}";
                                break;
                            case ErrorCode.Unauthorized:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.UNAUTHORIZED}";
                                break;
                            case ErrorCode.Internal:
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.INTERNAL_ERROR}";
                                break;
                            case ErrorCode.UnprocessableEntity:
                                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.UNPROCESSABLE_ENTITY}";
                                break;
                            default:
                                context.Response.StatusCode = 500;
                                errorMessage = cubicfoxException.UserFriendlyMessage;
                                errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.GENERAL_ERROR}";
                                break;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        errorCode = $"{ApplicationConstants.Name}.{ErrorRespondCode.GENERAL_ERROR}";
                        errorMessage = "An error has occurred.";
                    }

                    await context.Response.WriteAsync(new Error(errorCode, errorMessage, errorId));
                    logger.LogError("ErrorId:{errorId} Exception:{contextFeature.Error}", errorId,
                        contextFeature.Error);
                }
            }
        });
    }
}
