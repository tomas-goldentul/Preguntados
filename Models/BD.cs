using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

class BD
{
    private static string _connectionString = @"Server=localhost;
DataBase TP08_Goldentul_Gartenkrot; Integrated Security=True; TrustServerCertificate=True;";
    public List<Categorias> ObtenerCategorias ()
{
List<Categorias> Categorias = null;
using (SqlConnection connection = new SqlConnection(_connectionString))
{
string storedProcedure = "ObtenerCategorias";
Categorias =
connection.Query<Categorias>(storedProcedure,
commandType: CommandType.StoredProcedure)
.ToList();
}
return Categorias;
}

    public List<Dificultades> ObtenerDificultades ()
{
List<Dificultades> Dificultades = null;
using (SqlConnection connection = new SqlConnection(_connectionString))
{
string storedProcedure = "ObtenerDificultades";
Dificultades =
connection.Query<Dificultades>(storedProcedure,
commandType: CommandType.StoredProcedure)
.ToList();
}
return Dificultades;
}

    public List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
    {
        List<Preguntas> Preguntas = new List<Preguntas>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "EXEC ObtenerPreguntas";
            Preguntas = connection.Query<Preguntas>(query, new { pDificultad = dificultad, pCategoria = categoria }).ToList();
        }
        return Preguntas;
    }

    public List<Respuestas> ObtenerProximasRespuestas(int IDpregunta)
    {
        List<Respuestas> Respuestas = new List<Respuestas>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "EXEC ObtenerProximasRespuestas";
            Respuestas = connection.Query<Respuestas>(query, new { pIDpregunta = IDpregunta}).ToList();
        }
        return Respuestas;
    }

}
