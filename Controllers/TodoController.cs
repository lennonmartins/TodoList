using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNamespace;
using TodoList.DTO;

namespace TodoList.Controller;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> ObterTodos([FromServices]AppDbDataContext appDbDataContext)
    {
        var todos = await appDbDataContext
            .Todos
            .AsNoTracking()
            .ToListAsync();
        return Ok(todos);
    }
    
    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> ObterPeloId([FromServices] AppDbDataContext appDbDataContext, int id)
    {
        var todo = await appDbDataContext
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    [Route("todos")]
    public async Task<IActionResult> Cadastrar([FromServices] AppDbDataContext appDbDataContext, [FromBody] TodoDto model)
    {
        var todo = new Todo
        {
            Titulo = model.Titulo
        };

        try
        {
            await appDbDataContext.Todos.AddAsync(todo);
            await appDbDataContext.SaveChangesAsync();
            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("todos/{id}")]
    public IActionResult DeletarPeloId([FromServices]AppDbDataContext appDbDataContext, int id)
    {
        var todo = appDbDataContext.Todos.FirstOrDefault(x => x.Id == id);
        
        if (todo == null)
        {
            return NotFound("Registro não encontrado");
        }

        appDbDataContext.Remove(todo);
        appDbDataContext.SaveChanges();

        return NoContent();
    }

    [HttpPut]
    [Route("todos/{id}")]
    public async Task<IActionResult> AlterarPeloId([FromServices] AppDbDataContext appDbDataContext, [FromBody] TodoDto model, int id)
    {
        var todo = await appDbDataContext
            .Todos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null)
        {
            return NotFound("Registro não encontrado");
        }

        todo.Titulo = model.Titulo;
        await appDbDataContext.SaveChangesAsync();
        
        return Ok(todo);
    } 
}