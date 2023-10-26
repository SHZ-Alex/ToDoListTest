namespace ToDoList.Utility.Exceptions;

public class NotFoundApiException : ApiException
{
    private const string _descriptionException = "Запись не найдена";
    private const int _statusCode = 404;
    
    public NotFoundApiException() : base(_statusCode, _descriptionException)
    {
        
    }

    public NotFoundApiException(string description) : base(_statusCode, description)
    {
        
    }


}