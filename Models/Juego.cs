using Preguntados.Models;

public class Juego
{
    public string? username { get; set; }
    public int puntuajeActual { get; set; }
    public int cantidadPreguntasCorrectas { get; set; }
    public int contadorNroPreguntaActual { get; set; }
    public Preguntas? PreguntaActual { get; set; }
    public List<Preguntas> ListaPreguntas { get; set; }
    public List<Respuestas> ListaRespuestas { get; set; }

    public Juego()
    {
        // Inicializar directamente los campos de clase (no variables locales)
        username = "";
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = new List<Preguntas>();
        ListaRespuestas = new List<Respuestas>();
    }

    private void InicializarJuego()
    {
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = new List<Preguntas>();
        ListaRespuestas = new List<Respuestas>();
    }

    public void CargarPartida(string username, int Dificultad, int Categoria)
    {
        this.username = username; // Asignar el username recibido
        InicializarJuego();
        this.username = username; // Volver a asignar después de inicializar
        ListaPreguntas = BD.ObtenerPreguntas(Dificultad, Categoria);
        ListaRespuestas = new List<Respuestas>(); // Inicializar la lista de respuestas
        
        // Debug: Verificar que se cargaron las preguntas
        System.Diagnostics.Debug.WriteLine($"Preguntas cargadas: {ListaPreguntas?.Count ?? 0}");
    }

    public List<Categorias> ObtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }

    public List<Dificultades> ObtenerDificultades()
    {
        return BD.ObtenerDificultades();
    }

    public List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerProximasRespuestas(idPregunta);
        return ListaRespuestas ?? new List<Respuestas>();
    }

    public bool VerificarRespuesta(int IDrespuesta)
    {
        bool correcta = false;
        
        // Obtener las respuestas directamente de la base de datos para la pregunta actual
        if (PreguntaActual != null)
        {
            List<Respuestas> respuestasPreguntaActual = BD.ObtenerProximasRespuestas(PreguntaActual.IDpregunta);
            
            foreach (Respuestas respuesta in respuestasPreguntaActual)
            {
                if (respuesta.IDrespuesta == IDrespuesta)
                {
                    correcta = respuesta.Correcta;
                    break; // Salir del loop una vez encontrada
                }
            }
        }

        if (correcta)
        {
            puntuajeActual = puntuajeActual + 60;
            cantidadPreguntasCorrectas++;
        }
        
        contadorNroPreguntaActual++;
        return correcta;
    }

    public Preguntas? ObtenerProximaPregunta()
    {
        System.Diagnostics.Debug.WriteLine($"ListaPreguntas es null: {ListaPreguntas == null}");
        System.Diagnostics.Debug.WriteLine($"Contador actual: {contadorNroPreguntaActual}");
        System.Diagnostics.Debug.WriteLine($"Total preguntas: {ListaPreguntas?.Count ?? 0}");
        
        if (ListaPreguntas != null && contadorNroPreguntaActual < ListaPreguntas.Count)
        {
            PreguntaActual = ListaPreguntas[contadorNroPreguntaActual];
            System.Diagnostics.Debug.WriteLine($"Pregunta obtenida: {PreguntaActual?.Enunciado}");
            return PreguntaActual;
        }
        System.Diagnostics.Debug.WriteLine("No hay más preguntas o ListaPreguntas es null");
        return null; // No hay más preguntas
    }

    // Propiedades públicas para acceder a los datos si es necesario
    public string? Username => username;
    public int PuntuajeActual => puntuajeActual;
    public int CantidadPreguntasCorrectas => cantidadPreguntasCorrectas;
    public int ContadorNroPreguntaActual => contadorNroPreguntaActual;
}