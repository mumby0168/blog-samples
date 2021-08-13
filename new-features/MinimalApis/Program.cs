using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new TodoStore());
builder.Services.AddMediatR(typeof(TodoDto));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" }));

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// app.MapMedaitRPost<CreateTodo>("api/todo");
// app.MapMedaitRGetFromQuery<FetchAllTodos, IEnumerable<TodoDto>>("api/todo");
// app.MapMedaitRGetFromRoute<FetchTodo, TodoDto>("api/todo/{id}");
// app.MapMedaitRPut<UpdateTodo>("api/todo");
// app.MapMedaitRDelete<DeleteTodo>("api/todo");


app.MapGet("api/todo-test/", ([FromQuery] FetchTodo request) => {
    return Results.Ok(request.Id);
});


app.MapGet("api/todo", (FetchTodosQuery query) => {
    return Results.Ok($"Page: {query.Page} and size {query.PageSize}");
});

app.Run();



public static class MediatRExtensions
{
    public static MinimalActionEndpointConventionBuilder MapMedaitRPost<TRequest>(this IEndpointRouteBuilder builder, string pattern) where TRequest : IRequest
        => builder.MapPost(pattern, async (TRequest request, IMediator mediator) =>
        {

            try
            {
                await mediator.Send(request);
                return Results.Ok();
            }
            catch (MediatRException e)
            {
                return Results.StatusCode((int)e.Code);
            }
        });

    public static MinimalActionEndpointConventionBuilder MapMedaitRPut<TRequest>(this IEndpointRouteBuilder builder, string pattern) where TRequest : IRequest
        => builder.MapPut(pattern, async (TRequest request, IMediator mediator) =>
        {

            try
            {
                await mediator.Send(request);
                return Results.Ok();
            }
            catch (MediatRException e)
            {
                return Results.StatusCode((int)e.Code);
            }
        });

    public static MinimalActionEndpointConventionBuilder MapMedaitRGetFromQuery<TRequest, TResponse>(this IEndpointRouteBuilder builder, string pattern) where TRequest : IRequest<TResponse>
        => builder.MapGet(pattern, async ([FromQuery] TRequest request, IMediator mediator) =>
        {

            try
            {
                var got = await mediator.Send(request);
                return Results.Ok(got);
            }
            catch (MediatRException e)
            {
                return Results.StatusCode((int)e.Code);
            }
        });

    public static MinimalActionEndpointConventionBuilder MapMedaitRGetFromRoute<TRequest, TResponse>(this IEndpointRouteBuilder builder, string pattern) where TRequest : IRequest<TResponse>
    => builder.MapGet(pattern, async ([FromRoute] TRequest request, IMediator mediator) =>
    {

        try
        {
            var got = await mediator.Send(request);
            return Results.Ok(got);
        }
        catch (MediatRException e)
        {
            return Results.StatusCode((int)e.Code);
        }
    });

    public static MinimalActionEndpointConventionBuilder MapMedaitRDelete<TRequest>(this IEndpointRouteBuilder builder, string pattern) where TRequest : IRequest
        => builder.MapDelete(pattern, async (TRequest request, IMediator mediator) =>
        {

            try
            {
                await mediator.Send(request);
                return Results.Ok();
            }
            catch (MediatRException e)
            {
                return Results.StatusCode((int)e.Code);
            }
        });
}

public class MediatRException : Exception
{
    public MediatRException(HttpStatusCode code)
    {
        Code = code;
    }

    public HttpStatusCode Code { get; }

    public static MediatRException NotFound() => new MediatRException(HttpStatusCode.NotFound);

    public static MediatRException BadRequest() => new MediatRException(HttpStatusCode.BadRequest);
}

public record CreateTodo(string Name) : IRequest;

public record TodoDto(string Id, string Name, bool IsComplete);

public record UpdateTodo(string Id, string Name, bool IsComplete) : IRequest;

public record DeleteTodo(string Id) : IRequest;

public class FetchTodo : IRequest<TodoDto>
{
    public FetchTodo(string id)
    {
        Id = id;
    }

    public string Id { get; init; }

    public static bool TryParse(string value, out FetchTodo todo)
    {
        todo = new FetchTodo(value);
        return true;
    }
}

public class FetchTodosQuery
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}

public record FetchAllTodos(int Page, int PageSize) : IRequest<IEnumerable<TodoDto>>;

public class TodoStore
{
    public List<TodoDto> Todos { get; set; } = new();
}
