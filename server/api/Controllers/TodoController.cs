using api.DTOs;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("/api/todos")]
public class TodoController(MyDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAllTodos()
    {
        var todos = await dbContext.Todos.ToListAsync();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo([FromBody] CreateTodoDto dto)
    {
        var myTodo = new Todo()
        {
            Description = dto.Description,
            Title = dto.Title,
            Id = Guid.NewGuid().ToString(),
            Isdone = false,
            Priority = dto.Priority
        };
        
        await dbContext.Todos.AddAsync(myTodo);
        await dbContext.SaveChangesAsync();
        
        return Ok(myTodo);
    }
}