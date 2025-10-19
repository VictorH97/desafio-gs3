using API.Models.Request;

namespace API.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactEmailAsync(ContactRequest request);
    }
} 