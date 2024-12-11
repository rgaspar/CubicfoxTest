using Cubicfox.Domain.Common.Constants;

namespace Cubicfox.Domain.Common.Exceptions;

public class CubicfoxException : Exception
{
    public string UserFriendlyMessage { get; set; }
    public ErrorCode ErrorCode { get; set; }

    public CubicfoxException(ErrorCode errorCode, string userFriendlyMessage, Exception? innerException = null) : base(userFriendlyMessage, innerException)
    {
        ErrorCode = errorCode;
        UserFriendlyMessage = userFriendlyMessage;
    }
    public CubicfoxException(string message, string userFriendlyMessage, Exception? innerException = null) : base(message, innerException)
    {
        UserFriendlyMessage = userFriendlyMessage;
    }
    public CubicfoxException(ErrorCode errorCode, string message, string userFriendlyMessage, Exception? innerException = null) : base(message, innerException)
    {
        ErrorCode = errorCode;
        UserFriendlyMessage = userFriendlyMessage;
    }
}
