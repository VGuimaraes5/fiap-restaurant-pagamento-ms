using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Domain.Services;

namespace Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            return _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}