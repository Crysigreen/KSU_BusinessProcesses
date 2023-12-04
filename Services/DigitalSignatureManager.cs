

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Signatures;
using GroupDocs.Signature.Domain;
using GroupDocs.Signature.Options.Appearances;
using GroupDocs.Signature.Options;
using GroupDocs.Signature;
using Org.BouncyCastle.Pkcs;
using static iText.Signatures.PdfSigner;
using iText.Bouncycastle.Crypto;
using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;
using Org.BouncyCastle.Cms;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.Layout;
using iText.IO.Image;
using iText.Layout.Properties;





namespace KSU_BusinessProcesses.Services
{
    public class DigitalSignatureManager
    {
        public void SignPdf1(string inputPdf, string outputPdf, string pfxFilePath, string pfxPassword)
        {
            using (PdfReader reader = new PdfReader(inputPdf))
            {
                Pkcs12Store pk12 = new Pkcs12StoreBuilder().Build();
                pk12.Load(new FileStream(pfxFilePath, FileMode.Open, FileAccess.Read), pfxPassword.ToCharArray());
                string alias = null;
                foreach (var a in pk12.Aliases)
                {
                    alias = ((string)a);
                    if (pk12.IsKeyEntry(alias))
                        break;
                }

                ICipherParameters pk = pk12.GetKey(alias).Key;
                X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
                X509Certificate[] chain = new X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                {
                    chain[k] = ce[k].Certificate;
                }

                PdfSigner signer = new PdfSigner(reader, new FileStream(outputPdf, FileMode.Create), new StampingProperties());

                Rectangle rect = new Rectangle(200, 50, 200, 100);
                PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
                appearance
                    .SetReason("Test")
                    .SetContact("John Doe")
                    .SetLocation("Moscow")
                    .SetFontFamily("Arial")
                    .SetReuseAppearance(false)
                    .SetPageRect(rect)
                    .SetPageNumber(1);
                signer.SetFieldName("sig");

                


                IExternalSignature pks = new PrivateKeySignature(new PrivateKeyBC(pk), DigestAlgorithms.SHA256);

                IX509Certificate[] certificateWrappers = new IX509Certificate[chain.Length];
                for (int i = 0; i < certificateWrappers.Length; ++i)
                {
                    certificateWrappers[i] = new X509CertificateBC(chain[i]);
                }

                signer.SignDetached(pks, certificateWrappers, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                reader.Close();
            }
        }



        

       



        //public void Sign()
        //{
        //    // Подпишите PDF с помощью цифрового сертификата на C# с настраиваемым внешним видом и настройкой
        //    using (Signature signature = new Signature("C:\\Users\\Admin\\Desktop\\X\\Zadachi_Comb.pdf"))
        //    {
        //        DigitalSignOptions options = new DigitalSignOptions("C:\\Users\\Admin\\Desktop\\X\\certificate.pfx")
        //        {
        //            Password = "12345",
        //            // сведения о цифровом сертификате
        //            Location = "Moscow",
        //            ImageFilePath = "C:\\Users\\Admin\\Desktop\\X\\1stable-diffusion-xl.jpeg",
        //            Left = 50,                    // Set signature position
        //            Top = 50,
        //            // Пользовательский внешний вид подписи PDF
        //            Appearance = new PdfDigitalSignatureAppearance()
        //            {
        //                // Не показывать контактную информацию
        //                ContactInfoLabel = string.Empty,


        //                // Изменить метку местоположения
        //                LocationLabel = "From",
        //                DigitalSignedLabel = "By",
        //                DateSignedAtLabel = "On",

        //            },
        //            AllPages = true,
        //            Width = 250,
        //            Height = 80,
        //            // Установить границу подписи
        //            Border = new Border()
        //            {
        //                Visible = true,
        //                Color = Color.Blue,
        //                DashStyle = DashStyle.DashDot,
        //                Weight = 2,

        //            }
        //        };
        //        SignResult signResult = signature.Sign("C:\\Users\\Admin\\Desktop\\X\\signedDocument.pdf", options);
        //    }

        //}








    }
}
