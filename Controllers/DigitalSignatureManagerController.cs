using iText.Signatures;
using KSU_BusinessProcesses.Services;
using Microsoft.AspNetCore.Mvc;

namespace KSU_BusinessProcesses.Controllers
{
    [Route("signature")]
    [ApiController]
    public class DigitalSignatureManagerController : ControllerBase
    {
        private readonly DigitalSignatureManager _digitalSignatureManager;

        public DigitalSignatureManagerController(DigitalSignatureManager digitalSignatureManager)
        {
            _digitalSignatureManager = digitalSignatureManager;
        }


        [HttpPost]
        public IActionResult SignPdf()
        {

            try
            {

                string inputFilePath = "C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb.pdf";
                string outputFilePath = "C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb_Signed.pdf";
                string certificatePath = "C:\\Users\\Admin\\Desktop\\X\\certificate.pfx";
                string certificatePassword = "12345";
                string signatureContainerFile = "C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb_Signed.sig";

                //_digitalSignatureManager.SignPdf1(inputFilePath, outputFilePath, certificatePath, certificatePassword);
                //_digitalSignatureManager.Sign();
                _digitalSignatureManager.SignPdf1(inputFilePath, outputFilePath, certificatePath, certificatePassword);
                return Ok("Подпись успешно добавлена к PDF.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }



            //try
            //{
            //    // Параметры для вызова метода SignPdf
            //string inputFilePath = "C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb.pdf";
            //string outputFilePath = "C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb_Signed.pdf";
            //string certificatePath = "C:\\Users\\Admin\\Desktop\\X\\certificate.pfx";
            //string certificatePassword = "12345";


            //    // Создание экземпляра PdfSigner и вызов метода SignPdf
            //    _digitalSignatureManager.SignPdf(inputFilePath, outputFilePath, certificatePath, certificatePassword);

            //    // Возвращаем успешный результат
            //    return Ok("Подпись успешно добавлена к PDF.");
            //}
            //catch (Exception ex)
            //{
            //    // В случае ошибки возвращаем ошибку сервера
            //    return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            //}
        }
    }
}
