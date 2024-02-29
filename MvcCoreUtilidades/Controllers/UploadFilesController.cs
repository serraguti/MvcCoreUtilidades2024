using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Runtime.InteropServices;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPathProvider;

        public UploadFilesController(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider; 
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string path = 
                this.helperPathProvider.MapPath
                (fichero.FileName, Folders.Uploads);
            //SUBIMOS EL FICHERO UTILIZANDO Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //MEDIANTE IFormFile COPIAMOS EL CONTENIDO DEL FICHERO
                //AL STREAM
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            string urlServer = HttpContext.Request.Scheme
                    + "://" + HttpContext.Request.Host;
            ViewData["TEST"] = 
                HttpContext.Request.Scheme
                    + "://" + HttpContext.Request.Host;
            string urlPath =
                this.helperPathProvider.MapUrlPath(fichero.FileName
                , Folders.Uploads);
            ViewData["URL"] = urlPath;
            return View();
        }
    }
}
