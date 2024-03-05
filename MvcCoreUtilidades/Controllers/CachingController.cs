using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        //PODEMOS INDICAR EL TIEMPO EN SEGUNDOS PARA QUE 
        //RESPONDA DE NUEVO AL ACTION
        [ResponseCache(Duration = 15,
            Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha =
                DateTime.Now.ToLongDateString() + " -- "
                + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }


        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            //LA PRIMERA VEZ NO RECIBIMOS TIEMPO
            //VAMOS A PONER UN TIEMPO DE 60 SEGUNDOS
            if (tiempo == null)
            {
                tiempo = 5;
            }
            string fecha =
                DateTime.Now.ToLongDateString() + " -- "
                + DateTime.Now.ToLongTimeString();
            //PREGUNTAMOS SI EXISTE ALGO EN CACHE O NO EXISTE
            if (this.memoryCache.Get("FECHA") == null)
            {
                //NO EXISTE NADA EN CACHE TODAVIA
                //CREAMOS LAS OPCIONES PARA EL CACHE CON TIEMPO
                MemoryCacheEntryOptions options =
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                this.memoryCache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "Almacenando en Cache";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                //TENEMOS LA FECHA EN CACHE
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Recuperando de Cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }
    }
}
