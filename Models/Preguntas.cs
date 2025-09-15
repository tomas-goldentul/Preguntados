public class Preguntas
{
    public int IDcategoria { get; set; }
    public int IDpregunta { get; set; }
    public int IDdificultad { get; set; }
    public string Enunciado { get; set; }
    public string Foto { get; set; }

    public Preguntas(int IDcategoria, int IDpregunta, int IDdificultad, string Enunciado, string Foto)
    {
        this.IDcategoria = IDcategoria;
        this.IDpregunta = IDpregunta;
        this.IDdificultad = IDdificultad;
        this.Enunciado = Enunciado;
        this.Foto = Foto;

    }

}