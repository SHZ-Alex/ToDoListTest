using ToDoList.Handlers.IHandlers;

namespace ToDoList.Handlers;

public class NotionHandler : INotionHandler
{
    public async Task<string> SaveFileAndGetName(IFormFile getFileNameDto)
    {
        string fileName = Path.GetRandomFileName() + Path.GetExtension(getFileNameDto.FileName);
        string filePath = "wwwroot/NotionDocuments/" + fileName;
        string filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        
        await using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
        {
            await getFileNameDto.CopyToAsync(fileStream);
        }
        
        return filePath;
    }
    
    public void DeleteImage(string imageLocalPath)
    {
        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), imageLocalPath);
        FileInfo file = new FileInfo(oldFilePathDirectory);
        if (file.Exists)
            file.Delete();
    }
}