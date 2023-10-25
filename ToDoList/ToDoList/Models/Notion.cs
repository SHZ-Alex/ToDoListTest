using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models;

[Index(nameof(UserId))]
public class Notion : BaseDbObject
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    [Required]
    public string UserId { get; set; }
    public string? Description { get; set; }
    
    public string? FileLocalPath { get; set; }
}