using System.Net;
using Cubicfox.Application.Service.TimeLog.Service;
using Cubicfox.Application.Service.Zenquotes.Service;
using Cubicfox.Domain.Common.Exceptions;
using Moq;
using Moq.Protected;

namespace Cubicfox.Unittest.TestCases;

public class ZenqoutesServiceTest
{
    private ZenquotesService _zenquotesService;

    [Fact]
    public async Task GetZenquoutesRandomAsync_ReturnsDescription()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    "[ {\"q\":\"If you're brave enough to say goodbye, life will reward you with a new hello.\",\"a\":\"Paulo\",\"h\":\"<blockquote>&ldquo;If you're brave enough to say goodbye, life will reward you with a new hello.&rdquo; &mdash; <footer>Paulo</footer></blockquote>\"} ]")
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        client.BaseAddress = new Uri("https://zenquotes.io/api/random");
        
        mockFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

        _zenquotesService = new ZenquotesService(mockFactory.Object);

        // Act
        var result = await _zenquotesService.GetAsync(default);

        // Assert
        Assert.Equal("If you're brave enough to say goodbye, life will reward you with a new hello.", result.Description);
    }
    
    [Fact]
    public async Task GetZenquoutesRandomAsync_HttpStatusCodeUnathorized_ReturnsEmptyDescription()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(
                    "[ {\"q\":\"If you're brave enough to say goodbye, life will reward you with a new hello.\",\"a\":\"Paulo\",\"h\":\"<blockquote>&ldquo;If you're brave enough to say goodbye, life will reward you with a new hello.&rdquo; &mdash; <footer>Paulo</footer></blockquote>\"} ]")
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        client.BaseAddress = new Uri("https://zenquotes.io/api/random");
        
        mockFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

        _zenquotesService = new ZenquotesService(mockFactory.Object);

        // Act
        var result = await _zenquotesService.GetAsync(default);

        // Assert
        Assert.Equal(String.Empty, result.Description);
    }
    
    [Fact]
    public async Task GetZenquoutesRandomAsync_NoBasaeAddress_ThrowException()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(
                    "[ {\"q\":\"If you're brave enough to say goodbye, life will reward you with a new hello.\",\"a\":\"Paulo\",\"h\":\"<blockquote>&ldquo;If you're brave enough to say goodbye, life will reward you with a new hello.&rdquo; &mdash; <footer>Paulo</footer></blockquote>\"} ]")
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        mockFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

        _zenquotesService = new ZenquotesService(mockFactory.Object);

        // Act - Assert
        await Assert.ThrowsAsync<CubicfoxException>(() => _zenquotesService.GetAsync(default));  
    }
}
