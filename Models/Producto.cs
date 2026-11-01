using System.ComponentModel.DataAnnotations;

public class Producto
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = "";

    [Range(0.01, 1000000)]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}