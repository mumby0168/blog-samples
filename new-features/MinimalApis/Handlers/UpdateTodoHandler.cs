using MediatR;

namespace MinimalApis.Handlers
{
    public class UpdateTodoHandler: IRequestHandler<UpdateTodo>
    {
        private readonly TodoStore _todoStore;

        public UpdateTodoHandler(TodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        public Task<Unit> Handle(UpdateTodo request, CancellationToken cancellationToken)
        {
            var todo = _todoStore.Todos.FirstOrDefault(t => t.Id == request.Id);

            if(todo is null)
            {
                throw MediatRException.NotFound();
            }

            _todoStore.Todos.Remove(todo);
            _todoStore.Todos.Add(new (request.Id, request.Name, request.IsComplete));
        

            return Task.FromResult(Unit.Value);
        }
    }
}