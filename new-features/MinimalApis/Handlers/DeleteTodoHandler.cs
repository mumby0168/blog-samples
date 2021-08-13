using MediatR;

namespace MinimalApis.Handlers
{
    public class DeleteTodoHandler: IRequestHandler<DeleteTodo>
    {
        private readonly TodoStore _todoStore;

        public DeleteTodoHandler(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public Task<Unit> Handle(DeleteTodo request, CancellationToken cancellationToken)
        {
            var todo = _todoStore.Todos.FirstOrDefault(t => t.Id == request.Id);

            if(todo is null)
            {
                throw MediatRException.NotFound();
            }

            _todoStore.Todos.Remove(todo);

            return Task.FromResult(Unit.Value);
        }
    }
}