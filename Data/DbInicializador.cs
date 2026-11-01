using Microsoft.Data.Sqlite;

public static class DbInicializador
{
    public static void Inicializar(string connectionString)
    {
        using var con = new SqliteConnection(connectionString);
        con.Open();

        using var cmd = new SqliteCommand(@"
            CREATE TABLE IF NOT EXISTS productos (
                id     INTEGER PRIMARY KEY AUTOINCREMENT,
                nombre TEXT    NOT NULL,
                precio REAL    NOT NULL,
                stock  INTEGER NOT NULL
            );", con);
        cmd.ExecuteNonQuery();
    }
}