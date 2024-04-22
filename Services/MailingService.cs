using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using MailKit.Security;

namespace KSU_BusinessProcesses.Services
{
    public class MailingService
    {

        //string htmlContent = @"
        //<!DOCTYPE html>
        //<html lang=""en"">
        //<head>
        //    <meta charset=""UTF-8"">
        //    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        //    <title>KSUAngularProject</title>
        //    <style>
        //        body {
        //            font-family: Arial, sans-serif;
        //            margin: 0;
        //            padding: 0;
        //            background-color: #f4f4f4;
        //        }
        //        .container {
        //            min-height: 60px;
        //            max-width: 400px;
        //            margin: 20px auto;
        //            padding: 20px;
        //            background-color: #9d9d9d;
        //            border-radius: 16px;
        //            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        //            text-align: center;
        //        }
        //        .header {
        //            background-color: #007bff;
        //            color: #fff;
        //            padding: 20px 10px 10px 10px;
        //            border-top-left-radius: 10px;
        //            border-top-right-radius: 10px;
        //        }
        //        h1 {
        //            margin-bottom: 30px;
        //            font-size: 24px;
        //        }
        //        p {
        //            margin: 20px;
        //            line-height: 1.6;
        //            font-size: 19px;
        //        }
        //        .signature {
        //            margin-top: 40px;
        //            font-style: italic;
        //            color: #777;
        //        }
        //        .footer {
        //            background-color: #007bff;
        //            border-bottom-left-radius: 8px;
        //            border-bottom-right-radius: 8px;
        //            padding: 10px;
        //            color: #fff;
        //        }
        //        .text
        //        {
        //            border: 3px solid #007bff;
        //            padding: 50px;
        //        }

        //        .container-header {
        //            min-height: 20px;
        //            max-width: 400px;
        //            margin: 20px auto;
        //            padding: 5px 20px 5px 20px;
        //            background-color: #fff;
        //            border-radius: 16px;
        //            box-shadow: 0 0 10px 5px rgba(221, 221, 221, 1);
        //            text-align: center;
        //            background-color: #007bff;

        //        }

        //        .container-message {
        //            min-height: 160px;
        //            max-width: 400px;
        //            margin: 20px auto;
        //            padding: 20px;
        //            background-color: #fff;
        //            border-radius: 16px;
        //            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        //        }

        //        .container-support {
        //            min-height: 60px;
        //            max-width: 400px;
        //            margin: 20px auto;
        //            padding: 20px;
        //            background-color: #fff;
        //            border-radius: 16px;
        //            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        //        }
        //        .container-buttom {
        //            min-height: 80px;
        //            max-width: 400px;
        //            margin: 20px auto;
        //            padding: 20px;
        //            background-color: #fff;
        //            border-radius: 16px;
        //            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        //        }
        //        .header-message
        //        {
        //            color: white;
        //            padding: 5px;
        //        }
        //        .question-message
        //        {
        //            font-size: 25px;
        //        }
        //        .flex-container
        //        {
        //            display: flex; /* Используем flex-контейнер */
        //            justify-content: space-between; /* Распределяем элементы по горизонтали */
        //        }
        //        .column {
        //            width: 50%; /* Ширина каждой колонки */
        //        }
        //        .ksu-class
        //        {
        //            font-size: 15px;
        //        }
        //        p.ksu-class-img
        //        {
        //            display: inline-block;
        //            margin: 1px;
        //            line-height: 1.6;
        //            font-size: 19px;
        //        }
        //        .messangers
        //        {
        //            margin-left: 20px;
        //        }
        //        .ksu-class-header
        //        {
        //            font-size: 24px;
        //        }
        //    </style>
        //</head>
        //<body>
        //    <div class=""container"">
        //        <p class=""header-ksu"">КСУ Бизнес-процесами компании</p>
        //    </div>
        //    <div class=""container-header"">
        //        <p class=""header-message"">У Вас новое сообщение!</p>
        //    </div>
        //    <div class=""container-message"">
        //        <p>Добрый день!</p>
        //        <p>Вы являетесь сотрудником компании, роэтому Вы получили данное письмо-уведомление. Была создана новая проектная заявка <a href="""">Перейти</a>, чтобы просмотреть. </p>
        //    </div>
        //    <div class=""container-support"">
        //        <p class=""question-message"">Остались вопросы?</p>
        //        <p>Eсли у вас остались вопросы, обратитесь в <a href="""">техническую поддержку</a>.</p>
        //    </div>
        //    <div class=""container-buttom"">
        //        <p class=""ksu-class-header"">Команда КСУ Бизнес-процессов</p>
        //        <div class=""flex-container"">
        //            <div class=""column"">
        //                <p class=""ksu-class"">© КСУ Бизнес-процессов компании.</p>
        //                <p class=""ksu-class"">Все права защищены</p>
        //            </div>
        //            <div class=""column"">
        //                <p class=""ksu-class"">Политика конфиденциальности</p>
        //                <div class=""messangers"">
        //                    <p class=""ksu-class-img""><a target=""_blank"" href=""#"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Telegram"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=MaBa21nix8wCshQwkTybZQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy90ZzIucG5n"" alt=""Telegram"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
        //                    <p class=""ksu-class-img""><a target=""_blank"" href=""#""><img title=""Vkontakte"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=UJP8TPqt42IN3FPCabK9wQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy92azUucG5n"" alt=""VK"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
        //                    <p class=""ksu-class-img""><a target=""_blank"" href=""#"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Yandex.Dzen"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=RiM_6kpF2rHrjuC1kGru-w&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy96ZW4yLnBuZw~~"" alt=""Yandex.Dzen"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></p>
        //                </div>
        //            </div>
        //        </div>
        //    </div>

        //</body>
        //</html>

        //";

        string htmlContent = @"
        <!doctype html>
        <html lang=""en"">
        <head>
          <meta charset=""utf-8"">
          <title>KSUAngularProject</title>
          <base href=""/"">
          <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
            <link rel=""stylesheet"" href=""email.css"">
          <link rel=""icon"" type=""image/x-icon"" href=""favicon.ico"">
          <script src=""https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"" integrity=""sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"" crossorigin=""anonymous""></script>
          <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js"" integrity=""sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+"" crossorigin=""anonymous""></script>
          <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN"" crossorigin=""anonymous"">
          <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"" integrity=""sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL"" crossorigin=""anonymous""></script>
          <link href='https://unpkg.com/boxicons@2.0.7/css/boxicons.min.css' rel='stylesheet'>
          <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">


        </head>

        <style>

          body {
            width:1000px;
            margin:0px auto;
          }


          .container {
            /*display: grid;*/
            align-items: center;
            grid-template-columns: 1fr 1fr 1fr;
            /*column-gap: 1px;*/
          }

          img {
            max-width: 100%;
            max-height:100%;
          }

          text_ann {
            font-size: 50px;
          }



          td.esdev-mso-td_mr_css_attr{
            margin: 0;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 50px;
            padding-right: 50px;
            background-color: #ffd91d;
            border-radius: 12px;

          }

          es-m-p30r_mr_css_attr{
            margin: 0;
            padding-top: 15px;
            padding-bottom: 20px;
            padding-left: 50px;
            padding-right: 50px;
            background-color: gray;
            border-radius: 20px;
          }

          es-m-p30_mr_css_attr{
            margin: 0;
            padding-top: 20px;
            padding-bottom: 20px;
            padding-right: 40px;
            padding-left: 50px;
            border-radius: 20px;
            background-color: #ffffff;
          }


        </style>
        <body>
        <div id='page_context'>
        <!--<div class=""container"">-->
        <!--  <div class=""image"">-->
        <!--    <img src=""https://www.mlanet.org/media/bppffzczv.jpg"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" class=""adapt-img_mr_css_attr"" width=""50"" height=""50"">-->
        <!--  </div>-->
        <!--  <div class=""text_ann"">-->
        <!--    <h1>КСУ Бизнес-процесами компании</h1>-->
        <!--  </div>-->
        <!--</div>-->


          <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
            <tr>
              <td class=""leftcol"">
                    <img src=""https://www.mlanet.org/media/bppffzczv.jpg"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" class=""adapt-img_mr_css_attr"" width=""50"" height=""50"">
              </td>
              <td valign=""top""><h1>КСУ Бизнес-процесами компании</h1></td>
            </tr>
          </table>





        <tbody><tr>
          <td align=""left"" style=""padding:0;margin:0;padding-left:50px;padding-right:50px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:480px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""center"" height=""20"" style=""padding:0;margin:0""></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td class=""esdev-adapt-off_mr_css_attr es-m-p30r_mr_css_attr es-m-p30l_mr_css_attr"" align=""left"" bgcolor=""#FFD91D"" style=""margin:0;padding-top:10px;padding-bottom:10px;padding-left:50px;padding-right:50px;background-color:#ffd91d;border-radius:12px"">
            <table cellpadding=""0"" cellspacing=""0"" class=""esdev-mso-table_mr_css_attr"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;width:480px"">
              <tbody><tr>
                <td class=""esdev-mso-td_mr_css_attr"" valign=""top"" style=""padding:0;margin:0"">
                  <table cellpadding=""0"" cellspacing=""0"" align=""left"" class=""es-left_mr_css_attr"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:left"">
                    <tbody><tr>
                      <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:30px"">
                        <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                          <tbody><tr>
                            <td align=""center"" style=""padding:0;margin:0;font-size:0px""><img src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=VcKh1P-ij72AHW0diGDyKw&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF80MjdjMWExOWUyOGY0ZjY4NjI1YmE1ZGFiYzI5MmRkMjZiNWUxZmE1ZGEyODNkNTE1ZTczNTM4Zjk2OGIzZmI4L2ltYWdlcy9tYWlsX291dGxpbmVfMjgucG5n"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""30"" height=""30""></td>
                          </tr>
                          </tbody></table></td>
                    </tr>
                    </tbody></table></td>
                <td style=""padding:0;margin:0;width:20px""></td>
                <td class=""esdev-mso-td_mr_css_attr"" valign=""top"" style=""padding:0;margin:0"">
                  <table cellpadding=""0"" cellspacing=""0"" class=""es-right_mr_css_attr"" align=""right"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:right"">
                    <tbody><tr>
                      <td align=""left"" style=""padding:0;margin:0;width:430px"">
                        <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                          <tbody><tr>
                            <td align=""left"" style=""padding:0;margin:0""><h6 style=""margin:0;line-height:28px;font-family:Z-Roboto, Roboto, Arial, sans-serif;font-size:18px;font-style:normal;font-weight:500;color:#111111"" class=""MsoNormal_mr_css_attr"">У Вас новое сообщение!</h6></td>
                          </tr>
                          </tbody></table></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td align=""left"" style=""padding:0;margin:0;padding-left:50px;padding-right:50px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:480px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""center"" height=""20"" style=""padding:0;margin:0""></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        </tbody>

        <tbody><tr>
          <td class=""es-m-p30r_mr_css_attr es-m-p30l_mr_css_attr"" align=""left"" bgcolor=""#ffffff"" style=""margin:0;padding-top:15px;padding-bottom:20px;padding-left:50px;padding-right:50px;background-color:#ffffff;border-radius:20px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:480px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" style=""padding:0;margin:0;padding-top:15px""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:28px;color:#111111;font-size:20px"" class=""MsoNormal_mr_css_attr"">Добрый день!</p></td>
                    </tr>
                    <tr>
                      <td align=""left"" style=""padding:0;margin:0;padding-top:20px""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:28px;color:#111111;font-size:20px"" class=""MsoNormal_mr_css_attr"">Вы являетесь сотрудником компании, поэтому Вы получили данное письмо-уведомление.
                        <strong>Была создана новая проектная заявка</strong>
                        <a target=""_blank"" href=""https://biz.mail.ru/docs/saas/WorkMail/Startup/mx-record/index.html?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email""
                           style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px""
                           class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer"">Перейти</a>, чтобы просмотреть.&nbsp;<br><br></p>
        </td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        </tbody>

        <tbody><tr>
          <td align=""left"" style=""padding:0;margin:0;padding-left:50px;padding-right:50px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:480px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""center"" height=""20"" style=""padding:0;margin:0""></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td class=""es-m-p30_mr_css_attr"" align=""left"" style=""margin:0;padding-top:20px;padding-bottom:20px;padding-right:40px;padding-left:50px;border-radius:20px;background-color:#ffffff"" bgcolor=""#ffffff"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:490px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" style=""padding:0;margin:0""><h3 style=""margin:0;line-height:38px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:28px;font-style:normal;font-weight:500;color:#333333"" class=""MsoNormal_mr_css_attr"">Остались вопросы?</h3></td>
                    </tr>
                    <tr>
                      <td align=""left"" style=""padding:0;margin:0;padding-bottom:5px;padding-top:15px""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:28px;color:#333333;font-size:20px"" class=""MsoNormal_mr_css_attr"">Если у вас остались вопросы, обратитесь в <a target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077ff;font-size:20px;line-height:28px"" href=""https://biz.mail.ru/docs/saas/Support/support/index.html?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer"">техническую поддержку</a>.</p></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td align=""left"" style=""padding:0;margin:0"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:580px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""center"" height=""20"" style=""padding:0;margin:0""></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        </tbody>

        <tbody><tr>
          <td class=""es-m-p30_mr_css_attr"" align=""left"" bgcolor=""#ffffff"" style=""padding:0;margin:0;padding-top:40px;padding-left:40px;padding-right:40px;background-color:#ffffff;border-radius:20px 20px 0px 0px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:500px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" class=""es-m-txt-l_mr_css_attr"" style=""padding:0;margin:0;padding-bottom:20px""><h3 style=""margin:0;line-height:29px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:24px;font-style:normal;font-weight:500;color:#111111"" class=""MsoNormal_mr_css_attr"">Команда КСУ Бизнес-процессов</h3></td>
                    </tr>
                    <tr>
                      <td align=""left"" style=""padding:0;margin:0;padding-top:10px""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:22px;color:#111111;font-size:18px"" class=""MsoNormal_mr_css_attr"">Вы получили это письмо, потому что зарегистрировались на сайте <a target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077ff;font-size:18px;line-height:22px"" href=""https://biz.mail.ru?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer"">biz.mail.ru</a>.</p></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td class=""es-m-p30_mr_css_attr"" align=""left"" bgcolor=""#ffffff"" style=""padding:0;margin:0;padding-top:20px;padding-left:40px;padding-right:40px;background-color:#ffffff"">
            <table cellpadding=""0"" cellspacing=""0"" align=""left"" class=""es-left_mr_css_attr"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:left"">
              <tbody><tr>
                <td class=""es-m-p20b_mr_css_attr"" align=""center"" valign=""top"" style=""padding:0;margin:0;width:220px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" style=""padding:0;margin:0""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:28px;color:#111111;font-size:18px"" class=""MsoNormal_mr_css_attr"">© КСУ Бизнес-процессов компании<br>Все права защищены</p></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table>
            <table cellpadding=""0"" cellspacing=""0"" class=""es-right_mr_css_attr"" align=""right"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:right"">
              <tbody><tr>
                <td align=""left"" style=""padding:0;margin:0;width:260px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" style=""padding:0;margin:0""><p style=""margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:28px;color:#111111;font-size:20px"" class=""MsoNormal_mr_css_attr""><a target=""_blank"" href=""https://biz.mail.ru/docs/saas/LegalInformation/privacy-policy/index.html?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#000000;font-size:18px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer"">Политика конфиденциальности</a></p></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td class=""es-m-p30_mr_css_attr"" align=""left"" bgcolor=""#ffffff"" style=""margin:0;padding-top:20px;padding-bottom:20px;padding-left:40px;padding-right:40px;background-color:#ffffff;border-radius:0px 0px 20px 20px"">
            <table cellpadding=""0"" cellspacing=""0"" class=""es-right_mr_css_attr"" align=""right"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:right"">
              <tbody><tr>
                <td align=""left"" class=""es-m-p20b_mr_css_attr"" style=""padding:0;margin:0;width:284px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""left"" class=""es-m-txt-c_mr_css_attr es-m-p10t_mr_css_attr es-m-p0b_mr_css_attr es-m-p20r_mr_css_attr es-m-p20l_mr_css_attr"" style=""padding:0;margin:0;padding-bottom:20px;padding-left:25px;padding-right:40px;font-size:0px"">
                        <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt_mr_css_attr es-social_mr_css_attr"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                          <tbody><tr>
                            <td align=""center"" valign=""top"" style=""padding:0;margin:0;padding-right:15px""><a target=""_blank"" href=""https://t.me/vk_workspace_official?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Telegram"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=MaBa21nix8wCshQwkTybZQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy90ZzIucG5n"" alt=""Telegram"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                            <td align=""center"" valign=""top"" style=""padding:0;margin:0;padding-right:15px""><a target=""_blank"" href=""https://vk.com/vkworkspace?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Vkontakte"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=UJP8TPqt42IN3FPCabK9wQ&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy92azUucG5n"" alt=""VK"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                            <td align=""center"" valign=""top"" style=""padding:0;margin:0;padding-right:15px""><a target=""_blank"" href=""https://dzen.ru/id/623c60032d06b359f85ab327?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Yandex.Dzen"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=RiM_6kpF2rHrjuC1kGru-w&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF8xYzU4OTZkYzU3ZmM2Njg4Y2YzMmIyNzk5MmE4MzMyZDcyY2IwMWI1OGRhNTJmZTM3NWUzZDMzNTcyYmIyOTc3L2ltYWdlcy96ZW4yLnBuZw~~"" alt=""Yandex.Dzen"" height=""32"" width=""33"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                            <td align=""center"" valign=""top"" style=""padding:0;margin:0""><a target=""_blank"" href=""https://biz.mail.ru/blog/?utm_campaign=ProblemsNotifier&amp;utm_medium=email_admin&amp;utm_source=email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:none;color:#0077FF;font-size:20px;line-height:28px"" class=""MsoNormal_mr_css_attr"" rel="" noopener noreferrer""><img title=""Linkedin"" src=""https://proxy.imgsmail.ru?e=1713968983&amp;email=gosha_pustovalov%40mail.ru&amp;flags=0&amp;h=l5Ww2IWvuNMNV8ljoPml5Q&amp;is_https=1&amp;url173=b3F4b2Muc3RyaXBvY2RuLmVtYWlsL2NvbnRlbnQvZ3VpZHMvQ0FCSU5FVF9iMjdhMTU5NzI1ODhlNGQ3NDlkODliZWNkNmMzN2ZlOC9pbWFnZXMvemVuMS5wbmc~"" alt=""In"" height=""32"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                          </tr>
                          </tbody></table></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table>
            <table cellpadding=""0"" cellspacing=""0"" align=""left"" class=""es-left_mr_css_attr"" role=""none"" style=""border-collapse:collapse;border-spacing:0px;float:left"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:196px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    </table></td>
              </tr>
              </tbody></table></td>
        </tr>
        <tr>
          <td align=""left"" style=""padding:0;margin:0;padding-left:50px;padding-right:50px"">
            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""border-collapse:collapse;border-spacing:0px"">
              <tbody><tr>
                <td align=""center"" valign=""top"" style=""padding:0;margin:0;width:480px"">
                  <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px"">
                    <tbody><tr>
                      <td align=""center"" height=""20"" style=""padding:0;margin:0""></td>
                    </tr>
                    </tbody></table></td>
              </tr>
              </tbody></table></td>
        </tr>
        </tbody>

        </div>
        </body>

        ";

        private readonly IConfiguration _configuration;

        public MailingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string to)
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
                message.Subject = "Новая проектная заявка";

               
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
