using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    //Controlador usando a injeção do context 
    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    //Adicionar Filme via Post
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges(); //Comando para salvar as alterações no DB
        return CreatedAtAction(nameof(RecuperaFilmePorId),
                               new { id = filme.Id },
                               filme);
    }

    //Leitura da lista dos filmes
    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0,
                                             [FromQuery] int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    //Leitura dentro da lista dos filmes por ID
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
}
