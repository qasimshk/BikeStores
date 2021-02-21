using System.Net;

namespace bs.identity.domain.Models
{
    public class UserLoginResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public int ExpireIn { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
