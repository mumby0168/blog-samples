using MediatR;
using System;

namespace MinimalApis.Handlers
{
    public class CreateTodoHandler : IRequestHandler<CreateTodo>
    {
        private readonly TodoStore _todoStore;

        public CreateTodoHandler(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            _todoStore.Todos.Add(new TodoDto(Guid.NewGuid().ToString(), request.Name, false));
            return Task.FromResult(Unit.Value);
        }
    }
}