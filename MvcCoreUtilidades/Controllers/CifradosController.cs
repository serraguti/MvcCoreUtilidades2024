using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult CifradoBasico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoBasico
            (string contenido, string resultado, string accion)
        {
            //CIFRAMOS EL CONTENIDO RECIBIDO
            string response =
                HelperCryptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }else if (accion.ToLower() == "comparar")
            {
                //COMPARAMOS LA RESPUESTA CIFRADA CON EL VALOR DE LA 
                //CAJA RESULTADO
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "Los valores no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "Contenidos iguales!!!";
                }
            }
            return View();
        }
    }
}
