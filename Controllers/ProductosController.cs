using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo) => _repo = repo;

    // GET api/productos
    [HttpGet]
    public ActionResult<List<Producto>> Get() => Ok(_repo.ObtenerTodos());

    // GET api/productos/5
    [HttpGet("{id:int}")]
    public ActionResult<Producto> GetById(int id)
    {
        var producto = _repo.ObtenerPorId(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    // POST api/productos
    [HttpPost]
    public ActionResult<Producto> Post(Producto producto)
    {
        producto.Id = _repo.Insertar(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    // PUT api/productos/5
    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Producto producto)
    {
        producto.Id = id;
        return _repo.Actualizar(producto) ? NoContent() : NotFound();
    }

    // DELETE api/productos/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
        => _repo.Eliminar(id) ? NoContent() : NotFound();
}