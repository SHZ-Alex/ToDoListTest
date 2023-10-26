using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Handlers.IHandlers;
using ToDoList.Models;
using ToDoList.Models.Dto;
using ToDoList.Repository.IRepository;
using ToDoList.Utility.Exceptions;

namespace ToDoList.Repository;

public class NotionRepository : INotionRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public NotionRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<T>> Get<T>()
    {
        return await _db.Notions
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .Select(x => _mapper.Map<T>(x))
            .ToArrayAsync();
    }

    public async Task UpdateAsync(NotionRepositoryUpdateDto dto)
    {
        Notion? oldEntity = await _db.Notions.FindAsync(dto.Id);

        if (oldEntity is null)
            throw new NotFoundApiException();

        oldEntity.Description = dto.Description;
        oldEntity.Name = dto.Name;
        oldEntity.FileLocalPath = dto.FileLocalPath;

        await _db.SaveChangesAsync();
    }

    public async Task Post(Notion entity)
    {
        _db.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<T> GetById<T>(int id)
    {
        T? entity = await _db.Notions
            .AsNoTracking()
            .Where(x => !x.IsDeleted
                        && id == x.Id)
            .Select(x => _mapper.Map<T>(x))
            .FirstOrDefaultAsync();

        if (entity is null)
            throw new NotFoundApiException();

        return entity;
    }

    public async Task Delete(int id)
    {
        Notion? oldEntity = await _db.Notions.FindAsync(id);

        if (oldEntity is null || oldEntity.IsDeleted)
            throw new NotFoundApiException();

        oldEntity.IsDeleted = true;

        await _db.SaveChangesAsync();
    }
}