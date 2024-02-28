using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            //RECUPERAMOS LA RUTA DE NUESTRO SERVER
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string fileName = fichero.FileName;
            //NECESITAMOS LA RUTA FISICA PARA PODER ESCRIBIR EL FICHERO
            //LA RUTA ES LA COMBINACION DE TEMPFOLDER Y FILENAME
            // C:\Documents\Temp\file1.txt
            // /var/documents/temp/file1.txt
            //CUANDO ESTEMOS HABLANDO DE FILES (System.IO) PARA 
            //ACCEDER A RUTAS, SIEMPRE DEBEMOS UTILIZAR Path.Combine
            string path = Path.Combine(rootFolder, "users", fileName);
            //SUBIMOS EL FICHERO UTILIZANDO Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //MEDIANTE IFormFile COPIAMOS EL CONTENIDO DEL FICHERO
                //AL STREAM
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
