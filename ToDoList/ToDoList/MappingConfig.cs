using AutoMapper;
using ToDoList.Models;
using ToDoList.Models.Dto;

namespace ToDoList;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Notion, NotionRepositoryGetAllDto>();
            config.CreateMap<NotionRequestPostDto, Notion>();
            config.CreateMap<NotionRequestPutDto, NotionRepositoryUpdateDto>();
            config.CreateMap<Notion, Notion>();
        });
        return mappingConfig;
    }
}