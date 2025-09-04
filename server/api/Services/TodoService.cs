using System.ComponentModel.DataAnnotations;
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

    public async Task<Todo> ToggleTodoAsDone(Todo todo)
    {
        var currentObject = await dbContext.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id)
            ?? throw new ValidationException("Todo could not be found");

        currentObject.Isdone = !currentObject.Isdone;
        dbContext.Todos.Update(currentObject);
        await dbContext.SaveChangesAsync();
        
        return currentObject;
    }
}