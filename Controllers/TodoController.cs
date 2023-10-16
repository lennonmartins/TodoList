using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNamespace;

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
}