using System.Threading.Tasks;
using FM.Application.DTOs.Requests.Emails;

namespace FM.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
