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

    [HttpPost]
    public IActionResult Comenzar(int Dificultad, int Categoria, string User)
    {
        if (string.IsNullOrEmpty(User))
        {
            return BadRequest("El nombre de usuario no puede estar vacío.");
        }

        Partida partida = new Partida(Categoria, Dificultad, User);

        string? juegoString = HttpContext.Session.GetString("juego");

        if (string.IsNullOrEmpty(juegoString))
        {
            return BadRequest("No se encontró el objeto 'juego' en la sesión.");
        }

        Juego? juego = Objeto.StringToObject<Juego>(juegoString);
        if (juego == null)
        {
            return BadRequest("Error al deserializar el juego.");
        }

        HttpContext.Session.SetString("user", partida.username);
        HttpContext.Session.SetInt32("dificultad", partida.dificultad);
        HttpContext.Session.SetInt32("categoria", partida.categoria);

        juego.CargarPartida(partida.username, partida.dificultad, partida.categoria);

        // Guardar el juego actualizado en la sesión
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));

        return RedirectToAction("Jugar");
    }

    public IActionResult Jugar()
    {
        string? juegoString = HttpContext.Session.GetString("juego");
        if (string.IsNullOrEmpty(juegoString))
        {
            return RedirectToAction("Index");
        }

        Juego? juego = Objeto.StringToObject<Juego>(juegoString);
        if (juego == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.user = HttpContext.Session.GetString("user");
        Preguntas? PreguntaActual = juego.ObtenerProximaPregunta();
        ViewBag.preguntaActual = PreguntaActual;

        if (PreguntaActual != null)
        {
            HttpContext.Session.SetString("preguntaActual", Objeto.ObjectToString(PreguntaActual));
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
        string? juegoString = HttpContext.Session.GetString("juego");
        if (string.IsNullOrEmpty(juegoString))
            return RedirectToAction("Index");

        Juego? juego = Objeto.StringToObject<Juego>(juegoString);
        if (juego == null)
            return RedirectToAction("Index");

        // Recuperar la pregunta actual desde la sesión
        string? preguntaActualString = HttpContext.Session.GetString("preguntaActual");
        if (!string.IsNullOrEmpty(preguntaActualString))
        {
            juego.PreguntaActual = Objeto.StringToObject<Preguntas>(preguntaActualString);
        }

        bool esCorrecta = juego.VerificarRespuesta(idRespuesta);
        ViewBag.esCorrecta = esCorrecta;

        // Guardar el juego actualizado en la sesión
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));

        return View("Respuesta");
    }

    public IActionResult Fin()
    {
        string? juegoString = HttpContext.Session.GetString("juego");
        if (string.IsNullOrEmpty(juegoString))
            return RedirectToAction("Index");

        Juego? juego = Objeto.StringToObject<Juego>(juegoString);
        if (juego == null)
            return RedirectToAction("Index");

        ViewBag.Username = juego.Username ?? "Invitado";
        ViewBag.Puntaje = juego.PuntuajeActual;

        return View();
    }


}