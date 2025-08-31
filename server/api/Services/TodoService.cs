using api.DTOs;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TodoService(MyDbContext dbContext) : ITodoService
{
    public async Task<Todo> CreateTodo(CreateTodoDto dto)
    {
        var myTodo = new Todo()
        {
            Description = dto.Description,
            Title = dto.Title,
            Id = Guid.NewGuid().ToString(),
            Isdone = false,
        };
        
        await dbContext.Todos.AddAsync(myTodo);
        await dbContext.SaveChangesAsync();
        
        return myTodo;
    }

    public async Task<List<Todo>> GetAllTodos()
    {
        return await dbContext.Todos.ToListAsync();
    }
}