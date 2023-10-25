namespace ToDoList.Handlers.IHandlers;

public interface INotionHandler
{
    Task<string> SaveFileAndGetName(IFormFile getFileNameDto);
    void DeleteImage(string path);
}