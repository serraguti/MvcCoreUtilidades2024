namespace MvcCoreUtilidades.Helpers
{
    //AQUI DEBERIAMOS TENER TODAS LAS CARPETAS QUE 
    //DESEEMOS QUE NUESTROS CONTROLLERS UTILICEN
    public enum Folders { Images=0, Facturas=1, Uploads=2, Temporal=3 }
    public class HelperPathProvider
    {
        //NECESITAMOS ACCEDER AL SISTEMA DE ARCHIVOS DEL WEB SERVER (wwwroot)
        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            //HttpContext.Request.Host????
            string serverUrl = "https://localhost:7290/";
            string urlPath = serverUrl + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}
