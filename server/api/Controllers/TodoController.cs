using api.DTOs;
using api.Services;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("/api/todos")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAllTodos()
    {
        var todos = await todoService.GetAllTodos();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo([FromBody] CreateTodoDto dto)
    {
        var result = await todoService.CreateTodo(dto);
        return Ok(result);
    }
}