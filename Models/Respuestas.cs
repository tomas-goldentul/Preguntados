public class Respuestas
{
    public int IDrespuesta { get; set; }
    public int IDpregunta { get; set; }
    public int Opcion { get; set; }
    public string Contenido { get; set; }
    public bool Correcta { get; set; }
    public string Foto { get; set; }

    public Respuestas(int IDrespuesta, int IDpregunta, int Opcion, string Contenido, bool Correcta, string Foto)
    {
        this.IDrespuesta = IDrespuesta;
        this.IDpregunta = IDpregunta;
        this.Opcion = Opcion;
        this.Contenido = Contenido;
        this.Correcta = Correcta;
        this.Foto = Foto;

    }

}