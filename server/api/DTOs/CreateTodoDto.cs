using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public record CreateTodoDto(
    [Range(0,5)]
    int Priority, 
    string Title, 
    string Description);