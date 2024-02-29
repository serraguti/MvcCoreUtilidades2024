using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailExampleController : Controller
    {
        private HelperUploadFiles helperUpload;
        private HelperMails helperMail;

        public MailExampleController
            (HelperUploadFiles helperUpload
            , HelperMails helperMail)
        {
            this.helperUpload = helperUpload;
            this.helperMail = helperMail;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail
            (string para, string asunto
            , string mensaje, IFormFile file)
        {
            if (file != null)
            {
                string path =
                    await this.helperUpload.UploadFileAsync(file, Folders.Mails);
                await this.helperMail.SendMailAsync(para, asunto, mensaje, path);
            }
            else
            {
                await this.helperMail.SendMailAsync(para, asunto, mensaje);
            }
            ViewData["MENSAJE"] = "Email enviado correctamente";
            return View();
        }
    }
}
