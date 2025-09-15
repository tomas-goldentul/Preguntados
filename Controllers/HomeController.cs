using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Preguntados.Models;

namespace Preguntados.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        ViewBag.ObtenerDificultades();
        ViewBag.ObtenerCategorias();
        return View();
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        HttpContext.Session.SetString("user", username);
        HttpContext.Session.SetInt32("dificultad", dificultad);
        HttpContext.Session.SetInt32("categoria", categoria);
       return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        Preguntas PreguntaActual = Juego.ObtenerProximaPregunta();
        HttpContext.Session.SetString("preguntaActual", Objeto.ObjectToString(PreguntaActual));
        if (PreguntaActual != null)
        {
                List<Respuestas> proximasRespuestas = Juego.ObtenerProximasRespuestas(idPregunta);
                HttpContext.Session.SetString("proximasRespuestas", ObjetoList.ListToString(proximasRespuestas));
            return RedirectToAction("Jugar");
        }
        else
        {
           return RedirectToAction("Fin");
        }
    }
    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta){
        ViewBag.VerificarRespuesta(idRespuesta);
        return View("respuesta");
    }
}
