using Microsoft.Extensions.Logging;

namespace Cubicfox.Domain.Common.Utils;

public static class LogHelper
{
    public static void LogRequest(ILogger logger, string? path, string? requestData)
    {

        logger.LogInformation(
            "Request Path: {path}\nRequest Data: {requestData}",
            path,
            requestData
        );
    }

    public static void LogResponse(ILogger logger, string? logMessage, string? responseData, int? statusCode)
    {

        logger.LogInformation(
            "{logMessage}\nResponse Data: {responseData}\nStatus Code: {statusCode}",
            logMessage,
            responseData,
            statusCode
        );
    }
}
