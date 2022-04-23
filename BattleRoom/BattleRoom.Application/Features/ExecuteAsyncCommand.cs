using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BattleRoom.Application.Features;

public class ExecuteAsyncCommand : IRequest<Task<Unit>>
{
    public class Handler : IRequestHandler<ExecuteAsyncCommand, Task<Unit>>
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary/>
        public Handler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        /// <inheritdoc />
        public Task<Task<Unit>> Handle(ExecuteAsyncCommand request, CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateScope();
            var scopedMediator = scope.ServiceProvider.GetService<IMediator>();

            var task = scopedMediator.Send(request._command, CancellationToken.None);
            _ = task.ContinueWith(_ => scope.Dispose());
            
            return Task.FromResult(task);
        }
    }

    private readonly IRequest _command;
    
    public ExecuteAsyncCommand(IRequest command) => _command = command;
}