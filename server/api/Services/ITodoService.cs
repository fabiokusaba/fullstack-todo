using api.DTOs;
using efscaffold.Entities;

namespace api.Services;

public interface ITodoService
{
    Task<Todo> CreateTodo(CreateTodoDto dto);
    Task<List<Todo>> GetAllTodos();
    Task<Todo> ToggleTodoAsDone(Todo todo);
}