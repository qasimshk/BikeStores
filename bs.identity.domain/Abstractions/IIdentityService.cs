using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.domain.Abstractions
{
    public interface IIdentityService
    {
        Task<HttpResponseMessage> GenerateToken(string email, string password, CancellationToken cancellationToken);
        Task<HttpResponseMessage> RefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}
