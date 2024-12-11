using Cubicfox.Domain.Common.Response.Zenquotes;

namespace Cubicfox.Application.Service.Zenquotes.Interface;

public interface IZenquotesService
{
    Task<ZenquotesResponse> GetAsync(CancellationToken token);
}
