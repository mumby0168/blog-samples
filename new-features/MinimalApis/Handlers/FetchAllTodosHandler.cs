using MediatR;

namespace MinimalApis.Handlers
{
    public class FetchAllTodosHandler : IRequestHandler<FetchAllTodos, IEnumerable<TodoDto>>
    {
        private readonly TodoStore _todoStore;

        public FetchAllTodosHandler(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public Task<IEnumerable<TodoDto>> Handle(FetchAllTodos request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_todoStore.Todos.Skip((request.PageSize * request.Page) - request.PageSize).Take(request.PageSize));
        }
    }
}