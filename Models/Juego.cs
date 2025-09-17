public class Juego{
    private string username;
    private int puntuajeActual;
    private int cantidadPreguntasCorrectas;
    private int contadorNroPreguntaActual;
    private Preguntas PreguntaActual;
    private List<Preguntas> ListaPreguntas;
    private List<Respuestas> ListaRespuestas;

public Juego(){
    string username;
    int puntuajeActual;
    int cantidadPreguntasCorrectas;
    int contadorNroPreguntaActual;
    Preguntas PreguntaActual = new Preguntas(0, 0, 0, null, null);
    List<Preguntas> ListaPreguntas = new List<Preguntas>();
    List<Respuestas> ListaRespuestas = new List<Respuestas>();
}
private void InicializarJuego(){
    username = null;
    puntuajeActual = 0;
    cantidadPreguntasCorrectas = 0;
    contadorNroPreguntaActual= 0;
    PreguntaActual = null;
    ListaPreguntas = null;
    ListaRespuestas = null;
}
    public void CargarPartida(string username, int Dificultad, int Categoria){
        InicializarJuego();
        ListaPreguntas = BD.ObtenerPreguntas(Dificultad, Categoria);
    }
    public List<Categorias> ObtenerCategorias (){
        return BD.ObtenerCategorias();
    }
    public List<Dificultades> ObtenerDificultades (){
        return BD.ObtenerDificultades();
    }
    public List<Respuestas> ObtenerProximasRespuestas(int idPregunta){
        return BD.ObtenerProximasRespuestas(idPregunta);
    }
    public bool VerificarRespuesta(int IDrespuesta){
        bool correcta = false;
        foreach(Respuestas respuesta in ListaRespuestas){
            if (respuesta.IDrespuesta == IDrespuesta){
                correcta = respuesta.Correcta;
            }
        }
        if(correcta){
            puntuajeActual = puntuajeActual + 60;
            cantidadPreguntasCorrectas++;
        }
        contadorNroPreguntaActual++;
        return correcta;
    }
    public Preguntas ObtenerProximaPregunta(){
        PreguntaActual = ListaPreguntas[contadorNroPreguntaActual];
        return PreguntaActual;
    }
}