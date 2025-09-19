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
        HttpContext.Session.SetString("partida", username, dificultad, categoria);
        HttpContext.Session.SetString("user", username);
        HttpContext.Session.SetInt32("dificultad", dificultad);
        HttpContext.Session.SetInt32("categoria", categoria);
        juego.CargarPartida(username, dificultad, categoria);

       return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        ViewBag.user = HttpContext.Session.GetString("user");
        Preguntas PreguntaActual = juego.ObtenerProximaPregunta();
        ViewBag.preguntaActual = PreguntaActual;
        HttpContext.Session.SetString("preguntaActual", Objeto.ObjectToString(PreguntaActual));
        if (PreguntaActual != null)
        {
            List<Respuestas> proximasRespuestas = juego.ObtenerProximasRespuestas(PreguntaActual.IDpregunta);
            HttpContext.Session.SetString("proximasRespuestas", ObjetoList.ListToString(proximasRespuestas));
            ViewBag.proximasRespuestas = proximasRespuestas;
            return View();
        }
        else
        {
           return RedirectToAction("Fin");
        }
    }
    public IActionResult CargarPartida(string username){
        ViewBag.user = HttpContext.Session.GetString("user");
        
    }
    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        ViewBag.VerificarRespuesta(idRespuesta);
        return View("respuesta");
    }
}
