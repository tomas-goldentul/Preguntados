class Dificultades
{
    public int IDdificultad { get; set; }
    public string Nombre { get; set; }

    public Dificultades(int IDdificultad, string Nombre)
    {
        this.IDdificultad = IDdificultad;
        this.Nombre = Nombre;
    }
}