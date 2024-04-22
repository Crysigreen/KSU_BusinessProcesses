using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using MailKit.Security;

namespace KSU_BusinessProcesses.Services
{
    public class MailingService
    {

        string htmlContent = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>KSUAngularProject</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    background-color: #f4f4f4;
                }
                .container {
                    min-height: 60px;
                    max-width: 400px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #9d9d9d;
                    border-radius: 16px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    text-align: center;
                }
                .header {
                    background-color: #007bff;
                    color: #fff;
                    padding: 20px 10px 10px 10px;
                    border-top-left-radius: 10px;
                    border-top-right-radius: 10px;
                }
                h1 {
                    margin-bottom: 30px;
                    font-size: 24px;
                }
                p {
                    margin: 20px;
                    line-height: 1.6;
                    font-size: 19px;
                }
                .signature {
                    margin-top: 40px;
                    font-style: italic;
                    color: #777;
                }
                .footer {
                    background-color: #007bff;
                    border-bottom-left-radius: 8px;
                    border-bottom-right-radius: 8px;
                    padding: 10px;
                    color: #fff;
                }
                .text
                {
                    border: 3px solid #007bff;
                    padding: 50px;
                }

                .container-header {
                    min-height: 20px;
                    max-width: 400px;
                    margin: 20px auto;
                    padding: 5px 20px 5px 20px;
                    background-color: #fff;
                    border-radius: 16px;
                    box-shadow: 0 0 10px 5px rgba(221, 221, 221, 1);
                    text-align: center;
                    background-color: #007bff;

                }

                .container-message {
                    min-height: 160px;
                    max-width: 400px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 16px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }

                .container-support {
                    min-height: 60px;
                    max-width: 400px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 16px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
                .container-buttom {
                    min-height: 80px;
                    max-width: 400px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 16px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
                .header-message
                {
                    color: white;
                    padding: 5px;
                }
                .question-message
                {
                    font-size: 25px;
                }
                .flex-container
                {
                    display: flex; /* Используем flex-контейнер */
                    justify-content: space-between; /* Распределяем элементы по горизонтали */
                }
                .column {
                    width: 50%; /* Ширина каждой колонки */
                }
                .ksu-class
                {
                    font-size: 15px;
                }
                p.ksu-class-img
                {
                    display: inline-block;
                    margin: 1px;
                    line-height: 1.6;
                    font-size: 19px;
                }
                .messangers
                {
                    margin-left: 20px;
                }
                .ksu-class-header
                {
                    font-size: 24px;
                }
            </style>
        </head>
        <body>
            <div class=""container"">
                <p class=""header-ksu"">КСУ Бизнес-процесами компании</p>
            </div>
            <div class=""container-header"">
                <p class=""header-message"">У Вас новое сообщение!</p>
            </div>
            <div class=""container-message"">
                <p>Добрый день!</p>
                <p>Вы являетесь сотрудником компании, роэтому Вы получили данное письмо-уведомление. Была создана новая проектная заявка <a href="""">Перейти</a>, чтобы просмотреть. </p>
            </div>
            <div class=""container-support"">
                <p class=""question-message"">Остались вопросы?</p>
                <p>Eсли у вас остались вопросы, обратитесь в <a href="""">техническую поддержку</a>.</p>
            </div>
            <div class=""container-buttom"">
                <p class=""ksu-class-header"">Команда КСУ Бизнес-процессов</p>
                <div class=""flex-container"">
                    <div class=""column"">
                        <p class=""ksu-class"">© КСУ Бизнес-процессов компании.</p>
                        <p class=""ksu-class"">Все права защищены</p>
                    </div>
                    <div class=""column"">
                        <p class=""ksu-class"">Политика конфиденциальности</p>
                        <div class=""messangers"">
                            <p class=""ksu-class-img""><a target=""_blank"" href=""#"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Telegram"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=MaBa21nix8wCshQwkTybZQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy90ZzIucG5n"" alt=""Telegram"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
                            <p class=""ksu-class-img""><a target=""_blank"" href=""#""><img title=""Vkontakte"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=UJP8TPqt42IN3FPCabK9wQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy92azUucG5n"" alt=""VK"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
                            <p class=""ksu-class-img""><a target=""_blank"" href=""#"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Yandex.Dzen"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=RiM_6kpF2rHrjuC1kGru-w&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy96ZW4yLnBuZw~~"" alt=""Yandex.Dzen"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
                        </div>
                    </div>
                </div>
            </div>

        </body>
        </html>

        ";

        private readonly IConfiguration _configuration;

        public MailingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            string smtpServer = _configuration["SmtpSettings:Server"];
            int port = int.Parse(_configuration["SmtpSettings:Port"]);
            string username = _configuration["SmtpSettings:Username"];
            string password = _configuration["SmtpSettings:Password"];

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("",username));
                message.To.Add(new MailboxAddress("",to));
                message.Subject = subject;

               
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, port, true);
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("Письмо успешно отправлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
            }
        }



    }
}
