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

    public static List<Preguntas> ObtenerPreguntas(int Dificultad, int Categoria)
    {
        List<Preguntas> Preguntas = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "ObtenerPreguntas";
            Preguntas =
            connection.Query<Preguntas>(storedProcedure, new { pIDdificultad = Dificultad, pIDcategoria = Categoria },
            commandType: CommandType.StoredProcedure)
            .ToList();
        }
        return Preguntas;
    }
    public static List<Respuestas> ObtenerProximasRespuestas(int IDpregunta)
    {
        List<Respuestas> Respuestas = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "ObtenerProximasRespuestas";
            Respuestas =
            connection.Query<Respuestas>(storedProcedure, new { pIDpregunta = IDpregunta },
            commandType: CommandType.StoredProcedure)
            .ToList();
        }
        return Respuestas;
    }

}