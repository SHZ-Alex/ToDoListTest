using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models;

[Index(nameof(IsDeleted))]
public abstract class BaseDbObject
{
    public bool IsDeleted { get; set; }
}