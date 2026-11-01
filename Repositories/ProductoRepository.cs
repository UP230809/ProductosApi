using Microsoft.Data.Sqlite;

public class ProductoRepository : IProductoRepository
{
    private readonly string _cs;

    public ProductoRepository(IConfiguration config)
        => _cs = config.GetConnectionString("Default")!;

    // CONEXIÓN
    private SqliteConnection Conectar()
    {
        var con = new SqliteConnection(_cs);
        con.Open();
        return con;
    }

    private static Producto Mapear(SqliteDataReader r) => new()
    {
        Id = r.GetInt32(0),
        Nombre = r.GetString(1),
        Precio = Convert.ToDecimal(r.GetValue(2)),
        Stock = r.GetInt32(3)
    };

    // CONSULTAS
    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();
        using var con = Conectar();
        using var cmd = new SqliteCommand(
            "SELECT id, nombre, precio, stock FROM productos", con);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            lista.Add(Mapear(r));
        return lista;
    }

    // BUSCAR
    public Producto? ObtenerPorId(int id)
    {
        using var con = Conectar();
        using var cmd = new SqliteCommand(
            "SELECT id, nombre, precio, stock FROM productos WHERE id = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        return r.Read() ? Mapear(r) : null;
    }

    // INSERT
    public int Insertar(Producto p)
    {
        using var con = Conectar();
        using var cmd = new SqliteCommand(
            @"INSERT INTO productos (nombre, precio, stock)
              VALUES (@nombre, @precio, @stock);
              SELECT last_insert_rowid();", con);
        cmd.Parameters.AddWithValue("@nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@precio", p.Precio);
        cmd.Parameters.AddWithValue("@stock", p.Stock);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // ACTUALIZAR
    public bool Actualizar(Producto p)
    {
        using var con = Conectar();
        using var cmd = new SqliteCommand(
            @"UPDATE productos
              SET nombre = @nombre, precio = @precio, stock = @stock
              WHERE id = @id", con);
        cmd.Parameters.AddWithValue("@nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@precio", p.Precio);
        cmd.Parameters.AddWithValue("@stock", p.Stock);
        cmd.Parameters.AddWithValue("@id", p.Id);
        return cmd.ExecuteNonQuery() > 0;
    }

    // ELIMINAR
    public bool Eliminar(int id)
    {
        using var con = Conectar();
        using var cmd = new SqliteCommand(
            "DELETE FROM productos WHERE id = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }
}