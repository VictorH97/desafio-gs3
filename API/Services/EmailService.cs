using API.Models.Request;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendContactEmailAsync(ContactRequest request)
        {
            try
            {
                // Get SMTP settings from configuration
                var smtpServer = _configuration["Email:SmtpServer"] ?? "mail.greeceaviation.com";
                var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
                var smtpUsername = _configuration["Email:Username"];
                var smtpPassword = _configuration["Email:Password"];

                if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogError("Email configuration is missing");
                    return false;
                }

                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword)
                };

                using var message = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "Contact Greece Aviation"),
                    Subject = "Greece Aviation - Contact Form Submission",
                    Body = CreateEmailBody(request),
                    IsBodyHtml = true
                };

                // Add recipient
                message.To.Add(smtpUsername);

                // Add attachments
                for (int i = 0; i < request.Files.Count; i++)
                {
                    try
                    {
                        var fileBytes = Convert.FromBase64String(request.Files[i]);
                        var attachment = new Attachment(new MemoryStream(fileBytes), $"attachment_{i + 1}.pdf", "application/pdf");
                        message.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Failed to attach file {i + 1}: {ex.Message}");
                    }
                }

                await client.SendMailAsync(message);
                _logger.LogInformation("Contact email sent successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send contact email: {ex.Message}");
                return false;
            }
        }

        private static string CreateEmailBody(ContactRequest request)
        {
            var body = new StringBuilder();
            body.AppendLine("<html><body>");
            body.AppendLine("<h2>New Contact Form Submission</h2>");
            body.AppendLine("<table style='border-collapse: collapse; width: 100%;'>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Name:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Name}</td></tr>");
            
            if (!string.IsNullOrEmpty(request.Company))
            {
                body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Company:</td>");
                body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Company}</td></tr>");
            }
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Email:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Email}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Phone:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Phone}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Interest:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Interest}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Model:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Model}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Year:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Year}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Total Hours:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.TotalHours}</td></tr>");
            
            body.AppendLine("<tr><td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Message:</td>");
            body.AppendLine($"<td style='padding: 8px; border: 1px solid #ddd;'>{request.Message}</td></tr>");
            
            body.AppendLine("</table>");
            body.AppendLine("</body></html>");

            return body.ToString();
        }
    }
} 