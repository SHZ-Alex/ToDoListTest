using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Dto;

public class NotionRequestPutDto
{
    public int id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FileLocalPath { get; set; }
    public IFormFile? UploadedFile { get; set; }
}