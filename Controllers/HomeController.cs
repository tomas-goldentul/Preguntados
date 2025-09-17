using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Preguntados.Models;

namespace Preguntados.Controllers;

public class HomeController : Controller
{
    Juego juego = new Juego();
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
        ViewBag.Dificultades = BD.ObtenerDificultades();
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View();
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        HttpContext.Session.SetString("user", username);
        HttpContext.Session.SetInt32("dificultad", dificultad);
        HttpContext.Session.SetInt32("categoria", categoria);
        juego.CargarPartida(username, dificultad, categoria);

       return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        Preguntas PreguntaActual = juego.ObtenerProximaPregunta();
        HttpContext.Session.SetString("preguntaActual", Objeto.ObjectToString(PreguntaActual));
        if (PreguntaActual != null)
        {
            List<Respuestas> proximasRespuestas = juego.ObtenerProximasRespuestas(PreguntaActual.IDpregunta);
            HttpContext.Session.SetString("proximasRespuestas", ObjetoList.ListToString(proximasRespuestas));
            return RedirectToAction("Jugar");
        }
        else
        {
           return RedirectToAction("Fin");
        }
    }
    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        ViewBag.VerificarRespuesta(idRespuesta);
        return View("respuesta");
    }
}
