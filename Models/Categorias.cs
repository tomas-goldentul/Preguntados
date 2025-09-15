public class Categorias
{
    public int IDcategoria { get; set; }
    public string Nombre { get; set; }
    public string Foto { get; set; }

    public Categorias(int IDcategoria, string Nombre, string Foto)
    {
        this.IDcategoria = IDcategoria;
        this.Nombre = Nombre;
        this.Foto = Foto;
    }
}