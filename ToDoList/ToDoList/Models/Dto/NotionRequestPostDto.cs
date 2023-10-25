using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Dto;

public class NotionRequestPostDto
{
    public string? Name { get; set; }
    [Required]
    public string UserId { get; set; }
    public string? Description { get; set; }
    public IFormFile? UploadedFile { get; set; }
}