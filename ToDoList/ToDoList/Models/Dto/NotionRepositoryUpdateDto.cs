namespace ToDoList.Models.Dto;

public class NotionRepositoryUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FileLocalPath { get; set; }
}