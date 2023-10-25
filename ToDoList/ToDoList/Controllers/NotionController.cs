using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Handlers.IHandlers;
using ToDoList.Models;
using ToDoList.Models.Dto;
using ToDoList.Repository.IRepository;
using ToDoList.Utility.Exceptions;

namespace ToDoList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotionController : ControllerBase
{
    private readonly INotionRepository _repository;
    private IMapper _mapper;
    private readonly INotionHandler _handler;

    public NotionController(INotionRepository repository, IMapper mapper, INotionHandler handler)
    {
        _repository = repository;
        _mapper = mapper;
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<NotionRepositoryGetAllDto> response = await _repository.Get<NotionRepositoryGetAllDto>();

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        NotionRepositoryGetDto response = await _repository.GetById<NotionRepositoryGetDto>(id);

        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromForm]NotionRequestPostDto request)
    {
        if (!ModelState.IsValid)
            throw new BadRequestApiException();

        Notion entity = _mapper.Map<Notion>(request);
        if (request.UploadedFile is not null)
            entity.FileLocalPath = await _handler.SaveFileAndGetName(request.UploadedFile);

        await _repository.Post(entity);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromForm]NotionRequestPutDto request)
    {
        NotionRepositoryUpdateDto entity = _mapper.Map<NotionRepositoryUpdateDto>(request);
        if (request.UploadedFile is not null)
        {
            if (request.FileLocalPath is not null)
                _handler.DeleteImage(request.FileLocalPath);
            entity.FileLocalPath = await _handler.SaveFileAndGetName(request.UploadedFile);
        }
            
        await _repository.UpdateAsync(entity);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return Ok();
    }
}