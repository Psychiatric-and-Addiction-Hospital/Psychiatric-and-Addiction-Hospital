
using Microsoft.AspNetCore.Http;

namespace Application.DTOS.Request.Profile
{
    public class ChangeProfileImageRequest
    {
        public IFormFile ImageUrl { get; set; }
    }
}
