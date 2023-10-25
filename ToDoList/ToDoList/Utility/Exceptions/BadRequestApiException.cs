namespace ToDoList.Utility.Exceptions;

public class BadRequestApiException : ApiException
{
    private const string _descriptionException = "Запись не найдена";
    private const int _statusCode = 404;
    
    public BadRequestApiException() : base(_statusCode, _descriptionException)
    {
    }

    public BadRequestApiException(string description) : base(_statusCode, description)
    {
    }
}