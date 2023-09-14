using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

public class EmailController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public EmailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("send-email")]
    public IActionResult SendEmail()
    {
        try
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpServer = smtpSettings["SmtpServer"];
            var port = int.Parse(smtpSettings["Port"]);
            var userName = smtpSettings["UserName"];
            var password = smtpSettings["Password"];

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true, 
            };

            var message = new MailMessage
            {
                From = new MailAddress(userName),   
                Subject = "Akceptacja wniosku",
                Body = "Wniosek zostal zaakceptowany ",
            };

            message.To.Add("19717@student.ans-elblag.pl");

            smtpClient.Send(message);

            return Ok("Wiadomość e-mail została wysłana.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Wystąpił błąd: {ex.Message}");
        }
    }
}