public class Parida
{
    public int categoria { get; set; }
    public int dificultad { get; set; }
    public string username { get; set; }

    public Preguntas(int categoria, int dificultad, string username)
    {
        this.categoria = categoria;
        this.dificultad = dificultad;
        this.username = username;
    }
}