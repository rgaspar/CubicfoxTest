using System.Net.Http.Json;
using Cubicfox.Application.Service.Zenquotes.Interface;
using Cubicfox.Domain.Common.Constants;
using Cubicfox.Domain.Common.Exceptions;
using Cubicfox.Domain.Common.Models;
using Cubicfox.Domain.Common.Response.Zenquotes;

namespace Cubicfox.Application.Service.Zenquotes.Service;

public class ZenquotesService : IZenquotesService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ZenquotesService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<ZenquotesResponse> GetAsync(CancellationToken token)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ZenquotesAPI");
            var response = await client.GetAsync($"random");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ZenquotesRandom>>();
                if (result != null)
                {
                    return await Task.FromResult(new ZenquotesResponse() { Description = result.First().Quotes });
                }
            }
            return await Task.FromResult(new ZenquotesResponse() { Description = string.Empty });        
        }
        catch (Exception ex)
        {
            throw new CubicfoxException(ErrorCode.Zenquote, "Zenquote API error", "Zenquote API error", ex);
        }
    }
}
