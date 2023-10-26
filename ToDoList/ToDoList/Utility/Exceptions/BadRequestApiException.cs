namespace ToDoList.Utility.Exceptions;

public class BadRequestApiException : ApiException
{
    private const string _descriptionException = "Неверный запрос, заполните все обязательные поля";
    private const int _statusCode = 400;
    
    public BadRequestApiException() : base(_statusCode, _descriptionException)
    {
    }

    public BadRequestApiException(string description) : base(_statusCode, description)
    {
    }
}