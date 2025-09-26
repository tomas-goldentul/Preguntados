using Preguntados.Models;

public class Juego
{
    private string? username;
    private int puntuajeActual;
    private int cantidadPreguntasCorrectas;
    private int contadorNroPreguntaActual;
    private Preguntas? PreguntaActual;
    private List<Preguntas>? ListaPreguntas;
    private List<Respuestas>? ListaRespuestas;

    public Juego()
    {
        // Inicializar directamente los campos de clase (no variables locales)
        username = string.Empty;
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = new List<Preguntas>();
        ListaRespuestas = new List<Respuestas>();
    }

    private void InicializarJuego()
    {
        username = null;
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null;
        ListaRespuestas = null;
    }

    public void CargarPartida(string username, int Dificultad, int Categoria)
    {
        this.username = username; // Asignar el username recibido
        InicializarJuego();
        this.username = username; // Volver a asignar después de inicializar
        ListaPreguntas = BD.ObtenerPreguntas(Dificultad, Categoria);
        ListaRespuestas = new List<Respuestas>(); // Inicializar la lista de respuestas
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
        
        if (ListaRespuestas != null)
        {
            foreach (Respuestas respuesta in ListaRespuestas)
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
        if (ListaPreguntas != null && contadorNroPreguntaActual < ListaPreguntas.Count)
        {
            PreguntaActual = ListaPreguntas[contadorNroPreguntaActual];
            return PreguntaActual;
        }
        return null; // No hay más preguntas
    }

    // Propiedades públicas para acceder a los datos si es necesario
    public string? Username => username;
    public int PuntuajeActual => puntuajeActual;
    public int CantidadPreguntasCorrectas => cantidadPreguntasCorrectas;
    public int ContadorNroPreguntaActual => contadorNroPreguntaActual;
}