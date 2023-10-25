using ToDoList.Models;
using ToDoList.Models.Dto;

namespace ToDoList.Repository.IRepository;

public interface INotionRepository
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    /// <typeparam name="T">Возвращаемое Dto</typeparam>
    /// <returns></returns>
    Task<IEnumerable<T>> Get<T>();
    /// <summary>
    /// Обновляет запись
    /// </summary>
    /// <typeparam name="T">Возвращаемое Dto</typeparam>
    /// <returns></returns>
    Task UpdateAsync(NotionRepositoryUpdateDto dto);
    Task Post(Notion entity);
    Task<T> GetById<T>(int id);
    Task Delete(int id);
}