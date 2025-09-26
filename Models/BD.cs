
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

public static class BD
{
    private static string _connectionString = @"Server=localhost;
DataBase = TP08_Goldentul_Gartenkrot; Integrated Security=True; TrustServerCertificate=True;";
    public static List<Categorias> ObtenerCategorias()
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

    public static List<Dificultades> ObtenerDificultades()
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

    public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        return connection.Query<Preguntas>(
            "ObtenerPreguntas",
            new { dificultad = dificultad, categoria = categoria },
            commandType: CommandType.StoredProcedure
        ).ToList();
    }
}

    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        return connection.Query<Respuestas>(
            "ObtenerProximasRespuestas",
            new { idPregunta = idPregunta },
            commandType: CommandType.StoredProcedure
        ).ToList();
    }
}


}