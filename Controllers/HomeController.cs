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
        Juego juego = new Juego();
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        ViewBag.Dificultades = BD.ObtenerDificultades();
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View();
    }
    public IActionResult Comenzar(string User, int dificultad, int categoria)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        HttpContext.Session.SetString("user", User);
        HttpContext.Session.SetInt32("dificultad", dificultad);
        HttpContext.Session.SetInt32("categoria", categoria);
        juego.CargarPartida(User, dificultad, categoria);

        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
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
    
    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        ViewBag.VerificarRespuesta(idRespuesta);
        return View("respuesta");
    }
}
