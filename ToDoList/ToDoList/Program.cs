using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoList;
using ToDoList.Data;
using ToDoList.Extensions;
using ToDoList.Handlers;
using ToDoList.Handlers.IHandlers;
using ToDoList.Middlewares;
using ToDoList.Repository;
using ToDoList.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnetion"));
});

builder.AddAppAuthetication();
builder.Services.AddSingleton(MappingConfig.RegisterMaps().CreateMapper());
builder.Services.AddScoped<INotionRepository, NotionRepository>();
builder.Services.AddScoped<INotionHandler, NotionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration();

app.Run();

/*
 
 TODO:

API
    Разработать веб-сервис с использованием ASP.NET Core, который предоставит следующие API-маршруты:
Получение списка всех задач.
    Создание новой задачи.
    Получение информации о задаче по идентификатору.
    Редактирование существующей задачи.
    Удаление задачи.

    EF & PostgreSQL
При разработке использовать Entity Framework Core для взаимодействия с базой данных PostgreSQL.
    И нужно реализовать миграции для создания необходимых таблиц в базе данных.

    Swagger
    Создать документацию для API, используя Swagger

Identity (дополнительно)
Реализовать механизм аутентификации и авторизации пользователей.

    Дополнительные функции (по желанию):
Добавить возможность прикрепления файлов к задачам 

*/