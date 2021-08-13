using MediatR;

namespace MinimalApis.Handlers
{
    public class FetchTodoHandler : IRequestHandler<FetchTodo, TodoDto>
    {
        private readonly TodoStore _todoStore;

        public FetchTodoHandler(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public Task<TodoDto> Handle(FetchTodo request, CancellationToken cancellationToken)
        {
            var todo = _todoStore.Todos.FirstOrDefault(t => t.Id == request.Id);

            if(todo is null)
            {
                throw MediatRException.NotFound();
            }

            return Task.FromResult(todo);
        }
    }
}