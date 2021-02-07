using System.Net;

namespace bs.identity.domain.Models
{
    public class UserLoginResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public int ExpireIn { get; set; }
        public string Token { get; set; }
    }
}
